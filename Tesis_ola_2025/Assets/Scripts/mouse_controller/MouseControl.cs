using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public GameObject questionUI; // tu canvas de preguntas
    public MonoBehaviour playerLookScript; // el script que controla la cámara del jugador (por ej: MouseLook o CameraController)

    void Update()
    {
        if (questionUI.activeSelf)
        {
            // Mostrar cursor y liberar movimiento del mouse
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Desactivar control de cámara
            if (playerLookScript != null)
                playerLookScript.enabled = false;
        }
        else
        {
            // Ocultar cursor y bloquearlo al centro
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Activar control de cámara
            if (playerLookScript != null)
                playerLookScript.enabled = true;
        }
    }
}
