using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;

    [Header("Assign the CanvasGroup of FadePanel")]
    public CanvasGroup fadeGroup;

    [Header("Timings")]
    public float fadeOutTime = 0.8f;
    public float fadeInTime = 0.8f;

    bool busy = false;

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // IMPORTANT: preserva ESTE objeto (Canvas_Fade) y sus hijos
        DontDestroyOnLoad(gameObject);

        // Si no está asignado, lo busca en los hijos (incluye inactivos)
        if (fadeGroup == null)
            fadeGroup = GetComponentInChildren<CanvasGroup>(true);

        if (fadeGroup != null)
        {
            fadeGroup.alpha = 0f;
            fadeGroup.blocksRaycasts = true;
        }

        // Para hacer fade-in al entrar a una escena nueva
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Si por algún motivo el CanvasGroup se perdió, re-buscar
        if (fadeGroup == null)
            fadeGroup = GetComponentInChildren<CanvasGroup>(true);

        if (fadeGroup != null)
            StartCoroutine(FadeTo(0f, fadeInTime));
    }

    public void FadeAndLoad(string sceneName)
    {
        if (busy) return;
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        busy = true;

        // Re-chequeo por si se perdió referencia
        if (fadeGroup == null)
            fadeGroup = GetComponentInChildren<CanvasGroup>(true);

        if (fadeGroup == null)
        {
            Debug.LogError("FadeController: fadeGroup es null (no encuentra CanvasGroup).");
            SceneManager.LoadScene(sceneName);
            busy = false;
            yield break;
        }

        yield return FadeTo(1f, fadeOutTime); // negro antes
        yield return null;                    // asegurar frame
        SceneManager.LoadScene(sceneName);
        // Fade-in ocurre en OnSceneLoaded
        busy = false;
    }

    IEnumerator FadeTo(float target, float duration)
    {
        float start = fadeGroup.alpha;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / duration);
            fadeGroup.alpha = Mathf.Lerp(start, target, k);
            yield return null;
        }
        fadeGroup.alpha = target;
    }
}
