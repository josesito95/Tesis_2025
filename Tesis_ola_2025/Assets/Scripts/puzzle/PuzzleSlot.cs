using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleSlot : MonoBehaviour, IDropHandler
{
    public string pieceName; // nombre exacto esperado (ej: "piece_1")

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        var piece = eventData.pointerDrag.GetComponent<PuzzlePiece>();
        if (piece == null || piece.IsPlaced()) return;

        if (piece.name == pieceName)
        {
            // Colocar en este slot
            piece.PlaceOnSlot(GetComponent<RectTransform>());
            Debug.Log($"✅ Pieza correcta: {piece.name} → {pieceName}");
        }
        else
        {
            Debug.Log($"❌ Pieza incorrecta: {piece.name} (slot {pieceName})");
        }
    }
}
