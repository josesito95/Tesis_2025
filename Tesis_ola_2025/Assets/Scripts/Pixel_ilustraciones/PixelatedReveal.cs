using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class PixelatedReveal : MonoBehaviour
{
    [Header("Material/Shader")]
    public string pixelBlocksProperty = "_PixelBlocks";
    [Range(1, 4096)] public int revealedBlocks = 1;
    [Range(1, 4096)] public int startBlocks = 64;

    [Header("Gaze Settings")]
    public float gazeSecondsToReveal = 1.5f;
    public bool stayRevealed = true;
    public float resetSpeed = 2f;

    [Header("Counter Reference")]
    public RevealCounter counter;

    [Header("Luz de Revelado")] // 🔥 NUEVO
    public PaintingLightController lightController; // 🔥 NUEVO

    // Lectura externa
    public float Progress01 => Mathf.Clamp01(_gazeTimer / gazeSecondsToReveal);
    public bool IsRevealed => _isRevealed;

    Renderer _rend;
    MaterialPropertyBlock _mpb;
    float _gazeTimer;
    bool _isTargeted;
    bool _isRevealed;
    bool _alreadyCounted = false;
    int _propId;

    void Awake()
    {
        _rend = GetComponent<Renderer>();
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
            float current = GetBlocks();
            float next = Mathf.MoveTowards(current, startBlocks, resetSpeed * Time.deltaTime);
            SetBlocks(next);
            _gazeTimer = Mathf.Max(0f, _gazeTimer - Time.deltaTime);
        }
        else
        {
            _gazeTimer += Time.deltaTime;
            float t = Mathf.Clamp01(_gazeTimer / gazeSecondsToReveal);

            float blocks = Mathf.Lerp(startBlocks, revealedBlocks, t);
            SetBlocks(blocks);

            if (_gazeTimer >= gazeSecondsToReveal)
            {
                _isRevealed = true;
                if (stayRevealed) SetBlocks(revealedBlocks);

                if (!_alreadyCounted)
                {
                    if (counter != null)
                        counter.Add1();

                    _alreadyCounted = true;

                    // 🔥 NUEVO: encender luz cuando se revela
                    if (lightController != null)
                        lightController.TurnOnLight();
                }
            }
        }

        _isTargeted = false;
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
        _alreadyCounted = false;
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
