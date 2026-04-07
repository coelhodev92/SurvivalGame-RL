using UnityEngine;
using TMPro;

public class LobbyHUD : MonoBehaviour
{
    [Header("Referências")]
    public TextMeshProUGUI coinsText;

    private void Start()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (SaveManager.Instance != null)
            coinsText.text = $"Moedas: {SaveManager.Instance.GetData().totalCoins}";
    }
}