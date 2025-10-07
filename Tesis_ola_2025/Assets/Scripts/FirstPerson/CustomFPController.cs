using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFPController : MonoBehaviour
{
    [Header("Camera")]
    public float sensitivity;

    float xRotation;
    float yRotation;

    public GameObject camObj;
    public Transform camPos;
    public bool lockCam;

    [Header("Player")]
    public Animator anim;
    public float speed;

    public bool HasKey { get; set; } = false;

    void Update()
    {
        // --- CONTROL DE CÁMARA ---
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60, 55);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        camObj.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        camObj.transform.position = camPos.position;

        // --- MOVIMIENTO Y ANIMACIÓN ---
        Move();
        AnimationControls();
    }

    void Move()
    {
        // Solo permitir avance/retroceso (W y S)
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = (transform.forward * vertical).normalized;

        transform.position += dir * speed * Time.deltaTime;
    }

    void AnimationControls()
    {
        float vertical = Input.GetAxisRaw("Vertical");

        if (vertical != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        anim.SetFloat("Direction", vertical);
    }
}
