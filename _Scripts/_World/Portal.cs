using UnityEngine;
using TMPro;

public class Portal : MonoBehaviour
{
    [Header("Configurações")]
    public string portalLabel    = "Floresta Sombria";
    public KeyCode interactKey   = KeyCode.E;

    [Header("Referências")]
    public GameObject promptObject;
    public TextMeshPro promptText;

    private bool playerInRange = false;

    private void Start()
    {
        if (promptObject != null)
            promptObject.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(interactKey))
            EnterPortal();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (promptObject != null)
        {
            promptObject.SetActive(true);
            if (promptText != null)
                promptText.text = $"[E] Entrar — {portalLabel}";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (promptObject != null)
            promptObject.SetActive(false);
    }

    private void EnterPortal()
    {
        if (GameSceneManager.Instance != null)
            GameSceneManager.Instance.GoToGame();
    }
}