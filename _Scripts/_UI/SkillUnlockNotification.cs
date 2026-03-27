using UnityEngine;
using TMPro;
using System.Collections;

public class SkillUnlockNotification : MonoBehaviour
{
    [Header("Referências")]
    public TextMeshProUGUI notificationText;

    [Header("Configurações")]
    public float displayDuration = 3f;
    public float fadeDuration = 0.5f;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        SetAlpha(0f);
        notificationText.text = "";
    }

    public void ShowNotification(string skillName)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(DisplayRoutine(skillName));
    }

    private IEnumerator DisplayRoutine(string skillName)
    {
        notificationText.text = $"Habilidade obtida:\n<b>{skillName}</b>";
        SetAlpha(1f);

        yield return new WaitForSeconds(displayDuration);

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            SetAlpha(1f - (timer / fadeDuration));
            yield return null;
        }

        SetAlpha(0f);
        notificationText.text = "";
        currentCoroutine = null;
    }

    private void SetAlpha(float alpha)
    {
        Color c = notificationText.color;
        notificationText.color = new Color(c.r, c.g, c.b, alpha);
    }
}