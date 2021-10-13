using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private FadeUI firstPanel;
    [SerializeField]
    private FadeUI languagePanel;
    [SerializeField]
    private FadeUI mainUI;
    [SerializeField]
    private FadeUI settingsButton;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("language") == 1)
        {
            firstPanel.gameObject.SetActive(false);
            languagePanel.gameObject.SetActive(false);
            mainUI.ShowPanelWithoutAnimation();
            settingsButton.ShowPanelWithoutAnimation();
        }
    }

    public void HideLanguagePanel()
    {
        PlayerPrefs.SetInt("language", 1);
        languagePanel.HidePanel();
        firstPanel.HidePanel();
        mainUI.ShowPanel();
        settingsButton.ShowPanel();
    }
}
