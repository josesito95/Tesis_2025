using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdministradorLetras : MonoBehaviour
{
    public GameObject letraO, letraL, letraA;
    public Material materialEncendido;

    [Header("Cámara y botón")]
    public GameObject botonComenzar;
    public Transform camara;
    public Vector3 posicionCentrada; // destino de la cámara
    public float velocidadCamara = 2f;

    private bool oActiva = false, lActiva = false, aActiva = false;
    private bool centrarCamara = false;
    private bool botonActivado = false;

    public MouseCameraFollow cameraFollow;

    void Start()
    {
        posicionCentrada = cameraFollow.transform.position;
        // Aseguramos que el botón esté oculto al inicio
        if (botonComenzar != null)
        {
            botonComenzar.SetActive(false);
        }
    }

    public void ActivarLetra(CuboInteractivo.LetraTipo letra)
    {
        switch (letra)
        {
            case CuboInteractivo.LetraTipo.O:
                if (!oActiva) Activar(letraO); oActiva = true;
                break;
            case CuboInteractivo.LetraTipo.L:
                if (!lActiva) Activar(letraL); lActiva = true;
                break;
            case CuboInteractivo.LetraTipo.A:
                if (!aActiva) Activar(letraA); aActiva = true;
                break;
        }

        if (oActiva && lActiva && aActiva && !centrarCamara)
        {
            centrarCamara = true;
            cameraFollow.shouldTrackMouse = false;
        }
    }

    void Update()
    {
        if (centrarCamara && camara != null)
        {
            camara.position = Vector3.Lerp(camara.position, posicionCentrada, Time.deltaTime * velocidadCamara);

            // Cuando llega al centro, activamos el botón (una sola vez)
            if (!botonActivado && Vector3.Distance(camara.position, posicionCentrada) < 0.05f)
            {
                botonComenzar.SetActive(true);
                centrarCamara = false;
                botonActivado = true;
            }
        }
    }

    private void Activar(GameObject letra)
    {
        Renderer rend = letra.GetComponent<Renderer>();
        rend.material = materialEncendido;
        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", new Color(242f / 255f, 103f / 255f, 73f / 255f) * 1.5f);
    }

    // Se llama desde el botón al hacer clic
    public void IrANivel1()
    {
        SceneManager.LoadScene("Nivel1_LetraO"); // Asegurate de que "Nivel1" esté en las Build Settings
    }
}