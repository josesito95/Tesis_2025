using UnityEngine;
using UnityEngine.Playables;

public class RevealCounter : MonoBehaviour
{
    public int total = 3;                 // ¿cuántos cuadros son?
    public PlayableDirector director;     // tu VisionDirector (el que tiene la Timeline)

    [Header("Letra O final")]
    public GameObject portalO;            // ACA vas a arrastrar "o de ola para gameplay"

    private int current = 0;              // cuenta actual
    private bool played = false;          // para no reproducir dos veces

    // Llama a este método cada vez que un cuadro se revele
    public void Add1()
    {
        if (played) return;
        current++;

        if (current >= total)
        {
            played = true;
            if (director != null) director.Play(); // ¡reproducir la Timeline!
        }
    }

    // ESTE método lo va a llamar la Timeline con el Signal
    public void ActivateO()
    {
        if (portalO != null)
        {
            portalO.SetActive(true);
        }
    }
}
