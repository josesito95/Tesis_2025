using UnityEngine;
using System.Collections;

public class PaintingLightController : MonoBehaviour
{
    [Header("Luz de revelado")]
    public Light revealLight;
    public float targetIntensity = 200f;
    public float fadeTime = 1f;

    [Header("Efecto de parpadeo")]
    public int flickerCount = 4;
    public float flickerSpeed = 0.15f;

    [Header("Sonido")]
    public AudioSource lightAudio;

    bool revealed = false;

    void Start()
    {
        // Empieza apagada pero guarda el targetIntensity del inspector
        if (revealLight != null)
            revealLight.intensity = 0f;
    }

    public void TurnOnLight()
    {
        if (revealed) return;
        revealed = true;
        StartCoroutine(LightSequence());
    }

    IEnumerator LightSequence()
    {
        // 🔊 Reproducir sonido
        if (lightAudio != null)
            lightAudio.Play();

        // 💡 Parpadeo inicial (usa el targetIntensity del inspector)
        for (int i = 0; i < flickerCount; i++)
        {
            if (revealLight != null)
                revealLight.intensity = (i % 2 == 0) ? targetIntensity : 0f;

            yield return new WaitForSeconds(flickerSpeed);
        }

        // 🌅 Transición suave desde 0 hasta targetIntensity
        float t = 0f;
        while (t < fadeTime && revealLight != null)
        {
            t += Time.deltaTime;
            revealLight.intensity = Mathf.Lerp(0f, targetIntensity, t / fadeTime);
            yield return null;
        }

        if (revealLight != null)
            revealLight.intensity = targetIntensity;
    }
}