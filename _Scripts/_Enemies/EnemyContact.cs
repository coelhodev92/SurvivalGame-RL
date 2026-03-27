using UnityEngine;

public class EnemyContact : MonoBehaviour
{
    [Header("Configurações")]
    public float damagePerSecond = 10f;

    private Health playerHealth;
    private bool isTouchingPlayer = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<Health>();
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingPlayer = false;
            playerHealth = null;
        }
    }

    private void Update()
    {
        if (isTouchingPlayer && playerHealth != null)
        {
            playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}