using UnityEngine;

public class SkillChest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerSkillController skillController = other.GetComponent<PlayerSkillController>();

        if (skillController != null)
            skillController.UnlockRandomSkill();

        Destroy(gameObject);
    }
}