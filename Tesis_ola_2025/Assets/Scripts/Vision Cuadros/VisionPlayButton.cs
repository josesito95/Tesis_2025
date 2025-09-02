using UnityEngine;
using UnityEngine.Playables;

public class VisionPlayButton : MonoBehaviour
{
    public PlayableDirector director;

    // Este método es tu "botón". Lo llamás desde UI o UnityEvent.
    public void PlayVision()
    {
        if (director != null) director.Play();
    }
}