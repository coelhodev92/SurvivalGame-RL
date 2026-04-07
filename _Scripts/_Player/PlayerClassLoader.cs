using UnityEngine;
using System.Collections.Generic;

public class PlayerClassLoader : MonoBehaviour
{
    [Header("Classes Disponíveis")]
    public List<ClassData> availableClasses = new List<ClassData>();

    [HideInInspector] public float classBaseDamage    = 20f;
    [HideInInspector] public float classBaseMoveSpeed = 5f;
    [HideInInspector] public float classBaseMaxHealth = 100f;

    private void Start()
    {
        LoadClass();
    }

    private void LoadClass()
    {
        if (SaveManager.Instance == null) return;

        string selectedName = SaveManager.Instance.GetData().selectedClassName;

        if (string.IsNullOrEmpty(selectedName))
        {
            Debug.Log("Nenhuma classe selecionada — usando padrão.");
            return;
        }

        ClassData classData = availableClasses.Find(c => c.className == selectedName);

        if (classData == null)
        {
            Debug.LogWarning($"Classe '{selectedName}' não encontrada.");
            return;
        }

        ApplyClass(classData);
    }

    private void ApplyClass(ClassData classData)
    {
        // 1 — Guarda dano base puro da classe
        classBaseDamage = classData.projectileDamage;

        // 2 — Aplica atributos base da classe
        PlayerAttributes attributes = GetComponent<PlayerAttributes>();
        if (attributes != null)
        {
            attributes.intelligence = classData.baseIntelligence;
            attributes.wisdom       = classData.baseWisdom;
            attributes.vitality     = classData.baseVitality;
            attributes.agility      = classData.baseAgility;

            // 3 — Calcula bases ANTES dos bônus permanentes
            // Isso garante que a base reflete só a classe pura
            classBaseMaxHealth = 100f + (classData.baseVitality - 5) * 10f;
            classBaseMoveSpeed = 5f   + (classData.baseAgility  - 5) * 0.2f;

            // 4 — Aplica bônus permanentes por cima
            attributes.ApplyPermanentBonuses();
        }

        // 5 — Auto attack com multiplicadores corretos
        AutoShooter shooter = GetComponent<AutoShooter>();
        if (shooter != null)
        {
            shooter.fireRate = classData.fireRate;
            shooter.range    = classData.range;

            if (classData.projectilePrefab != null)
                shooter.projectilePrefab = classData.projectilePrefab;

            if (attributes != null)
                shooter.projectileDamage = classBaseDamage * attributes.DamageMultiplier;
            else
                shooter.projectileDamage = classBaseDamage;
        }

        // 6 — Vida máxima com bônus permanente de vitalidade
        Health health = GetComponent<Health>();
        if (health != null && attributes != null)
            health.SetMaxHealth(classBaseMaxHealth + attributes.MaxHealthBonus);

        // 7 — Velocidade com bônus permanente de agilidade
        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null && attributes != null)
            movement.moveSpeed = classBaseMoveSpeed + attributes.MoveSpeedBonus;

        // 8 — Visual
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = classData.classColor;

        // 9 — Inicializa skills da classe
        PlayerSkillController skillController = GetComponent<PlayerSkillController>();
        if (skillController != null)
            skillController.InitializeForClass(classData.className);

        Debug.Log($"Classe: {classData.className} | Dano:{shooter?.projectileDamage} | HP:{health?.GetMaxHealth()} | Speed:{movement?.moveSpeed}");
    }
}