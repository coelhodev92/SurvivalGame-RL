using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab Base")]
    public GameObject enemyPrefab;

    [Header("Tipos de Inimigo")]
    public List<EnemyData> enemyTypes = new List<EnemyData>();
    public List<int> spawnWeights     = new List<int>();

    [Header("Configurações de Spawn")]
    public float initialSpawnInterval    = 2f;
    public float minimumSpawnInterval    = 0.5f;
    public float spawnIntervalReduction  = 0.1f;
    public float reductionEverySeconds   = 10f;

    [Header("Distância de Spawn")]
    public float spawnDistanceFromCamera = 1.5f;

    private float spawnTimer       = 0f;
    private float reductionTimer   = 0f;
    private float currentSpawnInterval;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera            = Camera.main;
        currentSpawnInterval  = initialSpawnInterval;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }

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
        GameObject enemy      = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Aplica dados aleatórios baseados nos pesos
        EnemyData selectedData = GetWeightedRandomEnemy();
        if (selectedData != null)
        {
            EnemyController controller = enemy.GetComponent<EnemyController>();
            if (controller != null)
                controller.data = selectedData;
        }
    }

    private EnemyData GetWeightedRandomEnemy()
    {
        if (enemyTypes.Count == 0) return null;

        // Calcula peso total
        int totalWeight = 0;
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            int weight = i < spawnWeights.Count ? spawnWeights[i] : 1;
            totalWeight += weight;
        }

        // Sorteia baseado nos pesos
        int roll = Random.Range(0, totalWeight);
        int cumulative = 0;

        for (int i = 0; i < enemyTypes.Count; i++)
        {
            int weight = i < spawnWeights.Count ? spawnWeights[i] : 1;
            cumulative += weight;

            if (roll < cumulative)
                return enemyTypes[i];
        }

        return enemyTypes[0];
    }

    private Vector2 GetSpawnPositionOutsideCamera()
    {
        float camHeight = mainCamera.orthographicSize;
        float camWidth  = mainCamera.orthographicSize * mainCamera.aspect;
        Vector2 camPos  = mainCamera.transform.position;

        int side        = Random.Range(0, 4);
        Vector2 spawnPos = Vector2.zero;

        switch (side)
        {
            case 0:
                spawnPos = new Vector2(
                    Random.Range(camPos.x - camWidth, camPos.x + camWidth),
                    camPos.y + camHeight + spawnDistanceFromCamera);
                break;
            case 1:
                spawnPos = new Vector2(
                    Random.Range(camPos.x - camWidth, camPos.x + camWidth),
                    camPos.y - camHeight - spawnDistanceFromCamera);
                break;
            case 2:
                spawnPos = new Vector2(
                    camPos.x + camWidth + spawnDistanceFromCamera,
                    Random.Range(camPos.y - camHeight, camPos.y + camHeight));
                break;
            case 3:
                spawnPos = new Vector2(
                    camPos.x - camWidth - spawnDistanceFromCamera,
                    Random.Range(camPos.y - camHeight, camPos.y + camHeight));
                break;
        }

        return spawnPos;
    }

    private void ReduceSpawnInterval()
    {
        currentSpawnInterval -= spawnIntervalReduction;
        currentSpawnInterval  = Mathf.Max(currentSpawnInterval, minimumSpawnInterval);
    }
}