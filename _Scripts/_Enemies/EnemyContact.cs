using UnityEngine;

public class EnemyContact : MonoBehaviour
{
    [Header("Configurações")]
    public float damagePerSecond = 10f;
    public float damageInterval  = 0.5f; // Aplica dano a cada X segundos

    private Health playerHealth;
    private bool isTouchingPlayer = false;
    private float damageTimer     = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerHealth     = other.GetComponent<Health>();
        isTouchingPlayer = true;
        damageTimer      = damageInterval; // Aplica dano imediatamente no primeiro contato
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isTouchingPlayer = false;
        playerHealth     = null;
        damageTimer      = 0f;
    }

    private void Update()
    {
        if (!isTouchingPlayer || playerHealth == null) return;

        damageTimer += Time.deltaTime;

        if (damageTimer >= damageInterval)
        {
            damageTimer = 0f;
            float damageAmount = damagePerSecond * damageInterval;
            playerHealth.TakeDamage(damageAmount);
        }
    }
}