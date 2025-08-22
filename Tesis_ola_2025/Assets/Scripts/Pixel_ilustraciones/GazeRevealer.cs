using UnityEngine;
using UnityEngine.UI;

public class GazeRevealer : MonoBehaviour
{
    [Header("Raycast")]
    public float maxDistance = 25f;
    public LayerMask layerMask = ~0; // opcional, asigná una Layer "Portfolio" para filtrar

    [Header("UI (opcional)")]
    public Image radialProgress;     // UI Image (Filled, Radial360) en el centro

    PixelatedReveal _current;

    Camera _cam;

    void Awake()
    {
        _cam = GetComponent<Camera>();
        if (_cam == null) _cam = Camera.main;
    }

    void Update()
    {
        // Ray desde el centro de la pantalla
        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.TryGetComponent<PixelatedReveal>(out var pr))
            {
                _current = pr;
                pr.MarkTargetedThisFrame();

                if (radialProgress)
                    radialProgress.fillAmount = pr.Progress01;
            }
            else
            {
                ClearProgressUI();
            }
        }
        else
        {
            ClearProgressUI();
        }
    }

    void ClearProgressUI()
    {
        if (radialProgress)
            radialProgress.fillAmount = 0f;
        _current = null;
    }
}
