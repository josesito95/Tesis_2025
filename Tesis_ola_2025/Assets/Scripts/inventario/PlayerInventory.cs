using UnityEngine;
using UnityEngine.Events;

public enum CrystalType { Blue, Orange }

public class PlayerInventory : MonoBehaviour
{
    public bool hasBlue;
    public bool hasOrange;

    [Header("Eventos")]
    public UnityEvent onBothCollected;   // lo conectamos al fade de la pared + mensaje final

    public bool HasBoth => hasBlue && hasOrange;

    public void Collect(CrystalType type)
    {
        if (type == CrystalType.Blue)  hasBlue = true;
        if (type == CrystalType.Orange) hasOrange = true;

        if (HasBoth)
            onBothCollected?.Invoke();
    }
}
