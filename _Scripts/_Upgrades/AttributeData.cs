using UnityEngine;

[CreateAssetMenu(fileName = "Attribute", menuName = "Game/Attribute")]
public class AttributeData : ScriptableObject
{
    [Header("Informações")]
    public string attributeName;
    [TextArea] public string description;
    public AttributeType attributeType;
    public int amount = 1;
}