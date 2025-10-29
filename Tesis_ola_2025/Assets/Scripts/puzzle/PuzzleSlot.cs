using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleSlot : MonoBehaviour, IDropHandler
{
    public string pieceName; // nombre que debe coincidir con la pieza correcta

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && dropped.name == pieceName)
        {
            dropped.GetComponent<RectTransform>().anchoredPosition = 
                GetComponent<RectTransform>().anchoredPosition;

            Debug.Log("Pieza correcta colocada!");
        }
        else
        {
            Debug.Log("Pieza incorrecta.");
        }
    }
}
