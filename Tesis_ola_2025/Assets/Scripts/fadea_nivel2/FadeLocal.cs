using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeLocal : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public float fadeOutTime = 0.8f;
    public float fadeInTime = 0.8f;

    bool busy = false;

    void Awake()
    {
        if (fadeGroup == null) fadeGroup = GetComponentInChildren<CanvasGroup>(true);

        fadeGroup.alpha = 0f;
        fadeGroup.interactable = false;
        fadeGroup.blocksRaycasts = false; // NO bloquea UI del quiz
    }

    public void FadeOutAndLoad(string sceneName)
    {
        if (busy) return;
        StartCoroutine(CoFadeOutAndLoad(sceneName));
    }

    IEnumerator CoFadeOutAndLoad(string sceneName)
    {
        busy = true;

        // Bloquea clicks SOLO durante el fade-out
        fadeGroup.blocksRaycasts = true;

        float t = 0f;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = Mathf.Clamp01(t / fadeOutTime);
            yield return null;
        }

        // Carga escena
        SceneManager.LoadScene(sceneName);
    }

    // Llamalo desde Nivel_2 si querés fade-in (opcional)
    public IEnumerator FadeIn()
    {
        fadeGroup.alpha = 1f;
        fadeGroup.blocksRaycasts = true;

        float t = 0f;
        while (t < fadeInTime)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = 1f - Mathf.Clamp01(t / fadeInTime);
            yield return null;
        }

        fadeGroup.alpha = 0f;
        fadeGroup.blocksRaycasts = false; // vuelve a dejar UI libre
        busy = false;
    }
}
