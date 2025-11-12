using UnityEngine;
using TMPro;
using System.Collections;

public class MessageUI : MonoBehaviour
{
    public static MessageUI Instance;
    public TextMeshProUGUI label;
    public float fadeTime = 0.25f;

    void Awake()
    {
        Instance = this;
        if (!label) label = GetComponent<TextMeshProUGUI>();
        if (label) label.text = "";
    }

    public void Show(string text, float duration = 2f)
    {
        if (!label) return;
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(text, duration));
    }

    IEnumerator ShowRoutine(string text, float duration)
    {
        label.text = text;
        label.alpha = 0f;
        // fade in
        float t = 0f;
        while (t < fadeTime) { t += Time.deltaTime; label.alpha = Mathf.Lerp(0,1,t/fadeTime); yield return null; }
        label.alpha = 1f;
        // hold
        yield return new WaitForSeconds(duration);
        // fade out
        t = 0f;
        while (t < fadeTime) { t += Time.deltaTime; label.alpha = Mathf.Lerp(1,0,t/fadeTime); yield return null; }
        label.alpha = 0f;
        label.text = "";
    }
}
