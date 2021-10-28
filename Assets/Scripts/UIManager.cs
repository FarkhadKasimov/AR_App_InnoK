using System.Collections;
using System.Collections.Generic;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("FadeUI Elements")]
    [SerializeField]
    private FadeUI logo;
    [SerializeField]
    private FadeUI floor;
    [SerializeField]
    private FadeUI firstPanel;
    [SerializeField]
    private FadeUI languagePanel;
    public FadeUI mainUI;
    [SerializeField]
    private FadeUI settingsButton;
    [SerializeField]
    private FadeUI bg;
    [SerializeField]
    private FadeUI backButton;
    [SerializeField]
    private FadeUI settings;
    [SerializeField]
    private FadeUI screenshotButton;
    [SerializeField]
    private FadeUI audioButton;
    public FadeUI textPanel;

    [Header("UI Elements")]
    [SerializeField]
    private Text languageText;
    [SerializeField]
    private Image audioImage;
    [SerializeField]
    private Sprite audioOn;
    [SerializeField]
    private Sprite audioOff;

    [Header("Other components")]
    [SerializeField]
    private LeanLocalization leanLocalization;

    private void Start()
    {
        if (PlayerPrefs.GetInt("language") == 1)
        {
            firstPanel.gameObject.SetActive(false);
            languagePanel.gameObject.SetActive(false);
            mainUI.ShowPanelWithoutAnimation();
            settingsButton.ShowPanelWithoutAnimation();
        }
        languageText.text = leanLocalization.CurrentLanguage;
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
        screenshotButton.ShowPanel();
        audioButton.ShowPanel();
    }

    public void BackToMenu()
    {
        bg.ShowPanel();
        floor.ShowPanel();
        logo.ShowPanel();
        mainUI.ShowPanel();
        backButton.HidePanel();
        settings.HidePanel();
        screenshotButton.HidePanel();
        audioButton.HidePanel();
    }

    public void OpenSettings()
    {
        bg.ShowPanel();
        floor.ShowPanel();
        logo.ShowPanel();
        settings.ShowPanel();
        backButton.ShowPanel();
        mainUI.HidePanel();
        screenshotButton.HidePanel();
        audioButton.HidePanel();
    }

    public void ChangeLanguage()
    {
        switch (leanLocalization.CurrentLanguage)
        {
            case "Russian":
                leanLocalization.SetCurrentLanguage("English");
                languageText.text = "English";
                break;
            case "English":
                leanLocalization.SetCurrentLanguage("Uzbek");
                languageText.text = "O‘zbekcha";
                break;
            case "Uzbek":
                leanLocalization.SetCurrentLanguage("Russian");
                languageText.text = "Ðóññêèé";
                break;
        }
    }

    public void AudioSwitcher()
    {
        switch (AudioListener.volume)
        {
            case 0:
                AudioListener.volume = 1;
                audioImage.sprite = audioOn;
                break;
            case 1:
                AudioListener.volume = 0;
                audioImage.sprite = audioOff;
                break;
        }
    }
}
