using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Configurações")]
    public float moveSpeed = 2.5f;

    private Transform playerTransform;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Busca o player pela tag
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
            playerTransform = player.transform;
        else
            Debug.LogWarning("EnemyMovement: Nenhum GameObject com tag 'Player' encontrado.");
    }

    private void FixedUpdate()
    {
        if (playerTransform == null) return;

        // Calcula direção até o player e move
        Vector2 direction = ((Vector2)playerTransform.position - rb.position).normalized;
        Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}