using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [Header("XP")]
    public GameObject experienceOrbPrefab;
    public int orbCount    = 1;
    public float xpPerOrb  = 10f; // Controlado pelo EnemyController

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
        if (GameManager.Instance != null)
            GameManager.Instance.RegisterKill();

        if (experienceOrbPrefab != null)
        {
            for (int i = 0; i < orbCount; i++)
            {
                Vector2 offset   = Random.insideUnitCircle * 0.5f;
                Vector3 spawnPos = transform.position + (Vector3)offset;

                GameObject orb = Instantiate(experienceOrbPrefab, spawnPos, Quaternion.identity);

                // Aplica o xpValue correto no orbe
                ExperienceOrb experienceOrb = orb.GetComponent<ExperienceOrb>();
                if (experienceOrb != null)
                    experienceOrb.xpValue = xpPerOrb;
            }
        }

        Destroy(gameObject);
    }
}