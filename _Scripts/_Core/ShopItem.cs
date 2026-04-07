using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Game/ShopItem")]
public class ShopItem : ScriptableObject
{
    [Header("Informações")]
    public string itemName;
    [TextArea] public string description;
    public int cost;

    [Header("Efeito")]
    public ShopItemType itemType;
    public float value;
}

public enum ShopItemType
{
    PermanentIntelligence,
    PermanentWisdom,
    PermanentVitality,
    PermanentAgility,
    PermanentDamage,
    PermanentMoveSpeed,
}