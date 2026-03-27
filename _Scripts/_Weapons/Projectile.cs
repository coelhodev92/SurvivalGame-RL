using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Configurações")]
    public float speed = 10f;
    public float damage = 20f;
    public float lifetime = 3f;

    private Vector2 direction;
    private bool hasHit = false; // evita duplo hit

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        OnUpdate();
    }

    protected virtual void OnUpdate() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;
        if (other.CompareTag("Player")) return;

        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            hasHit = true;
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}