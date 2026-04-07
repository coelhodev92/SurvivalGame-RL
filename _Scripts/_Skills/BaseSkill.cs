using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    [Header("Configurações Base")]
    public float baseCooldown  = 3f;
    public string SkillName    = "Habilidade";
    public string ownerClass   = ""; // Nome da classe dona desta skill

    protected PlayerSkillController skillController;
    protected PlayerAttributes playerAttributes;
    protected float cooldownTimer = 0f;

    protected virtual void Awake()
    {
        skillController = GetComponentInParent<PlayerSkillController>();
        if (skillController == null)
            skillController = GetComponent<PlayerSkillController>();

        playerAttributes = GetComponentInParent<PlayerAttributes>();
        if (playerAttributes == null)
            playerAttributes = GetComponent<PlayerAttributes>();
    }

    protected virtual void Start()
    {
        if (skillController != null)
            skillController.RegisterSkill(this);
    }

    protected float GetCurrentCooldown()
    {
        if (playerAttributes == null) return baseCooldown;
        return Mathf.Max(0.5f, baseCooldown * playerAttributes.CooldownMultiplier);
    }

    protected Transform FindClosestEnemy(float range = 20f)
    {
        Collider2D[] hits  = Physics2D.OverlapCircleAll(transform.position, range);
        Transform closest  = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest         = hit.transform;
            }
        }

        return closest;
    }

    protected abstract void UseSkill();
}