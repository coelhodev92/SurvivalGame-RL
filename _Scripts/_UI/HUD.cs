using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [Header("Vida")]
    public Image healthBar;

    [Header("XP")]
    public Image xpBar;

    [Header("Nível")]
    public TextMeshProUGUI levelText;

    [Header("Timer")]
    public TextMeshProUGUI timerText;

    [Header("Moedas")]
    public TextMeshProUGUI coinsText;

    private Health playerHealth;
    private PlayerExperience playerExperience;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerHealth     = player.GetComponent<Health>();
            playerExperience = player.GetComponent<PlayerExperience>();
        }
        else
        {
            Debug.LogWarning("HUD: Player não encontrado na cena.");
        }
    }

    private void Update()
    {
        if (playerHealth != null)
            healthBar.fillAmount = playerHealth.GetCurrentHealth() / playerHealth.GetMaxHealth();

        if (playerExperience != null)
        {
            xpBar.fillAmount = playerExperience.GetCurrentXP() / playerExperience.GetXPToNextLevel();
            levelText.text   = $"Nível {playerExperience.GetCurrentLevel()}";
        }

        if (timerText != null && GameManager.Instance != null)
            timerText.text = GameManager.Instance.GetFormattedTime();

        if (coinsText != null && SaveManager.Instance != null)
            coinsText.text = $"$ {SaveManager.Instance.GetData().totalCoins}";
    }
}