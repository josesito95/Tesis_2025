using UnityEngine;

public class OrbitWall : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;          // El personaje
    public Transform orbitCenter;     // Centro de la O
    public Transform wall;            // La pared que orbitará

    [Header("Órbita")]
    public float orbitRadius = 5f;    // Radio de la órbita
    public float degreesPerMeter = 30f; // Grados por metro de avance
    public bool clockwise = true;     // Dirección de la órbita

    [Header("Orientación")]
    [Tooltip("0 = mira adelante, 90 = mira derecha, 180 = mira atrás, -90 = mira izquierda")]
    public float rotationOffset = 90f; // Ajuste para que la pared esté derecha

    private float startZ;

    void Start()
    {
        if (player == null || orbitCenter == null || wall == null)
        {
            Debug.LogError("[OrbitWall] Faltan referencias en el inspector");
            enabled = false;
            return;
        }

        startZ = player.position.z;
        PositionWallOnOrbit(0f);
    }

    void Update()
    {
        float distanceZ = player.position.z - startZ;
        if (distanceZ > 0f)
        {
            float angle = distanceZ * degreesPerMeter * (clockwise ? 1f : -1f);
            PositionWallOnOrbit(angle);
        }
    }

    void PositionWallOnOrbit(float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        
        // Calcular posición en la órbita (círculo)
        float x = orbitCenter.position.x + Mathf.Sin(angleRad) * orbitRadius;
        float z = orbitCenter.position.z + Mathf.Cos(angleRad) * orbitRadius;
        
        // Mover la pared a la posición calculada
        wall.position = new Vector3(x, wall.position.y, z);
        
        // Calcular la dirección tangente (perpendicular al radio)
        Vector3 tangentDirection = new Vector3(Mathf.Cos(angleRad), 0f, -Mathf.Sin(angleRad));
        
        // Aplicar rotación con offset ajustable desde el Inspector
        wall.rotation = Quaternion.LookRotation(tangentDirection) * Quaternion.Euler(0f, rotationOffset, 0f);
    }

    // Para debug visual en la escena
    void OnDrawGizmosSelected()
    {
        if (orbitCenter != null)
        {
            // Dibujar círculo de la órbita
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(orbitCenter.position, orbitRadius);
            
            // Dibujar línea desde el centro hasta la pared actual
            if (wall != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(orbitCenter.position, wall.position);
                
                // Dibujar dirección de la pared
                Gizmos.color = Color.blue;
                Vector3 wallForward = wall.forward * 2f;
                Gizmos.DrawLine(wall.position, wall.position + wallForward);
            }
        }
    }
}