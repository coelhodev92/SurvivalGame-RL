using UnityEngine;

public class FireNovaEffect : MonoBehaviour
{
    [Header("Configurações")]
    public float expandSpeed = 5f;
    public float maxScale = 4f;
    public float lifetime = 0.4f;

    private float timer = 0f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Expande o círculo
        float scale = Mathf.Lerp(0f, maxScale, timer / lifetime);
        transform.localScale = new Vector3(scale, scale, 1f);

        // Fade out
        if (spriteRenderer != null)
        {
            float alpha = Mathf.Lerp(0.6f, 0f, timer / lifetime);
            Color c = spriteRenderer.color;
            spriteRenderer.color = new Color(c.r, c.g, c.b, alpha);
        }

        if (timer >= lifetime)
            Destroy(gameObject);
    }
}