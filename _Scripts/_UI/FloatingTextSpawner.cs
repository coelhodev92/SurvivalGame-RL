using UnityEngine;

public class FloatingTextSpawner : MonoBehaviour
{
    public static FloatingTextSpawner Instance { get; private set; }

    [Header("Prefab")]
    public GameObject floatingTextPrefab;

    [Header("Cores")]
    public Color damageColor = new Color(1f, 0.3f, 0.3f);
    public Color xpColor     = new Color(0.4f, 1f, 0.4f);
    public Color healColor   = new Color(1f, 1f, 0.4f);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SpawnDamage(Vector3 position, float amount)
    {
        Spawn(position, $"{Mathf.RoundToInt(amount)}", damageColor);
    }

    public void SpawnXP(Vector3 position, float amount)
    {
        Spawn(position, $"+{Mathf.RoundToInt(amount)} XP", xpColor);
    }

    private void Spawn(Vector3 position, string text, Color color)
    {
        if (floatingTextPrefab == null) return;

        // Offset maior e mais variado
        Vector3 offset = new Vector3(
            Random.Range(-0.6f, 0.6f),
            Random.Range(0.3f, 0.8f),
            0f
        );

        GameObject obj = Instantiate(floatingTextPrefab, position + offset, Quaternion.identity);

        FloatingText floatingText = obj.GetComponent<FloatingText>();
        if (floatingText != null)
            floatingText.SetText(text, color);
    }
}