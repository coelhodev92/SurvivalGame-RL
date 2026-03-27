using UnityEngine;

public class MiniBossSpawner : MonoBehaviour
{
    [Header("Configurações")]
    public GameObject miniBossPrefab;
    public float spawnInterval = 60f;
    public float spawnDistanceFromCamera = 2f;

    private float timer = 0f;
    private Camera mainCamera;
    private bool miniBossAlive = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (miniBossAlive) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnMiniBoss();
        }
    }

    private void SpawnMiniBoss()
    {
        float camHeight = mainCamera.orthographicSize;
        float camWidth  = mainCamera.orthographicSize * mainCamera.aspect;
        Vector2 camPos  = mainCamera.transform.position;

        // Spawna sempre acima da câmera
        Vector2 spawnPos = new Vector2(
            camPos.x + Random.Range(-camWidth * 0.5f, camWidth * 0.5f),
            camPos.y + camHeight + spawnDistanceFromCamera
        );

        GameObject boss = Instantiate(miniBossPrefab, spawnPos, Quaternion.identity);

        // Escuta a morte do mini boss para liberar o próximo spawn
        Health health = boss.GetComponent<Health>();
        if (health != null)
            health.OnDeath += () => miniBossAlive = false;

        miniBossAlive = true;
        Debug.Log("Mini Boss spawnado!");
    }
}