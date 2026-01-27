using UnityEngine;
using TMPro;

public class MessageUI : MonoBehaviour
{
    public static MessageUI Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI label;

    [Header("Settings")]
    public float defaultTime = 2f;

    private float timer;

    private void Awake()
    {
        // Singleton seguro: no destruye el Canvas
        if (Instance != null && Instance != this)
        {
            Destroy(this); // destruye solo este script
            return;
        }

        Instance = this;

        if (label != null)
            label.text = "";
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f && label != null)
            {
                label.text = "";
            }
        }
    }

    /// <summary>
    /// Muestra un mensaje en pantalla por un tiempo.
    /// </summary>
    /// <param name="msg">Texto a mostrar</param>
    /// <param name="time">Duración (si es -1 usa defaultTime)</param>
    public void Show(string msg, float time = -1f)
    {
        if (label == null)
        {
            Debug.LogWarning("[MessageUI] Label no asignado.");
            return;
        }

        label.text = msg;
        timer = (time > 0f) ? time : defaultTime;
    }
}
