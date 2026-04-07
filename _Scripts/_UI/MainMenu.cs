using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (GameSceneManager.Instance != null)
            GameSceneManager.Instance.GoToLobby();
    }
}