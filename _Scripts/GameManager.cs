using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float survivalTime = 0f;
    private int totalKills     = 0;
    private bool isGameOver    = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (!isGameOver)
            survivalTime += Time.deltaTime;
    }

    public void RegisterKill()
    {
        totalKills++;
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;

        // Registra a run no save
        if (SaveManager.Instance != null)
            SaveManager.Instance.RegisterRunEnd(survivalTime, totalKills);

        GameOverScreen gameOverScreen = FindAnyObjectByType<GameOverScreen>();
        if (gameOverScreen != null)
            gameOverScreen.Show(GetFormattedTime(), totalKills);
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(survivalTime / 60f);
        int seconds = Mathf.FloorToInt(survivalTime % 60f);
        return $"{minutes:00}:{seconds:00}";
    }

    public bool IsGameOver() => isGameOver;
}