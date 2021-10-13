using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private FadeUI logo;
    [SerializeField]
    private FadeUI floor;
    [SerializeField]
    private FadeUI firstPanel;
    [SerializeField]
    private FadeUI languagePanel;
    [SerializeField]
    private FadeUI mainUI;
    [SerializeField]
    private FadeUI settingsButton;
    [SerializeField]
    private FadeUI bg;
    [SerializeField]
    private FadeUI backButton;

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

    public void StartAR()
    {
        bg.HidePanel();
        logo.HidePanel();
        mainUI.HidePanel();
        floor.HidePanel();
        backButton.ShowPanel();
    }

    public void BackToMenu()
    {
        bg.ShowPanel();
        floor.ShowPanel();
        logo.ShowPanel();
        mainUI.ShowPanel();
        backButton.HidePanel();
    }
}
