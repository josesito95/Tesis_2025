using UnityEngine;

[RequireComponent(typeof(WallFade))]
public class BothCrystalsWallGate : MonoBehaviour
{
    public string finalMessage = "Somos la combinación de calma y fuerza";

    public void OpenWithFade()
    {
        if (MessageUI.Instance) MessageUI.Instance.Show(finalMessage, 2.5f);
        GetComponent<WallFade>().FadeOut(); // usa tu WallFade (URP Transparent)
    }
}
