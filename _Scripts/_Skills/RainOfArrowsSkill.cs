using UnityEngine;
using System.Collections;

public class RainOfArrowsSkill : BaseSkill
{
    [Header("Chuva de Flechas")]
    public GameObject arrowPrefab;
    public float radius      = 5f;
    public float baseDamage  = 20f;
    public int arrowCount    = 8;

    protected override void Awake()
    {
        base.Awake();
        baseCooldown = 6f;
        SkillName    = "Chuva de Flechas";
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
        StartCoroutine(SpawnArrows());
    }

    private IEnumerator SpawnArrows()
    {
        float finalDamage = baseDamage;
        if (playerAttributes != null)
            finalDamage = baseDamage * playerAttributes.DamageMultiplier;

        // Spawna flechas em ângulos distribuídos ao redor do player
        float angleStep = 360f / arrowCount;

        for (int i = 0; i < arrowCount; i++)
        {
            float angle = i * angleStep;
            float rad   = angle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            // Spawna na borda do raio
            Vector3 spawnPos      = transform.position + (Vector3)(dir * radius);
            GameObject arrow      = Instantiate(arrowPrefab, spawnPos, Quaternion.identity);
            Projectile projectile = arrow.GetComponent<Projectile>();

            if (projectile != null)
            {
                // Flexa aponta para o centro — para dentro
                projectile.SetDirection(-dir);
                projectile.damage = finalDamage;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}