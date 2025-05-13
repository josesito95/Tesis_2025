using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboInteractivo : MonoBehaviour
{
    public enum LetraTipo { O, L, A }
    public LetraTipo letra;
    public Material materialEncendido;
    
    private bool fueActivado = false;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        if (fueActivado) return;

        // Cambia el color del cubo para feedback
        // rend.material = CuboApagado;

        // Llama al administrador para prender la letra
        GameObject.FindObjectOfType<AdministradorLetras>().ActivarLetra(letra);

        fueActivado = true;
    }
}