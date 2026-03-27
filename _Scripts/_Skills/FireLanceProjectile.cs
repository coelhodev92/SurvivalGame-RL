using UnityEngine;
using System.Collections.Generic;

public class FireLanceProjectile : Projectile
{
    [Header("Lança de Fogo")]
    public float baseDamage = 50f;
    public int maxPierceCount = 3; // Quantos inimigos perfura

    private List<GameObject> hitEnemies = new List<GameObject>();
    private int pierceCount = 0;

    protected override void OnUpdate()
    {
        // Sem efeito visual extra por enquanto
    }

    private new void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;

        // Ignora inimigos já acertados
        if (hitEnemies.Contains(other.gameObject)) return;

        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            hitEnemies.Add(other.gameObject);
            pierceCount++;

            // Destrói após perfurar o máximo de inimigos
            if (pierceCount >= maxPierceCount)
                Destroy(gameObject);
        }
    }
}