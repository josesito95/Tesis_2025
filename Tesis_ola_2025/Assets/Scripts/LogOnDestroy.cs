using UnityEngine;

public class LogOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.LogError($"[UI] Se destruyó: {name}");
        Debug.LogError(System.Environment.StackTrace);
    }
}
