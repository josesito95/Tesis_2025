using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class PixelatedReveal : MonoBehaviour
{
    [Header("Material/Shader")]
    public string pixelBlocksProperty = "_PixelBlocks";
    [Range(1,4096)] public int revealedBlocks = 1;      // nítido
    [Range(1,4096)] public int startBlocks = 64;        // pixelado inicial
    [Header("Gaze Settings")]
    public float gazeSecondsToReveal = 1.5f;           // tiempo mirando para revelar
    public bool stayRevealed = true;                   // ¿queda revelada?
    public float resetSpeed = 2f;                      // qué tan rápido vuelve a pixelarse si no queda revelada

    // Lectura externa (para UI de progreso)
    public float Progress01 => Mathf.Clamp01(_gazeTimer / gazeSecondsToReveal);
    public bool IsRevealed => _isRevealed;

    Renderer _rend;
    MaterialPropertyBlock _mpb;
    float _gazeTimer;
    bool _isTargeted;
    bool _isRevealed;
    int _propId;

    void Awake()
    {
        _rend = GetComponent<Renderer>();
        // por defecto, MeshCollider conviene en un Quad/plane
        if (!TryGetComponent<Collider>(out var c)) gameObject.AddComponent<BoxCollider>();
        _mpb = new MaterialPropertyBlock();
        _propId = Shader.PropertyToID(pixelBlocksProperty);

        SetBlocks(startBlocks);
    }

    void Update()
    {
        if (_isRevealed && stayRevealed)
            return;

        if (!_isTargeted)
        {
            // Si no está mirando y no quedó revelada, volvemos hacia startBlocks
            float current = GetBlocks();
            float next = Mathf.MoveTowards(current, startBlocks, resetSpeed * Time.deltaTime);
            SetBlocks(next);
            _gazeTimer = Mathf.Max(0f, _gazeTimer - Time.deltaTime); // se va perdiendo el progreso
        }
        else
        {
            // Mantiene el timer cargándose mientras mira
            _gazeTimer += Time.deltaTime;
            float t = Mathf.Clamp01(_gazeTimer / gazeSecondsToReveal);

            // Interpolamos de pixelado fuerte a nítido
            float blocks = Mathf.Lerp(startBlocks, revealedBlocks, t);
            SetBlocks(blocks);

            if (_gazeTimer >= gazeSecondsToReveal)
            {
                _isRevealed = true;
                if (stayRevealed) SetBlocks(revealedBlocks);
            }
        }

        _isTargeted = false; // se setea en true desde el raycast cada frame si sigue mirando
    }

    public void MarkTargetedThisFrame()
    {
        _isTargeted = true;
    }

    public void ForceReset()
    {
        _isRevealed = false;
        _gazeTimer = 0f;
        SetBlocks(startBlocks);
    }

    void SetBlocks(float value)
    {
        _rend.GetPropertyBlock(_mpb);
        _mpb.SetFloat(_propId, value);
        _rend.SetPropertyBlock(_mpb);
    }

    float GetBlocks()
    {
        _rend.GetPropertyBlock(_mpb);
        return _mpb.GetFloat(_propId);
    }
}
