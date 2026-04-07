using UnityEngine;
using TMPro;

public class ClassSelector : MonoBehaviour
{
    [Header("Configurações")]
    public KeyCode interactKey = KeyCode.E;

    [Header("Prompt")]
    public GameObject promptObject;
    public TextMeshPro promptText;

    private bool playerInRange    = false;
    private ClassSelectionUI selectionUI;

    private void Start()
    {
        selectionUI = FindAnyObjectByType<ClassSelectionUI>();

        if (promptObject != null)
            promptObject.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(interactKey))
        {
            if (selectionUI != null)
                selectionUI.Show();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (promptObject != null)
        {
            promptObject.SetActive(true);
            if (promptText != null)
                promptText.text = "[E] Escolher Classe";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (promptObject != null)
            promptObject.SetActive(false);
    }
}