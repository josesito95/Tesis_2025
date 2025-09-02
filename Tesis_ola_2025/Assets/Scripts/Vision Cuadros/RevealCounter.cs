using UnityEngine;
using UnityEngine.Playables;

public class RevealCounter : MonoBehaviour
{
    public int total = 3;                 // ¿cuántos cuadros son?
    public PlayableDirector director;     // tu VisionDirector (el que tiene la Timeline)

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
}
