using UnityEngine;
using UnityEngine.UI;

public class QuizTrigger : MonoBehaviour
{
    [Header("Configuraciones")]
    public GameObject quizUI;       // El canvas del quiz
    public GameObject wallToRemove; // La pared que queremos eliminar
    public Button correctButton;    // Botón correcto
    public Button[] allButtons;     // Todos los botones (incluido el correcto)

    bool playerInside = false;

    void Start()
    {
        quizUI.SetActive(false);

        // Asignamos eventos
        foreach (Button b in allButtons)
        {
            b.onClick.AddListener(() => OnAnswer(b));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerInside)
        {
            playerInside = true;
            quizUI.SetActive(true);
            // Pausa opcional
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnAnswer(Button selected)
    {
        if (selected == correctButton)
        {
            // ✅ Respuesta correcta
            wallToRemove.SetActive(false);
            quizUI.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            // ❌ Incorrecta (puede quedarse igual o dar feedback)
            Debug.Log("Respuesta incorrecta, intenta de nuevo.");
        }
    }
}
