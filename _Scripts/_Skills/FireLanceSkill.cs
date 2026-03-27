using UnityEngine;

public class FireLanceSkill : BaseSkill
{
    [Header("Lança de Fogo")]
    public GameObject fireLancePrefab;
    public float range = 15f;

    protected override void Awake()
    {
        base.Awake();
        baseCooldown = 3f;
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
        Transform target = FindClosestEnemy(range);
        if (target == null) return;

        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;

        GameObject lance = Instantiate(fireLancePrefab, transform.position, Quaternion.identity);
        FireLanceProjectile projectile = lance.GetComponent<FireLanceProjectile>();

        if (projectile != null)
        {
            projectile.SetDirection(direction);

            // Escala o dano com Inteligência
            if (playerAttributes != null)
                projectile.damage = projectile.baseDamage * playerAttributes.DamageMultiplier;
        }
    }
}