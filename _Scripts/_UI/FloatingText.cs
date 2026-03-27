using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [Header("Configurações")]
    public float moveSpeed = 1.5f;
    public float lifetime = 1f;

    private TextMeshPro textMesh;
    private float timer = 0f;
    private Color startColor;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        startColor = textMesh.color;
    }

    public void SetText(string text, Color color)
    {
        textMesh.text = text;
        textMesh.color = color;
        startColor = color;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Sobe suavemente
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Fade out
        float alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
        textMesh.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        if (timer >= lifetime)
            Destroy(gameObject);
    }
}