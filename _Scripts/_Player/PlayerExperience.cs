using UnityEngine;
using System;

public class PlayerExperience : MonoBehaviour
{
    [Header("Configurações")]
    public float xpToNextLevel = 100f;
    public float xpGrowthPerLevel = 50f;

    private float currentXP = 0f;
    private int currentLevel = 1;
    private LevelUpMenu levelUpMenu;

    public event Action OnLevelUp;

    private void Awake()
    {
        levelUpMenu = FindAnyObjectByType<LevelUpMenu>();
    }

    public void AddExperience(float amount)
    {
        currentXP += amount;
        Debug.Log($"XP: {currentXP} / {xpToNextLevel}");

        if (currentXP >= xpToNextLevel)
            LevelUp();
    }

    private void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;
        xpToNextLevel += xpGrowthPerLevel;

        Debug.Log($"LEVEL UP! Nível atual: {currentLevel}");
        OnLevelUp?.Invoke();

        if (levelUpMenu != null)
            levelUpMenu.ShowMenu();
    }

    public float GetCurrentXP() => currentXP;
    public float GetXPToNextLevel() => xpToNextLevel;
    public int GetCurrentLevel() => currentLevel;
}