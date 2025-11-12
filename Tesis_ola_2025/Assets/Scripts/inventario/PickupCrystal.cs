using UnityEngine;

public class PickupCrystal : MonoBehaviour, IInteractable
{
    public CrystalType type;           // Blue / Anaranjado (o Rojo)
    [TextArea] public string pickupMessage;
    public AudioSource pickupSfx;       // opcional
    public GameObject visual;           // el mesh del cristal (por si el script está en un vacío padre)

    public void Interact(PlayerInventory inv)
    {
        if (inv == null) return;

        inv.Collect(type);
        if (pickupSfx) pickupSfx.Play();
        if (MessageUI.Instance) MessageUI.Instance.Show(pickupMessage, 2f);

        if (visual != null) visual.SetActive(false);
        else gameObject.SetActive(false);
    }

    public string Hint() => "Tomar cristal";
}
