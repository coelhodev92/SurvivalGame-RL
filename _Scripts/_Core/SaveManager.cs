using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private SaveData currentData;
    private string savePath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "save.json");
        Load();

        Debug.Log($"SaveManager iniciado. Path: {savePath}");
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(currentData, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"Salvo — Melhor tempo: {currentData.bestSurvivalTime}s | Moedas: {currentData.totalCoins}");
    }

    public void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            currentData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log($"Carregado — Melhor tempo: {currentData.bestSurvivalTime}s | Moedas: {currentData.totalCoins}");
        }
        else
        {
            currentData = new SaveData();
            Debug.Log("Nenhum save encontrado. Criando novo.");
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);

        currentData = new SaveData();
        Debug.Log("Save deletado.");
    }

    public SaveData GetData() => currentData;

    public void AddCoins(int amount)
    {
        currentData.totalCoins += amount;
        Save();
    }

    public void SpendCoins(int amount)
    {
        currentData.totalCoins -= amount;
        currentData.totalCoins = Mathf.Max(0, currentData.totalCoins);
        Save();
    }

    public void RegisterRunEnd(float survivalTime, int kills)
    {
        currentData.totalRuns++;
        currentData.totalKills += kills;

        if (survivalTime > currentData.bestSurvivalTime)
            currentData.bestSurvivalTime = survivalTime;

        Save();
    }

    public bool CanAfford(int cost)
    {
        return currentData.totalCoins >= cost;
    }
}