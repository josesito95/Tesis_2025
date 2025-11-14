using UnityEngine;

public class PickupCrystal : MonoBehaviour, IInteractable
{
    public enum CrystalType { Blue, Orange }

    public CrystalType type;
    [TextArea] public string pickupMessage = "";
    public AudioSource pickupSfx;
    public GameObject visual;

    public string Hint()
    {
        return "Tomar cristal";
    }

    public void Interact(PlayerInventory inv)
    {
        if (inv == null)
        {
            Debug.LogWarning("[PickupCrystal] PlayerInventory es NULL, no puedo guardar el cristal.");
            return;
        }

        // Marcar en el inventario
        if (type == CrystalType.Blue)
            inv.CollectBlue();
        else
            inv.CollectOrange();

        // Mensaje del cristal
        if (!string.IsNullOrEmpty(pickupMessage) && MessageUI.Instance != null)
            MessageUI.Instance.Show(pickupMessage, 3f);

        // Sonido
        if (pickupSfx != null)
            pickupSfx.Play();

        // Apagar visual
        if (visual != null)
            visual.SetActive(false);
        else
            gameObject.SetActive(false);
    }
}
