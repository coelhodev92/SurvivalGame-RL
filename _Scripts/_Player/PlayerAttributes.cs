using UnityEngine;
using System;

public class PlayerAttributes : MonoBehaviour
{
    [Header("Atributos Base")]
    public int intelligence = 5;  // Dano das skills
    public int wisdom = 5;        // Cooldown das skills
    public int vitality = 5;      // Vida máxima
    public int agility = 5;       // Velocidade

    // Eventos para outros sistemas reagirem
    public event Action OnAttributesChanged;

    // Multiplicadores calculados a partir dos atributos
    public float DamageMultiplier => 1f + (intelligence - 5) * 0.1f;
    public float CooldownMultiplier => 1f - (wisdom - 5) * 0.05f;
    public float MaxHealthBonus => (vitality - 5) * 10f;
    public float MoveSpeedBonus => (agility - 5) * 0.2f;

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
        Debug.Log($"{type} agora: {GetAttributeValue(type)}");
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

public enum AttributeType
{
    Intelligence,
    Wisdom,
    Vitality,
    Agility
}