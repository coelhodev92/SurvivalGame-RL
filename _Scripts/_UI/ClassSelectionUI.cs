using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ClassSelectionUI : MonoBehaviour
{
    [Header("Referências")]
    public GameObject panel;
    public List<Button> classButtons;
    public List<TextMeshProUGUI> classNameTexts;
    public List<TextMeshProUGUI> classDescriptionTexts;
    public TextMeshProUGUI selectedClassText;

    [Header("Classes Disponíveis")]
    public List<ClassData> availableClasses = new List<ClassData>();

    private void Awake()
    {
        panel.SetActive(false);
        BuildUI();
        UpdateSelectedClassText();
    }

    public void Show()
    {
        UpdateSelectedClassText();
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Hide()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void BuildUI()
    {
        for (int i = 0; i < classButtons.Count; i++)
        {
            if (i >= availableClasses.Count)
            {
                classButtons[i].gameObject.SetActive(false);
                continue;
            }

            ClassData classData = availableClasses[i];
            int capturedIndex   = i;

            classNameTexts[i].text        = classData.className;
            classDescriptionTexts[i].text = classData.classDescription;

            // Cor do botão reflete a classe
            classButtons[i].GetComponent<Image>().color = classData.classColor;
            classButtons[i].onClick.RemoveAllListeners();
            classButtons[i].onClick.AddListener(() => SelectClass(capturedIndex));
        }
    }

    private void SelectClass(int index)
    {
        ClassData chosen = availableClasses[index];

        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.GetData().selectedClassName = chosen.className;
            SaveManager.Instance.Save();
        }

        UpdateSelectedClassText();
        Debug.Log($"Classe selecionada: {chosen.className}");

        Hide();
    }

    private void UpdateSelectedClassText()
    {
        if (selectedClassText == null) return;

        string current = SaveManager.Instance != null
            ? SaveManager.Instance.GetData().selectedClassName
            : "";

        selectedClassText.text = string.IsNullOrEmpty(current)
            ? "Nenhuma classe selecionada"
            : $"Classe atual: {current}";
    }
}