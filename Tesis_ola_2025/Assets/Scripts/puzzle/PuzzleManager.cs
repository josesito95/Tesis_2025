using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public Image[] piezas;      // Las piezas de UI (Image)
    public Sprite[] solucion;   // Sprites en orden correcto
    public GameObject pared;    // Pared que se va a desvanecer
    public float fadeSpeed = 1f;

    private bool puzzleResuelto = false;

    void Start()
    {
        // Mezcla las piezas al inicio
        MezclarPiezas();
    }

    void Update()
    {
        if (!puzzleResuelto && VerificarPuzzle())
        {
            Debug.Log("✅ Puzzle completado!");
            puzzleResuelto = true;
            StartCoroutine(DesvanecerPared());
        }
    }

    void MezclarPiezas()
    {
        // Desordena las imágenes (simple mezcla visual)
        for (int i = 0; i < piezas.Length; i++)
        {
            int randomIndex = Random.Range(0, piezas.Length);
            Sprite temp = piezas[i].sprite;
            piezas[i].sprite = piezas[randomIndex].sprite;
            piezas[randomIndex].sprite = temp;
        }
    }

    bool VerificarPuzzle()
    {
        // Verifica si todas las piezas están en su sprite correcto
        for (int i = 0; i < piezas.Length; i++)
        {
            if (piezas[i].sprite != solucion[i])
                return false;
        }
        return true;
    }

    System.Collections.IEnumerator DesvanecerPared()
    {
        Debug.Log("🧱 Desvaneciendo pared...");

        Renderer renderer = pared.GetComponent<Renderer>();
        Color colorInicial = renderer.material.color;

        while (renderer.material.color.a > 0)
        {
            Color nuevoColor = renderer.material.color;
            nuevoColor.a -= Time.deltaTime * fadeSpeed;
            renderer.material.color = nuevoColor;
            yield return null;
        }

        pared.SetActive(false);
        Debug.Log("🚪 Pared desaparecida.");
    }
}
