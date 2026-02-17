using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToNextScene : MonoBehaviour
{
    [Header("Scene")]
    public string nextSceneName = "Nivel_2";

    [Header("Optional")]
    public bool disableAfterUse = true;

    private bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (used) return;
        if (!other.CompareTag("Player")) return;

        used = true;

        if (disableAfterUse)
            GetComponent<Collider>().enabled = false;

        SceneManager.LoadScene(nextSceneName);
    }
}
