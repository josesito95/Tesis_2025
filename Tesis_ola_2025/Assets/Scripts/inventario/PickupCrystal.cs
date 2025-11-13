using UnityEngine;

public enum CrystalType
{
    Blue,
    Orange
}

public class PickupCrystal : MonoBehaviour, IInteractable
{
    public CrystalType type;
    [TextArea]
    public string pickupMessage = "Tomar cristal";

    public AudioSource pickupSfx;
    public GameObject visual;   // opcional, si no lo usás deja vacío

    // Texto que muestra el Interactor
    public string Hint()
    {
        return "Tomar cristal";
    }

    public void Interact(PlayerInventory inv)
    {
        Debug.Log("[PickupCrystal] Interact con " + name);

        if (inv == null)
        {
            Debug.LogError("[PickupCrystal] PlayerInventory es NULL, no puedo guardar el cristal.");
            return;
        }

        if (type == CrystalType.Blue)
        {
            inv.CollectBlue();
            Debug.Log("[PickupCrystal] Azul representa la calma.");
        }
        else
        {
            inv.CollectOrange();
            Debug.Log("[PickupCrystal] Rojo representa la fuerza.");
        }

        if (pickupSfx != null)
            pickupSfx.Play();

        // Ocultar cristal
        if (visual != null)
            visual.SetActive(false);
        else
            gameObject.SetActive(false);
    }
}
