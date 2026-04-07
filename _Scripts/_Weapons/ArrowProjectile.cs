using UnityEngine;

public class ArrowProjectile : Projectile
{
    [Header("Flecha")]
    public float baseDamage = 12f;

    protected override void OnUpdate()
    {
        // Aponta na direção do movimento
        float angle = Mathf.Atan2(
            GetDirection().y,
            GetDirection().x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}