using UnityEngine;

public class PortalWithFade : MonoBehaviour
{
    public string nextSceneName = "Nivel_2";
    bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (used) return;
        if (!other.CompareTag("Player")) return;

        used = true;

        if (FadeController.Instance != null)
            FadeController.Instance.FadeAndLoad(nextSceneName);
        else
            Debug.LogError("FadeController.Instance es null. Falta Canvas_Fade con FadeController.");
    }
}
