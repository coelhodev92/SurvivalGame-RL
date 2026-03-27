using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    [Header("Configurações")]
    public float xpValue = 10f;
    public float attractionRadius = 3f;
    public float moveSpeed = 5f;

    private Transform playerTransform;
    private bool isAttracting = false;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerTransform = player.transform;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= attractionRadius)
            isAttracting = true;

        if (isAttracting)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                playerTransform.position,
                moveSpeed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerExperience playerXP = other.GetComponent<PlayerExperience>();
        if (playerXP != null)
            playerXP.AddExperience(xpValue);

        // Spawna número de XP
        if (FloatingTextSpawner.Instance != null)
            FloatingTextSpawner.Instance.SpawnXP(transform.position, xpValue);

        Destroy(gameObject);
    }
}