using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [Header("XP")]
    public GameObject experienceOrbPrefab;
    public int orbCount = 1;

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
        if (experienceOrbPrefab != null)
        {
            for (int i = 0; i < orbCount; i++)
            {
                // Offset aleatório para os orbes não empilharem
                Vector2 offset = Random.insideUnitCircle * 0.5f;
                Vector3 spawnPos = transform.position + (Vector3)offset;
                Instantiate(experienceOrbPrefab, spawnPos, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }
}