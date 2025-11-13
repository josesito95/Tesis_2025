using UnityEngine;
using TMPro;

public class Interactor : MonoBehaviour
{
    public Camera cam;
    public float distance = 3f;
    public LayerMask interactMask;   // capa Interactable
    public TextMeshProUGUI hintLabel;

    // 👉 referencia directa al inventario del jugador
    public PlayerInventory playerInventory;

    void Start()
    {
        if (cam == null) cam = Camera.main;

        // Si no lo asignaste a mano, lo busco en la escena
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory == null)
                Debug.LogError("[Interactor] No encontré ningún PlayerInventory en la escena.");
        }

        if (hintLabel != null) hintLabel.text = "";
    }

    void Update()
    {
        if (cam == null) return;

        Ray r = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(r, out RaycastHit hit, distance, interactMask))
        {
            // usa InParent para soportar collider en hijo
            var ia = hit.collider.GetComponentInParent<IInteractable>();

            if (ia != null)
            {
                if (hintLabel != null) hintLabel.text = $"E — {ia.Hint()}";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("[Interactor] E presionada sobre " + hit.collider.name + " ia=" + ia);

                    if (playerInventory == null)
                    {
                        Debug.LogError("[Interactor] playerInventory es NULL, no puedo interactuar.");
                        return;
                    }

                    ia.Interact(playerInventory);
                }

                return;
            }
        }

        if (hintLabel != null) hintLabel.text = "";
    }
}
