using UnityEngine;

public class OPortalFX : MonoBehaviour
{
    public float appearSpeed = 2f;
    public float floatHeight = 0.15f;
    public float floatSpeed = 1.5f;
    public float rotationSpeed = 25f;

    private Vector3 targetScale;
    private Vector3 startPos;
    private float timer = 0f;

    void OnEnable()
    {
        targetScale = transform.localScale;
        transform.localScale = Vector3.zero;
        startPos = transform.position;
    }

    void Update()
    {
        // crecer suavemente
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * appearSpeed);

        // flotación
        timer += Time.deltaTime;
        transform.position = startPos + Vector3.up * Mathf.Sin(timer * floatSpeed) * floatHeight;

        // rotación
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
