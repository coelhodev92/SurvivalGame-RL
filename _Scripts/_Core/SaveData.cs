using System;

[Serializable]
public class SaveData
{
    // Moeda de meta progressão
    public int totalCoins = 0;

    // Estatísticas gerais
    public int totalRuns              = 0;
    public int totalKills             = 0;
    public float bestSurvivalTime     = 0f;

    // Classe selecionada
    public string selectedClassName   = "";

    // Bônus permanentes da loja
    public int permanentIntelligence      = 0;
    public int permanentWisdom            = 0;
    public int permanentVitality          = 0;
    public int permanentAgility           = 0;
    public float permanentDamageBonus     = 0f;
    public float permanentMoveSpeedBonus  = 0f;

    // Desbloqueios
    public bool archerClassUnlocked   = false;
    public bool warriorClassUnlocked  = false;
}