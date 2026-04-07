using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Informações")]
    public string enemyName = "Inimigo";

    [Header("Atributos")]
    public float maxHealth      = 30f;
    public float moveSpeed      = 2.5f;
    public float damagePerHit   = 5f;
    public float damageInterval = 0.5f;
    public int   minCoins       = 1;
    public int   maxCoins       = 3;
    public int   orbCount       = 1;
    public float xpValue        = 10f;

    [Header("Visual")]
    public Color color          = Color.red;
    public Vector3 scale        = Vector3.one;
}