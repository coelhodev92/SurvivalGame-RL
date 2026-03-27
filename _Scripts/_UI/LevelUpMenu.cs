using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class LevelUpMenu : MonoBehaviour
{
    [Header("Botões de Upgrade")]
    public GameObject menuPanel;
    public List<Button> upgradeButtons;
    public List<TextMeshProUGUI> upgradeNameTexts;
    public List<TextMeshProUGUI> upgradeDescriptionTexts;

    [Header("Painel de Atributos")]
    public TextMeshProUGUI intText;
    public TextMeshProUGUI wisText;
    public TextMeshProUGUI vitText;
    public TextMeshProUGUI agiText;

    private AttributeDatabase attributeDatabase;
    private List<AttributeData> currentOptions;
    private PlayerAttributes playerAttributes;
    private Health playerHealth;
    private PlayerMovement playerMovement;
    private AutoShooter autoShooter;

    private void Awake()
    {
        attributeDatabase = FindAnyObjectByType<AttributeDatabase>();
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerAttributes = player.GetComponent<PlayerAttributes>();
            playerHealth     = player.GetComponent<Health>();
            playerMovement   = player.GetComponent<PlayerMovement>();
            autoShooter      = player.GetComponent<AutoShooter>();
        }

        menuPanel.SetActive(false);
    }

    public void ShowMenu()
    {
        currentOptions = attributeDatabase.GetRandomAttributes(3);

        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            if (i < currentOptions.Count)
            {
                int capturedIndex = i;
                upgradeNameTexts[i].text        = currentOptions[i].attributeName;
                upgradeDescriptionTexts[i].text = currentOptions[i].description;
                upgradeButtons[i].gameObject.SetActive(true);
                upgradeButtons[i].onClick.RemoveAllListeners();
                upgradeButtons[i].onClick.AddListener(() => SelectAttribute(capturedIndex));
            }
            else
            {
                upgradeButtons[i].gameObject.SetActive(false);
            }
        }

        UpdateAttributeDisplay();

        menuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void UpdateAttributeDisplay()
    {
        if (playerAttributes == null) return;

        intText.text = $"Inteligência: <b>{playerAttributes.intelligence}</b>";
        wisText.text = $"Sabedoria: <b>{playerAttributes.wisdom}</b>";
        vitText.text = $"Vitalidade: <b>{playerAttributes.vitality}</b>";
        agiText.text = $"Agilidade: <b>{playerAttributes.agility}</b>";
    }

    private void SelectAttribute(int index)
    {
        AttributeData chosen = currentOptions[index];
        playerAttributes.IncreaseAttribute(chosen.attributeType, chosen.amount);
        ApplyAttributeEffects();

        menuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ApplyAttributeEffects()
    {
        if (playerHealth != null)
            playerHealth.SetMaxHealth(100f + playerAttributes.MaxHealthBonus);

        if (playerMovement != null)
            playerMovement.moveSpeed = 5f + playerAttributes.MoveSpeedBonus;

        if (autoShooter != null)
            autoShooter.projectileDamage = 20f * playerAttributes.DamageMultiplier;
    }
}