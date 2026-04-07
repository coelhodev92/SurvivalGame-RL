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
    private PlayerClassLoader classLoader;

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
            classLoader      = player.GetComponent<PlayerClassLoader>();
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

        SaveData saveData = SaveManager.Instance != null ? SaveManager.Instance.GetData() : null;

        int permInt = saveData != null ? saveData.permanentIntelligence : 0;
        int permWis = saveData != null ? saveData.permanentWisdom : 0;
        int permVit = saveData != null ? saveData.permanentVitality : 0;
        int permAgi = saveData != null ? saveData.permanentAgility : 0;

        int baseInt = playerAttributes.intelligence - permInt;
        int baseWis = playerAttributes.wisdom - permWis;
        int baseVit = playerAttributes.vitality - permVit;
        int baseAgi = playerAttributes.agility - permAgi;

        intText.text = permInt > 0 ? $"Inteligência: <b>{baseInt}</b> +{permInt}" : $"Inteligência: <b>{baseInt}</b>";
        wisText.text = permWis > 0 ? $"Sabedoria: <b>{baseWis}</b> +{permWis}"    : $"Sabedoria: <b>{baseWis}</b>";
        vitText.text = permVit > 0 ? $"Vitalidade: <b>{baseVit}</b> +{permVit}"   : $"Vitalidade: <b>{baseVit}</b>";
        agiText.text = permAgi > 0 ? $"Agilidade: <b>{baseAgi}</b> +{permAgi}"    : $"Agilidade: <b>{baseAgi}</b>";
    }

    private void SelectAttribute(int index)
    {
        AttributeData chosen = currentOptions[index];
        playerAttributes.IncreaseAttribute(chosen.attributeType, chosen.amount);
        ApplyAttributeEffects(chosen.attributeType);

        menuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ApplyAttributeEffects(AttributeType changedAttribute)
    {
        if (playerAttributes == null || classLoader == null) return;

        switch (changedAttribute)
        {
            case AttributeType.Vitality:
                if (playerHealth != null)
                    playerHealth.SetMaxHealth(classLoader.classBaseMaxHealth + playerAttributes.MaxHealthBonus);
                break;

            case AttributeType.Agility:
                if (playerMovement != null)
                    playerMovement.moveSpeed = classLoader.classBaseMoveSpeed + playerAttributes.MoveSpeedBonus;
                break;

            case AttributeType.Intelligence:
                if (autoShooter != null)
                    autoShooter.projectileDamage = classLoader.classBaseDamage * playerAttributes.DamageMultiplier;
                break;

            case AttributeType.Wisdom:
                // Calculado dinamicamente nas skills
                break;
        }
    }
}