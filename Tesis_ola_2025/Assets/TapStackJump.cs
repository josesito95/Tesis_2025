using UnityEngine;

public class TapStackJump : MonoBehaviour
{
    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Fuerza de salto")]
    public float baseJumpForce = 7f;         // salto base
    public float perTapBonus = 0.8f;         // cuánto suma cada toque
    public float maxExtraCharge = 20f;       // tope del acumulador

    [Header("Ventana de acumulación")]
    public float tapWindow = 0.6f;           // tiempo entre toques que siguen sumando
    public float passiveDecayPerSec = 0.0f;  // opcional: decaimiento por segundo de la carga (0 = sin decaimiento)

    [Header("Consumo al saltar (opcional)")]
    public bool consumeOnJump = false;       // si true, consume parte de la carga al saltar
    [Range(0f, 1f)]
    public float consumePercent = 0.5f;      // ej. 0.5 = consume 50% de la carga

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;

    Rigidbody rb;
    float extraCharge = 0f;
    float lastTapTime = -999f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (!rb) Debug.LogError("TapStackJump: Falta Rigidbody en el player.");
        if (!groundCheck) Debug.LogWarning("TapStackJump: Asigna un GroundCheck en el Inspector.");
    }

    void Update()
    {
        // Decaimiento pasivo (opcional)
        if (passiveDecayPerSec > 0f && extraCharge > 0f)
        {
            extraCharge = Mathf.Max(0f, extraCharge - passiveDecayPerSec * Time.deltaTime);
        }

        // Registrar toques de salto para acumular carga
        if (Input.GetKeyDown(jumpKey))
        {
            float dt = Time.time - lastTapTime;
            lastTapTime = Time.time;

            // Si el toque ocurre dentro de la ventana, sumamos. Si no, reiniciamos cadena pero conservamos la carga previa.
            if (dt <= tapWindow)
            {
                extraCharge = Mathf.Min(maxExtraCharge, extraCharge + perTapBonus);
            }
            else
            {
                // Primer toque de una nueva cadena (también suma)
                extraCharge = Mathf.Min(maxExtraCharge, extraCharge + perTapBonus);
            }

            // Si estamos en el suelo, ejecutar el salto inmediatamente con la carga acumulada.
            if (IsGrounded())
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        // Resetear velocidad vertical para que el impulso sea consistente
        Vector3 v = rb.velocity;
        v.y = 0f;
        rb.velocity = v;

        float force = baseJumpForce + extraCharge;
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);

        if (consumeOnJump && extraCharge > 0f)
        {
            extraCharge *= (1f - consumePercent); // consume un porcentaje
        }
        // Si no consumimos, la carga queda y el próximo salto será aún más alto (hasta el tope).
    }

    bool IsGrounded()
    {
        if (!groundCheck) return false;
        return Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }

    // --- Helpers públicos para debug/UI ---
    public float GetExtraCharge() => extraCharge;
    public void ResetCharge() => extraCharge = 0f;
}