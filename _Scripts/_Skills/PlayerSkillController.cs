using UnityEngine;
using System.Collections.Generic;

public class PlayerSkillController : MonoBehaviour
{
    private List<BaseSkill> activeSkills = new List<BaseSkill>();
    private List<BaseSkill> lockedSkills = new List<BaseSkill>();
    private PlayerAttributes playerAttributes;
    private SkillUnlockNotification notification;

    private void Awake()
    {
        playerAttributes = GetComponent<PlayerAttributes>();
        notification = FindAnyObjectByType<SkillUnlockNotification>();

        BaseSkill[] allSkills = GetComponents<BaseSkill>();
        foreach (BaseSkill skill in allSkills)
        {
            if (skill.enabled)
                activeSkills.Add(skill);
            else
                lockedSkills.Add(skill);
        }
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

        int randomIndex = Random.Range(0, lockedSkills.Count);
        BaseSkill skillToUnlock = lockedSkills[randomIndex];

        skillToUnlock.enabled = true;
        activeSkills.Add(skillToUnlock);
        lockedSkills.RemoveAt(randomIndex);

        // Mostra a notificação com o nome legível da skill
        if (notification != null)
            notification.ShowNotification(skillToUnlock.SkillName);

        Debug.Log($"Skill desbloqueada: {skillToUnlock.GetType().Name}");
    }

    public PlayerAttributes GetAttributes() => playerAttributes;
}