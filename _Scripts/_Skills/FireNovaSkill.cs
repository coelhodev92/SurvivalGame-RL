using UnityEngine;

public class FireNovaSkill : BaseSkill
{
    [Header("Nova de Fogo")]
    public float radius = 4f;
    public float baseDamageValue = 40f;
    public GameObject novaEffectPrefab;

    protected override void Awake()
    {
        base.Awake();
        baseCooldown = 5f;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= GetCurrentCooldown())
        {
            cooldownTimer = 0f;
            UseSkill();
        }
    }

    protected override void UseSkill()
    {
        // Calcula dano com Inteligência
        float finalDamage = baseDamageValue;
        if (playerAttributes != null)
            finalDamage = baseDamageValue * playerAttributes.DamageMultiplier;

        // Aplica dano em todos os inimigos no raio
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            Health health = hit.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(finalDamage);
        }

        // Spawna efeito visual
        if (novaEffectPrefab != null)
            Instantiate(novaEffectPrefab, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.3f, 0f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}