using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public Image[] piezas; // Las piezas del puzzle (en orden actual)
    public Sprite[] solucion; // Orden correcto
    public GameObject pared; // La pared que se disolverá
    public float fadeSpeed = 1f;

    private bool puzzleCompletado = false;
    private Material paredMat;

    void Start()
    {
        if (pared != null)
            paredMat = pared.GetComponent<Renderer>().material;
    }

    // Llamar cuando se quiera verificar el puzzle (por ejemplo, al presionar un botón)
    public void VerificarPuzzle()
    {
        for (int i = 0; i < piezas.Length; i++)
        {
            if (piezas[i].sprite != solucion[i])
                return; // Si alguna no coincide, el puzzle no está resuelto
        }

        if (!puzzleCompletado)
        {
            puzzleCompletado = true;
            StartCoroutine(DesvanecerPared());
        }
    }

    private IEnumerator DesvanecerPared()
    {
        if (paredMat == null) yield break;

        Color color = paredMat.color;

        while (color.a > 0)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            paredMat.color = color;
            yield return null;
        }

        pared.SetActive(false);
    }
}
