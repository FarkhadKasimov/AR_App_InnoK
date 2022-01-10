using System;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
using Vuforia;

public class SimpleCloudRecoEventHandler : MonoBehaviour
{
    [HideInInspector]
    public CloudRecoBehaviour mCloudRecoBehaviour;
    public bool mIsScanning = false;
    string mTargetMetadata = "";

    public Parametrs parameter;
    public ImageTargetBehaviour ImageTargetTemplate;
    

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
            mCloudRecoBehaviour.ClearTrackables(false);
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

        Debug.Log($"Metadata: {mTargetMetadata}");

        //if (ImageTargetTemplate)
        //{
            /* Enable the new result with the same ImageTargetBehaviour: */
            mCloudRecoBehaviour.EnableTracking(cloudRecoSearchResult, ImageTargetTemplate.gameObject);
        //}

        parameter = JsonUtility.FromJson<Parametrs>(mTargetMetadata);

    }
}

[Serializable]
public class Parametrs
{
    public string model;
    public float scale;
    public float rotation;

    public bool audio;
    public string audio_en;
    public string audio_ru;
    public string audio_uz;

    public bool text;
    public string text_en;
    public string text_ru;
    public string text_uz;

    public string url;
}