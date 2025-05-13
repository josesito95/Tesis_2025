using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MouseCameraFollow : MonoBehaviour
{
    public float maxOffset = 3f; // Máxima distancia que la cámara se puede mover desde su punto inicial
    public float moveSpeed = 5f; // Qué tan rápido se mueve la cámara

    private Vector3 initialPosition;

    public bool shouldTrackMouse;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!shouldTrackMouse) return;

        // Convertimos la posición del mouse en un rango de -1 a 1
        float mouseX = (Input.mousePosition.x / Screen.width - 0.5f) * 2;
        float mouseY = (Input.mousePosition.y / Screen.height - 0.5f) * 2;

        // Calculamos el nuevo offset
        Vector3 offset = new Vector3(mouseX * maxOffset, mouseY * maxOffset, 0);

        // Movemos la cámara suavemente hacia la nueva posición
        transform.position = Vector3.Lerp(transform.position, initialPosition + offset, Time.deltaTime * moveSpeed);
    }
}