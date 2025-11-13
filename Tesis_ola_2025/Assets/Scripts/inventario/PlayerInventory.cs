using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool HasBlue  { get; private set; }
    public bool HasOrange { get; private set; }

    // Llamados desde los cristales
    public void CollectBlue()
    {
        HasBlue = true;
        Debug.Log("[Inventory] Tomó cristal AZUL");
        CheckBoth();
    }

    public void CollectOrange()
    {
        HasOrange = true;
        Debug.Log("[Inventory] Tomó cristal ROJO");
        CheckBoth();
    }

    void CheckBoth()
    {
        if (HasBlue && HasOrange)
        {
            Debug.Log("[Inventory] ¡Tiene los dos cristales! Somos la combinación de calma y fuerza.");

            // 🔓 Romper pared2 directamente
            GameObject pared = GameObject.Find("pared2");
            if (pared != null)
            {
                pared.SetActive(false);
                Debug.Log("[Inventory] pared2 desactivada.");
            }
            else
            {
                Debug.LogWarning("[Inventory] No encontré un objeto llamado 'pared2' en la escena.");
            }
        }
    }
}
