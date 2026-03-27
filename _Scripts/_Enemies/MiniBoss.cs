using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    [Header("Configurações")]
    public GameObject chestPrefab;

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
        if (chestPrefab != null)
            Instantiate(chestPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}