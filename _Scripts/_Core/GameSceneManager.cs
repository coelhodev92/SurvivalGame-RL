using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    [Header("Nomes das Cenas")]
    public string lobbySceneName = "LobbyScene";
    public string gameSceneName  = "GameScene";

    [Header("Configurações")]
    public float fadeDuration = 0.5f;

    private SceneTransition sceneTransition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        sceneTransition = FindAnyObjectByType<SceneTransition>();
    }

    public void GoToGame()
    {
        StartCoroutine(LoadSceneWithFade(gameSceneName));
    }

    public void GoToLobby()
    {
        StartCoroutine(LoadSceneWithFade(lobbySceneName));
    }

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        // Garante timeScale normal antes de carregar
        Time.timeScale = 1f;

        if (sceneTransition != null)
            yield return StartCoroutine(sceneTransition.FadeOut());

        yield return SceneManager.LoadSceneAsync(sceneName);

        sceneTransition = FindAnyObjectByType<SceneTransition>();

        if (sceneTransition != null)
            yield return StartCoroutine(sceneTransition.FadeIn());
    }
}