using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class WallFade : MonoBehaviour
{
    public float fadeDuration = 2f;
    private Material mat;
    private Color startColor;

    void Start()
    {
        // Forzamos una copia del material para no modificar el asset original
        mat = GetComponent<Renderer>().material;
        startColor = mat.GetColor("_BaseColor");
        Debug.Log("Material base color encontrado: " + startColor);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            Color newColor = startColor;
            newColor.a = alpha;
            mat.SetColor("_BaseColor", newColor);
            yield return null;
        }

        // Finalmente desactivamos la pared
        gameObject.SetActive(false);
    }
}
