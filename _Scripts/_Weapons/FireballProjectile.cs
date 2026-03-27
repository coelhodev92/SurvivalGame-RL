using UnityEngine;

public class FireballProjectile : Projectile
{
    [Header("Fireball")]
    public float rotationSpeed = 300f;

    protected override void OnUpdate()
    {
        // Rotaciona a bola de fogo enquanto se move
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}