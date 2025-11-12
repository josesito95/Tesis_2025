using UnityEngine;
using TMPro;

public class Interactor : MonoBehaviour
{
    public Camera cam;
    public float distance = 3f;
    public LayerMask interactMask; // crea Layer "Interactable" y asignalo a tus orbes
    public TextMeshProUGUI hintLabel; // opcional para mostrar “E — ...”

    void Start()
    {
        if (cam == null) cam = Camera.main;
        if (hintLabel) hintLabel.text = "";
    }

    void Update()
    {
        if (cam == null) return;

        Ray r = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(r, out RaycastHit hit, distance, interactMask))
        {
            var ia = hit.collider.GetComponent<IInteractable>();
            if (ia != null)
            {
                if (hintLabel) hintLabel.text = $"E — {ia.Hint()}";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    var inv = GetComponentInParent<PlayerInventory>();
                    ia.Interact(inv);
                }
                return;
            }
        }
        if (hintLabel) hintLabel.text = "";
    }
}
