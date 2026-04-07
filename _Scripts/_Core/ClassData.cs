using UnityEngine;

[CreateAssetMenu(fileName = "ClassData", menuName = "Game/ClassData")]
public class ClassData : ScriptableObject
{
    [Header("Informações")]
    public string className       = "Mago";
    [TextArea]
    public string classDescription = "Conjura magias de fogo devastadoras.";

    [Header("Atributos Base")]
    public int baseIntelligence = 5;
    public int baseWisdom       = 5;
    public int baseVitality     = 5;
    public int baseAgility      = 5;

    [Header("Visual")]
    public Color classColor     = Color.blue;

    [Header("Auto Attack")]
    public GameObject projectilePrefab;
    public float projectileDamage = 20f;
    public float fireRate         = 1f;
    public float range            = 6f;
}