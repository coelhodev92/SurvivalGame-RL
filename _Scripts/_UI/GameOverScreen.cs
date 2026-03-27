using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [Header("Referências")]
    public GameObject panel;
    public TextMeshProUGUI survivalTimeText;
    public Button restartButton;

    private void Awake()
    {
        panel.SetActive(false);
        restartButton.onClick.AddListener(Restart);
    }

    public void Show(string formattedTime)
    {
        panel.SetActive(true);
        survivalTimeText.text = $"Você sobreviveu\n{formattedTime}";
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}