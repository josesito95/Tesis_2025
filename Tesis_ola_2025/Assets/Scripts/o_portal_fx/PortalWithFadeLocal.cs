using UnityEngine;

public class PortalWithFadeLocal : MonoBehaviour
{
    public string nextSceneName = "Nivel_2";
    public FadeLocal fade; // arrastrar Canvas_Fade aquí

    bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (used) return;
        if (!other.CompareTag("Player")) return;

        used = true;

        if (fade != null)
            fade.FadeOutAndLoad(nextSceneName);
        else
            Debug.LogError("Portal: No asignaste FadeLocal (arrastrá Canvas_Fade al campo fade).");
    }
}
