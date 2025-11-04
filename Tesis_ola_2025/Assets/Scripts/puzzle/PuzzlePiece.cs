using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RectTransform rect;
    CanvasGroup cg;
    Canvas canvas;

    Vector2 startAnchoredPos;
    Transform startParent;

    bool placed = false;                // <- NUEVO
    RectTransform snappedSlot = null;   // <- NUEVO

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (placed) return; // si ya está colocada, no se arrastra
        startAnchoredPos = rect.anchoredPosition;
        startParent = transform.parent;
        transform.SetParent(canvas.transform, true); // traer al frente
        cg.blocksRaycasts = false; // importante para que el slot reciba el drop
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (placed) return;
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cg.blocksRaycasts = true;

        // Si NO cayó en un slot correcto, volvemos atrás
        if (!placed)
        {
            transform.SetParent(startParent, true);
            rect.anchoredPosition = startAnchoredPos;
        }
        else if (snappedSlot != null)
        {
            // fijamos definitivamente en el slot
            transform.SetParent(snappedSlot.parent, true); // mismo panel
            rect.anchoredPosition = snappedSlot.anchoredPosition;
        }
    }

    // Llamado por el slot cuando la pieza es correcta
    public void PlaceOnSlot(RectTransform slot)
    {
        placed = true;
        snappedSlot = slot;
        rect.anchoredPosition = slot.anchoredPosition;
        cg.blocksRaycasts = true;
    }

    public bool IsPlaced() => placed;
}
