using UnityEngine;
using TMPro;

public class MessageUI : MonoBehaviour
{
    public static MessageUI Instance { get; private set; }

    public TextMeshProUGUI label;
    public float defaultTime = 2f;

    float timer;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (label != null)
            label.text = "";
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f && label != null)
                label.text = "";
        }
    }

    public void Show(string msg, float time = -1f)
    {
        if (label == null)
        {
            Debug.LogWarning("[MessageUI] label es NULL, no puedo mostrar: " + msg);
            return;
        }

        label.text = msg;
        timer = (time > 0f) ? time : defaultTime;
    }
}
