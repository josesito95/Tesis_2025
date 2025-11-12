using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CustomFPController : MonoBehaviour
{
    [Header("Camera")]
    public float sensitivity = 120f;
    public GameObject camObj;
    public Transform camPos;
    public bool lockCam = false;

    float xRotation;
    float yRotation;

    [Header("Player")]
    public Animator anim;
    public float speed = 3.5f;

    // Inventario simple que ya venías usando
    public bool HasKey { get; set; } = false;

    // Internos
    CharacterController cc;
    bool controlsEnabled = true;   // para bloquear/desbloquear desde UI/puzzle

    void Start()
    {
        cc = GetComponent<CharacterController>();

        // Cursor bloqueado por defecto (modo juego)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Si tenés una cámara hija, asegurate de posicionarla
        if (camObj && camPos)
            camObj.transform.position = camPos.position;
    }

    void Update()
    {
        if (!controlsEnabled)
            return; // si desactivaste controles (por puzzle/menú), no hacer nada

        Look();
        Move();
        AnimationControls();
    }

    void Look()
    {
        if (lockCam) return;

        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 55f);

        // Rotación del cuerpo y de la cámara
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        if (camObj)
        {
            camObj.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            if (camPos) camObj.transform.position = camPos.position;
        }
    }

    void Move()
    {
        // Sólo adelante/atrás con W/S
        float vertical = Input.GetAxisRaw("Vertical"); // -1 a 1
        Vector3 dir = (transform.forward * vertical);

        // CharacterController aplica gravedad automáticamente con SimpleMove
        cc.SimpleMove(dir * speed);
    }

    void AnimationControls()
    {
        if (!anim) return;
        float vertical = Input.GetAxisRaw("Vertical");
        bool walking = Mathf.Abs(vertical) > 0.01f;

        anim.SetBool("Walking", walking);
        anim.SetFloat("Direction", vertical);
    }

    // ========= API pública útil para UI/Puzzle =========

    /// <summary> Activa/Desactiva por completo el control del jugador (look + move) </summary>
    public void SetControlsEnabled(bool enabled)
    {
        controlsEnabled = enabled;

        if (enabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    /// <summary> Sólo bloquear/permitir la mirada (por si querés dejar W/S activo) </summary>
    public void SetLookLocked(bool locked)
    {
        lockCam = locked;
    }
}
