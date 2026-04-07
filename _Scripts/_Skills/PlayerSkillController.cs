using UnityEngine;
using System.Collections.Generic;

public class PlayerSkillController : MonoBehaviour
{
    private List<BaseSkill> activeSkills = new List<BaseSkill>();
    private List<BaseSkill> lockedSkills = new List<BaseSkill>();
    private PlayerAttributes playerAttributes;
    private SkillUnlockNotification notification;
    private string currentClassName = "";

    private void Awake()
    {
        playerAttributes = GetComponent<PlayerAttributes>();
        notification     = FindAnyObjectByType<SkillUnlockNotification>();
    }

    // Chamado pelo PlayerClassLoader após carregar a classe
    public void InitializeForClass(string className)
    {
        currentClassName = className;
        activeSkills.Clear();
        lockedSkills.Clear();

        BaseSkill[] allSkills = GetComponents<BaseSkill>();

        foreach (BaseSkill skill in allSkills)
        {
            // Desativa todas primeiro
            skill.enabled = false;

            // Ignora skills de outras classes
            if (!string.IsNullOrEmpty(skill.ownerClass) && skill.ownerClass != className)
                continue;

            // Skills sem ownerClass não pertencem a nenhuma classe — ignora
            if (string.IsNullOrEmpty(skill.ownerClass))
                continue;

            // Adiciona na lista correta
            lockedSkills.Add(skill);
        }

        Debug.Log($"Skills inicializadas para {className} — {lockedSkills.Count} skills bloqueadas.");
    }

    public void RegisterSkill(BaseSkill skill)
    {
        if (!activeSkills.Contains(skill))
            activeSkills.Add(skill);
    }

    public void UnlockRandomSkill()
    {
        if (lockedSkills.Count == 0)
        {
            Debug.Log("Todas as skills já foram desbloqueadas.");
            return;
        }

        int randomIndex          = Random.Range(0, lockedSkills.Count);
        BaseSkill skillToUnlock  = lockedSkills[randomIndex];

        skillToUnlock.enabled = true;
        activeSkills.Add(skillToUnlock);
        lockedSkills.RemoveAt(randomIndex);

        if (notification != null)
            notification.ShowNotification(skillToUnlock.SkillName);

        Debug.Log($"Skill desbloqueada: {skillToUnlock.SkillName}");
    }

    public PlayerAttributes GetAttributes() => playerAttributes;
}