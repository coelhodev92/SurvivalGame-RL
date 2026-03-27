using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject enemyPrefab;

    [Header("Configurações de Spawn")]
    public float initialSpawnInterval = 2f;
    public float minimumSpawnInterval = 0.5f;
    public float spawnIntervalReduction = 0.1f;
    public float reductionEverySeconds = 10f;

    [Header("Distância de Spawn")]
    public float spawnDistanceFromCamera = 1.5f;

    private float spawnTimer = 0f;
    private float reductionTimer = 0f;
    private float currentSpawnInterval;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        currentSpawnInterval = initialSpawnInterval;
    }

    private void Update()
    {
        // Controla o tempo entre spawns
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }

        // Reduz o intervalo de spawn com o tempo
        reductionTimer += Time.deltaTime;
        if (reductionTimer >= reductionEverySeconds)
        {
            reductionTimer = 0f;
            ReduceSpawnInterval();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = GetSpawnPositionOutsideCamera();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetSpawnPositionOutsideCamera()
    {
        // Calcula os limites da câmera em world space
        float camHeight = mainCamera.orthographicSize;
        float camWidth = mainCamera.orthographicSize * mainCamera.aspect;

        Vector2 camPosition = mainCamera.transform.position;

        // Escolhe um dos 4 lados aleatoriamente
        int side = Random.Range(0, 4);
        Vector2 spawnPos = Vector2.zero;

        switch (side)
        {
            case 0: // Cima
                spawnPos = new Vector2(
                    Random.Range(camPosition.x - camWidth, camPosition.x + camWidth),
                    camPosition.y + camHeight + spawnDistanceFromCamera
                );
                break;
            case 1: // Baixo
                spawnPos = new Vector2(
                    Random.Range(camPosition.x - camWidth, camPosition.x + camWidth),
                    camPosition.y - camHeight - spawnDistanceFromCamera
                );
                break;
            case 2: // Direita
                spawnPos = new Vector2(
                    camPosition.x + camWidth + spawnDistanceFromCamera,
                    Random.Range(camPosition.y - camHeight, camPosition.y + camHeight)
                );
                break;
            case 3: // Esquerda
                spawnPos = new Vector2(
                    camPosition.x - camWidth - spawnDistanceFromCamera,
                    Random.Range(camPosition.y - camHeight, camPosition.y + camHeight)
                );
                break;
        }

        return spawnPos;
    }

    private void ReduceSpawnInterval()
    {
        currentSpawnInterval -= spawnIntervalReduction;
        currentSpawnInterval = Mathf.Max(currentSpawnInterval, minimumSpawnInterval);
        Debug.Log($"Spawn interval agora: {currentSpawnInterval}s");
    }
}