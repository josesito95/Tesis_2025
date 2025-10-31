using UnityEngine;
using UnityEngine.UI;

public class CartelTrigger : MonoBehaviour
{
    public GameObject questionPanel;  // Panel con las preguntas
    public Light revealLight;         // Luz que se enciende al acertar

    private bool playerInside = false;

    void Start()
    {
        questionPanel.SetActive(false);
        revealLight.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            questionPanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            questionPanel.SetActive(false);
        }
    }

    // Este método se llamará desde los botones del UI
    public void OnAnswerSelected(bool isCorrect)
    {
        if (isCorrect)
        {
            revealLight.enabled = true;
            questionPanel.SetActive(false);
            Debug.Log("Respuesta correcta: luz activada");
        }
        else
        {
            Debug.Log("Respuesta incorrecta, intenta otra vez");
        }
    }
}
