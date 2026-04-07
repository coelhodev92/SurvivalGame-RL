using UnityEngine;
using System;

public class PlayerAttributes : MonoBehaviour
{
    [Header("Atributos Base")]
    public int intelligence = 5;
    public int wisdom       = 5;
    public int vitality     = 5;
    public int agility      = 5;

    public event Action OnAttributesChanged;

    // Chamado pelo PlayerClassLoader após aplicar a classe
    public void ApplyPermanentBonuses()
    {
        if (SaveManager.Instance == null) return;

        SaveData data = SaveManager.Instance.GetData();

        intelligence += data.permanentIntelligence;
        wisdom       += data.permanentWisdom;
        vitality     += data.permanentVitality;
        agility      += data.permanentAgility;

        Debug.Log($"Bônus permanentes aplicados — INT:{intelligence} WIS:{wisdom} VIT:{vitality} AGI:{agility}");

        OnAttributesChanged?.Invoke();
    }

    public float DamageMultiplier   => 1f + (intelligence - 5) * 0.1f;
    public float CooldownMultiplier => 1f - (wisdom - 5) * 0.05f;
    public float MaxHealthBonus     => (vitality - 5) * 10f;
    public float MoveSpeedBonus     => (agility - 5) * 0.2f;

    public void IncreaseAttribute(AttributeType type, int amount = 1)
    {
        switch (type)
        {
            case AttributeType.Intelligence: intelligence += amount; break;
            case AttributeType.Wisdom:       wisdom += amount;       break;
            case AttributeType.Vitality:     vitality += amount;     break;
            case AttributeType.Agility:      agility += amount;      break;
        }

        OnAttributesChanged?.Invoke();
    }

    public int GetAttributeValue(AttributeType type)
    {
        switch (type)
        {
            case AttributeType.Intelligence: return intelligence;
            case AttributeType.Wisdom:       return wisdom;
            case AttributeType.Vitality:     return vitality;
            case AttributeType.Agility:      return agility;
            default:                         return 0;
        }
    }
}