using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class IntroUIController : MonoBehaviour
{
    public TextMeshProUGUI introText;
    public Image background;
    public float displayTime = 3f;
    public float fadeDuration = 2f;
    public AudioSource ambientSound;

    void Start()
    {
        // Opcional: reproducir sonido de fondo
        if (ambientSound != null)
            ambientSound.Play();

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(displayTime);

        float t = 0f;
        Color textColor = introText.color;
        Color bgColor = background.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalizedTime = t / fadeDuration;

            textColor.a = Mathf.Lerp(1f, 0f, normalizedTime);
            bgColor.a = Mathf.Lerp(bgColor.a, 0f, normalizedTime);

            introText.color = textColor;
            background.color = bgColor;

            yield return null;
        }

        introText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }
}
