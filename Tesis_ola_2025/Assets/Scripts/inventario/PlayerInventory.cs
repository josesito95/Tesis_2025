using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [Header("Estado de cristales")]
    [SerializeField] bool hasBlue;
    [SerializeField] bool hasOrange;

    // Lectura desde otros scripts (si hace falta)
    public bool HasBlue  => hasBlue;
    public bool HasOrange => hasOrange;

    [Header("Mensaje final")]
    [TextArea]
    public string finalMessage = "Somos la combinación de calma y fuerza.";

    [Header("Eventos")]
    public UnityEvent OnBothCollected;

    bool alreadyTriggered = false;

    public void CollectBlue()
    {
        hasBlue = true;
        CheckBoth();
    }

    public void CollectOrange()
    {
        hasOrange = true;
        CheckBoth();
    }

    void CheckBoth()
    {
        if (hasBlue && hasOrange && !alreadyTriggered)
        {
            alreadyTriggered = true;

            // Mensaje final en pantalla
            if (MessageUI.Instance != null && !string.IsNullOrEmpty(finalMessage))
            {
                MessageUI.Instance.Show(finalMessage, 3f);
            }

            // Evento para tirar la pared, etc.
            OnBothCollected?.Invoke();
        }
    }
}
