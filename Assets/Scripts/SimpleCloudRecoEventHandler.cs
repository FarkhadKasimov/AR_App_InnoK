using System;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class SimpleCloudRecoEventHandler : MonoBehaviour
{
    CloudRecoBehaviour mCloudRecoBehaviour;
    bool mIsScanning = false;
    string mTargetMetadata = "";

    public ImageTargetBehaviour ImageTargetTemplate;
    [SerializeField]
    private ContentController contentController;
    [SerializeField]
    private LoadAudio loadAudio;
    [SerializeField]
    private string text;
    [SerializeField]
    private Text textUI;

    [Obsolete]
    // Register cloud reco callbacks
    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
    [Obsolete]
    //Unregister cloud reco callbacks when the handler is destroyed
    void OnDestroy()
    {
        mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }

    public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
        Debug.Log("Cloud Reco initialized");
    }

    public void OnInitError(CloudRecoBehaviour.InitError initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }

    public void OnUpdateError(CloudRecoBehaviour.QueryError updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());

    }

    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;

        if (scanning)
        {
            // Clear all known targets
        }
    }

    // Here we handle a cloud target recognition event
    [Obsolete]
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
    {
        // Store the target metadata
        mTargetMetadata = cloudRecoSearchResult.MetaData;

        // Stop the scanning by disabling the behaviour
        mCloudRecoBehaviour.enabled = false;

        if (ImageTargetTemplate)
        {
            /* Enable the new result with the same ImageTargetBehaviour: */
            mCloudRecoBehaviour.EnableTracking(cloudRecoSearchResult, ImageTargetTemplate.gameObject);
        }

        Parametrs parameter = new Parametrs();
        parameter = JsonUtility.FromJson<Parametrs>(mTargetMetadata);

        if (!String.IsNullOrEmpty(parameter.model))
        {
            contentController.LoadContent(parameter.model);
        }

        if (!String.IsNullOrEmpty(parameter.audio))
        {
            StartCoroutine(loadAudio.GetAudioClip(parameter.audio));
        }

        if (!String.IsNullOrEmpty(parameter.text))
        {
            text = parameter.text;
            textUI.text = text;
        }
    }
}

[Serializable]
public class Parametrs
{
    public string model;
    public string audio;
    public string text;
}