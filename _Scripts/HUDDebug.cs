using UnityEngine;
using UnityEngine.UI;

public class HUDDebug : MonoBehaviour
{
    public Image healthBar;

    private Health playerHealth;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerHealth = player.GetComponent<Health>();

        Debug.Log($"HUDDebug Start - playerHealth: {playerHealth}, healthBar: {healthBar}");
    }

    private void Update()
    {
//        Debug.Log("HUDDebug Update rodando");

        if (playerHealth != null && healthBar != null)
            healthBar.fillAmount = playerHealth.GetCurrentHealth() / playerHealth.GetMaxHealth();
    }
}