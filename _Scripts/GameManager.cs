using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float survivalTime = 0f;
    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        // Garante que o timeScale começa em 1
        Time.timeScale = 1f;
        Debug.Log($"GameManager iniciado. TimeScale: {Time.timeScale}");
    }

    private void Update()
    {
        if (!isGameOver)
        {
            survivalTime += Time.deltaTime;
//            Debug.Log($"TimeScale: {Time.timeScale}");
        }
}

    public void TriggerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;

        Debug.Log($"Game Over! Tempo sobrevivido: {GetFormattedTime()}");

        GameOverScreen gameOverScreen = FindAnyObjectByType<GameOverScreen>();
        if (gameOverScreen != null)
            gameOverScreen.Show(GetFormattedTime());
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(survivalTime / 60f);
        int seconds = Mathf.FloorToInt(survivalTime % 60f);
        return $"{minutes:00}:{seconds:00}";
    }

    public bool IsGameOver() => isGameOver;
}