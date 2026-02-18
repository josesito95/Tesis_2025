using UnityEngine;
using UnityEngine.UI;

public class QuizTrigger : MonoBehaviour
{
    [Header("Configuraciones")]
    public GameObject quizUI;
    public GameObject wallToRemove;
    public Button correctButton;
    public Button[] allButtons;

    [Header("Bloqueo rápido")]
    public bool unlocked = false; // 🔒 empieza bloqueado

    bool playerInside = false;
    bool completed = false;

    void Start()
    {
        quizUI.SetActive(false);

        foreach (Button btn in allButtons)
        {
            Button captured = btn; // ✅ evita bug de captura
            captured.onClick.AddListener(() => OnAnswer(captured));
        }
    }

    // 🔓 llamá a esto cuando el cuadro se revele
    public void UnlockTrigger()
    {
        unlocked = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || playerInside || completed) return;

        // ✅ Gate rápido
        if (!unlocked)
        {
            Debug.Log("Primero revelá el cuadro.");
            return;
        }

        playerInside = true;
        quizUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInside = false;
    }

    void OnAnswer(Button selected)
    {
        if (completed) return;

        if (selected == correctButton)
        {
            completed = true;

            wallToRemove.SetActive(false);
            quizUI.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            gameObject.SetActive(false); // apaga el trigger y listo
        }
        else
        {
            Debug.Log("Respuesta incorrecta, intenta de nuevo.");
        }
    }
}
