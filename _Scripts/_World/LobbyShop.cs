using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class LobbyShop : MonoBehaviour
{
    [Header("Configurações")]
    public KeyCode interactKey = KeyCode.E;
    public List<ShopItem> items = new List<ShopItem>();

    [Header("Prompt de Interação")]
    public GameObject promptObject;
    public TextMeshPro promptText;

    [Header("Referências UI")]
    public GameObject shopPanel;
    public List<Button> shopButtons;
    public List<TextMeshProUGUI> itemNameTexts;
    public List<TextMeshProUGUI> itemDescriptionTexts;
    public List<TextMeshProUGUI> itemCostTexts;
    public TextMeshProUGUI shopCoinsText;

    private bool playerInRange = false;
    private bool isOpen        = false;
    private LobbyHUD lobbyHUD;

    private void Start()
    {
        lobbyHUD = FindAnyObjectByType<LobbyHUD>();
        shopPanel.SetActive(false);

        if (promptObject != null)
            promptObject.SetActive(false);

        BuildShopUI();
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(interactKey))
        {
            if (isOpen) CloseShop();
            else OpenShop();
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
                promptText.text = "[E] Abrir Loja";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        CloseShop();

        if (promptObject != null)
            promptObject.SetActive(false);
    }

    private void OpenShop()
    {
        isOpen = true;
        UpdateShopCoins();
        shopPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void CloseShop()
    {
        isOpen = false;
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void BuildShopUI()
    {
        for (int i = 0; i < shopButtons.Count; i++)
        {
            if (i >= items.Count)
            {
                shopButtons[i].gameObject.SetActive(false);
                continue;
            }

            int capturedIndex = i;
            itemNameTexts[i].text        = items[i].itemName;
            itemDescriptionTexts[i].text = items[i].description;
            itemCostTexts[i].text        = $"{items[i].cost} moedas";
            shopButtons[i].onClick.RemoveAllListeners();
            shopButtons[i].onClick.AddListener(() => BuyItem(capturedIndex));
        }
    }

    private void BuyItem(int index)
    {
        ShopItem item = items[index];

        if (!SaveManager.Instance.CanAfford(item.cost))
        {
            Debug.Log("Moedas insuficientes.");
            return;
        }

        SaveManager.Instance.SpendCoins(item.cost);
        ApplyPermanentBonus(item);
        UpdateShopCoins();

        if (lobbyHUD != null)
            lobbyHUD.UpdateDisplay();

        Debug.Log($"Comprado: {item.itemName}");
    }

    private void ApplyPermanentBonus(ShopItem item)
    {
        SaveData data = SaveManager.Instance.GetData();

        switch (item.itemType)
        {
            case ShopItemType.PermanentIntelligence:
                data.permanentIntelligence += (int)item.value;
                break;
            case ShopItemType.PermanentWisdom:
                data.permanentWisdom += (int)item.value;
                break;
            case ShopItemType.PermanentVitality:
                data.permanentVitality += (int)item.value;
                break;
            case ShopItemType.PermanentAgility:
                data.permanentAgility += (int)item.value;
                break;
            case ShopItemType.PermanentDamage:
                data.permanentDamageBonus += item.value;
                break;
            case ShopItemType.PermanentMoveSpeed:
                data.permanentMoveSpeedBonus += item.value;
                break;
        }

        SaveManager.Instance.Save();
    }

    private void UpdateShopCoins()
    {
        if (shopCoinsText != null && SaveManager.Instance != null)
            shopCoinsText.text = $"Moedas: {SaveManager.Instance.GetData().totalCoins}";
    }
}