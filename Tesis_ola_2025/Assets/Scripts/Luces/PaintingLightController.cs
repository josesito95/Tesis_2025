using UnityEngine;

public class PaintingLightController : MonoBehaviour
{
    [Header("Asignar en Inspector")]
    public Light revealLight;          // La luz que va a prenderse
    public float targetIntensity = 2f; // Qué tan fuerte se prende
    public float fadeTime = 1f;        // Cuánto tarda en prender

    bool revealed = false;

    void Start()
    {
        if (revealLight != null) revealLight.intensity = 0f;
    }

    // Este método lo vamos a llamar desde TU script de revelado
    public void TurnOnLight()
    {
        if (revealed) return;
        revealed = true;
        if (revealLight != null)
            StartCoroutine(FadeLightUp());
    }

    System.Collections.IEnumerator FadeLightUp()
    {
        float t = 0f;
        float start = revealLight.intensity;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            revealLight.intensity = Mathf.Lerp(start, targetIntensity, t / fadeTime);
            yield return null;
        }
        revealLight.intensity = targetIntensity;
    }
}
