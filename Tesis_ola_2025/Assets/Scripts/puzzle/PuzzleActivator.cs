using UnityEngine;

public class PuzzleActivator : MonoBehaviour
{
    public GameObject puzzleCanvas; // Canvas con el puzzle (set inactive por defecto)
    public GameObject player;       // Player FP (arrastrar en inspector)

    private CustomFPController playerController;
    private bool isPlayerInside = false;

    void Start()
    {
        if (puzzleCanvas != null)
            puzzleCanvas.SetActive(false);

        if (player != null)
            playerController = player.GetComponent<CustomFPController>();

        Debug.Log("[PuzzleActivator] Start. puzzleCanvas=" + (puzzleCanvas!=null) + " player=" + (player!=null) + " controller=" + (playerController!=null));
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            OpenPuzzle();
        }
    }

    void OpenPuzzle()
    {
        Debug.Log("[PuzzleActivator] OpenPuzzle called");
        if (puzzleCanvas != null) puzzleCanvas.SetActive(true);
        if (playerController != null) playerController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ClosePuzzle()
    {
        Debug.Log("[PuzzleActivator] ClosePuzzle called");
        if (puzzleCanvas != null) puzzleCanvas.SetActive(false);
        if (playerController != null) playerController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[PuzzleActivator] OnTriggerEnter with " + other.name);
        // Chequeo por referencia directa o tag
        if (player != null && other.gameObject == player)
        {
            isPlayerInside = true;
            Debug.Log("[PuzzleActivator] Player entered trigger (by reference).");
        }
        else if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            Debug.Log("[PuzzleActivator] Player entered trigger (by tag).");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("[PuzzleActivator] OnTriggerExit with " + other.name);
        if (player != null && other.gameObject == player)
        {
            isPlayerInside = false;
            ClosePuzzle();
        }
        else if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            ClosePuzzle();
        }
    }
}
