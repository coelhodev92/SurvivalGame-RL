using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Dados")]
    public EnemyData data;

    private Rigidbody2D rb;
    private Transform playerTransform;
    private Health health;
    private SpriteRenderer spriteRenderer;
    private EnemyDeath enemyDeath;
    private CoinDrop coinDrop;

    private bool isTouchingPlayer = false;
    private float damageTimer     = 0f;
    private Health playerHealth;

    private void Awake()
    {
        rb             = GetComponent<Rigidbody2D>();
        health         = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyDeath     = GetComponent<EnemyDeath>();
        coinDrop       = GetComponent<CoinDrop>();
    }

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerHealth    = player.GetComponent<Health>();
        }

        ApplyData();
    }

    private void ApplyData()
    {
        if (data == null) return;

        // Visual
        spriteRenderer.color = data.color;
        transform.localScale = data.scale;

        // Vida
        health.maxHealth = data.maxHealth;

        // XP — passa para o EnemyDeath
        if (enemyDeath != null)
        {
            enemyDeath.orbCount = data.orbCount;
            enemyDeath.xpPerOrb = data.xpValue;
        }

        // Moedas
        if (coinDrop != null)
        {
            coinDrop.minCoins = data.minCoins;
            coinDrop.maxCoins = data.maxCoins;
        }
    }

    private void FixedUpdate()
    {
        if (playerTransform == null) return;

        Vector2 direction   = ((Vector2)playerTransform.position - rb.position).normalized;
        Vector2 newPosition = rb.position + direction * data.moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private void Update()
    {
        if (!isTouchingPlayer || playerHealth == null) return;

        damageTimer += Time.deltaTime;

        if (damageTimer >= data.damageInterval)
        {
            damageTimer = 0f;
            playerHealth.TakeDamage(data.damagePerHit);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isTouchingPlayer = true;
        damageTimer      = data.damageInterval;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isTouchingPlayer = false;
        damageTimer      = 0f;
    }
}