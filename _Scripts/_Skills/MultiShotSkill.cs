using UnityEngine;

public class MultiShotSkill : BaseSkill
{
    [Header("Multi Shot")]
    public GameObject arrowPrefab;
    public float range         = 12f;
    public float baseDamage    = 12f;
    public int arrowCount      = 3;
    public float spreadAngle   = 30f;

    protected override void Awake()
    {
        base.Awake();
        baseCooldown = 4f;
        SkillName    = "Multi Shot";
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

        float finalDamage = baseDamage;
        if (playerAttributes != null)
            finalDamage = baseDamage * playerAttributes.DamageMultiplier;

        Vector2 baseDirection = ((Vector2)target.position - (Vector2)transform.position).normalized;
        float baseAngle       = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;

        // Calcula os ângulos das flechas em leque
        float startAngle = baseAngle - spreadAngle;
        float step       = arrowCount > 1 ? (spreadAngle * 2f) / (arrowCount - 1) : 0f;

        for (int i = 0; i < arrowCount; i++)
        {
            float angle   = startAngle + step * i;
            float rad     = angle * Mathf.Deg2Rad;
            Vector2 dir   = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject arrow      = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            Projectile projectile = arrow.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.SetDirection(dir);
                projectile.damage = finalDamage;
            }
        }
    }
}