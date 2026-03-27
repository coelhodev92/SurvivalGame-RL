using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [Header("Configurações")]
    public float maxHealth = 100f;

    private float currentHealth;
    private DamageFlash damageFlash;

    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
        damageFlash = GetComponent<DamageFlash>();
    }

    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;

        if (damageFlash != null)
            damageFlash.Flash();

        // Spawna número de dano
        if (FloatingTextSpawner.Instance != null)
            FloatingTextSpawner.Instance.SpawnDamage(transform.position, amount);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void SetMaxHealth(float newMax)
    {
        float difference = newMax - maxHealth;
        maxHealth = newMax;
        currentHealth += difference;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }

    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
}