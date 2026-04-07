using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    [Header("Configurações")]
    public GameObject coinPrefab;
    public int minCoins = 1;
    public int maxCoins = 3;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        health.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        if (coinPrefab == null) return;

        int amount = Random.Range(minCoins, maxCoins + 1);
        for (int i = 0; i < amount; i++)
        {
            Vector2 offset   = Random.insideUnitCircle * 0.4f;
            Vector3 spawnPos = transform.position + (Vector3)offset;
            Instantiate(coinPrefab, spawnPos, Quaternion.identity);
        }
    }
}