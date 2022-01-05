using Lean.Localization;
using System;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CustomObserverEventHandler : DefaultObserverEventHandler
{
    public SimpleCloudRecoEventHandler simpleCloudRecoEventHandler;
    [SerializeField]
    private API api;
    [SerializeField]
    private LeanLocalization leanLocalization;
    [SerializeField]
    private ContentController contentController;
    [SerializeField]
    private LoadAudio loadAudio;
    [SerializeField]
    private string text;
    [SerializeField]
    private Text textUI;
    [SerializeField]
    private UIManager uiManager;

    public static bool downloaded;

    protected override void OnTrackingFound()
    {
        Debug.Log("Target Found");

        if (!downloaded)
        {
            downloaded = true;

            if (!String.IsNullOrEmpty(simpleCloudRecoEventHandler.parameter.model))
            {
                contentController.LoadContent(simpleCloudRecoEventHandler.parameter.model, simpleCloudRecoEventHandler.parameter.scale, simpleCloudRecoEventHandler.parameter.rotation, simpleCloudRecoEventHandler.parameter.url);
            }

            if (simpleCloudRecoEventHandler.parameter.audio)
            {
                switch (leanLocalization.CurrentLanguage)
                {
                    case "Russian":
                        loadAudio.audioCoroutine = StartCoroutine(loadAudio.GetAudioClip(simpleCloudRecoEventHandler.parameter.audio_ru));
                        break;
                    case "English":
                        loadAudio.audioCoroutine = StartCoroutine(loadAudio.GetAudioClip(simpleCloudRecoEventHandler.parameter.audio_en));
                        break;
                    case "Uzbek":
                        loadAudio.audioCoroutine = StartCoroutine(loadAudio.GetAudioClip(simpleCloudRecoEventHandler.parameter.audio_uz));
                        break;
                }
            }

            if (simpleCloudRecoEventHandler.parameter.text)
            {
                switch (leanLocalization.CurrentLanguage)
                {
                    case "Russian":
                        text = simpleCloudRecoEventHandler.parameter.text_ru;
                        textUI.text = text;
                        break;
                    case "English":
                        text = simpleCloudRecoEventHandler.parameter.text_en;
                        textUI.text = text;
                        break;
                    case "Uzbek":
                        text = simpleCloudRecoEventHandler.parameter.text_uz;
                        textUI.text = text;
                        break;
                }
            }

            if (simpleCloudRecoEventHandler.parameter.text)
            {
                uiManager.textPanel.ShowPanel();
            }
        }



        base.OnTrackingFound();
    }

    protected override void OnTrackingLost()
    {
        Debug.Log("Target Lost");

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        uiManager.textPanel.HidePanel();
        loadAudio.StopAudio();
        simpleCloudRecoEventHandler.mCloudRecoBehaviour.enabled = true;
        downloaded = false;
        api.StopDownloading();
        base.OnTrackingLost();
    }
}