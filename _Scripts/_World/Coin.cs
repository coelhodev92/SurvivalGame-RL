using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Configurações")]
    public int value          = 1;
    public float attractionRadius = 3f;
    public float moveSpeed    = 6f;
    public float lifetime     = 10f;

    private Transform playerTransform;
    private bool isAttracting = false;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerTransform = player.transform;

        Destroy(gameObject, lifetime);
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

        if (SaveManager.Instance != null)
            SaveManager.Instance.AddCoins(value);

        Destroy(gameObject);
    }
}