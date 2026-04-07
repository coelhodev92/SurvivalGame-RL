using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [Header("Referências")]
    public GameObject panel;
    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI bestTimeText;
    public Button restartButton;
    public Button lobbyButton;

    private void Awake()
    {
        panel.SetActive(false);
        restartButton.onClick.AddListener(Restart);
        lobbyButton.onClick.AddListener(GoToLobby);
    }

    public void Show(string formattedTime, int kills)
    {
        panel.SetActive(true);
        survivalTimeText.text = $"Você sobreviveu\n{formattedTime}";
        killsText.text        = $"Inimigos derrotados: {kills}";

        if (SaveManager.Instance != null)
        {
            float best    = SaveManager.Instance.GetData().bestSurvivalTime;
            int bestMin   = Mathf.FloorToInt(best / 60f);
            int bestSec   = Mathf.FloorToInt(best % 60f);
            bestTimeText.text = $"Melhor tempo: {bestMin:00}:{bestSec:00}";
        }
    }

    private void Restart()
    {
        if (GameSceneManager.Instance != null)
            GameSceneManager.Instance.GoToGame();
    }

    private void GoToLobby()
    {
        if (GameSceneManager.Instance != null)
            GameSceneManager.Instance.GoToLobby();
    }
}