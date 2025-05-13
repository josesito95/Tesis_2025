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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -60, 55);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        camObj.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        camObj.transform.position = camPos.position;

        Move();
        AnimationControls();
    }

    void Move()
    {
        Vector3 dir = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    void AnimationControls()
    {
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        anim.SetFloat("Direction", Input.GetAxisRaw("Vertical"));
    }
}
