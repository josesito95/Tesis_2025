using UnityEngine;

public class PortalWithFadeLocal : MonoBehaviour
{
    public string nextSceneName = "Nivel_2";
    private FadeLocal fade;
    private bool used = false;

    void Awake()
    {
        // Busca el FadeLocal que esté en la escena (Nivel_1)
        fade = FindObjectOfType<FadeLocal>(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (used) return;
        if (!other.CompareTag("Player")) return;

        used = true;

        if (fade != null)
        {
            Debug.Log("PORTAL: Encontré FadeLocal en " + fade.gameObject.name + " y hago fade OUT antes de cargar.");
            fade.FadeOutAndLoad(nextSceneName);
        }
        else
        {
            Debug.LogError("PORTAL: No encontré FadeLocal en la escena. (¿Canvas_Fade está activo en Nivel_1?)");
        }
    }
}
