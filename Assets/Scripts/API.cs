using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class API : MonoBehaviour
{
#if UNITY_ANDROID
    const string BundleFolder = "http://ar.innocenter.uz/Android/";
#else
    const string BundleFolder = "http://ar.innocenter.uz/iOS/";
#endif

    [HideInInspector]
    public Coroutine coroutine;

    [System.Obsolete]
    public void GetBundleObject(string assetName, UnityAction<GameObject> callback, Transform bundleParent, float scale, float rotation, string url)
    {
        coroutine = StartCoroutine(GetDisplayBundleRoutine(assetName, callback, bundleParent, scale, rotation, url));
    }

    public void StopDownloading()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    [System.Obsolete]
    IEnumerator GetDisplayBundleRoutine(string assetName, UnityAction<GameObject> callback, Transform bundleParent, float scale, float rotation, string url)
    {
        if (!string.IsNullOrEmpty(assetName))
        {
            string bundleURL = "";
            string assetN = assetName.Replace(" ", "-");

            if (url != null)
            {
                bundleURL = url + assetN + "-";

                Debug.Log(bundleURL);
            }
            else
            {
                bundleURL = BundleFolder + assetN + "-";
                Debug.Log("Url is empty");
            }

            //append platform to asset bundle name
#if UNITY_ANDROID
            bundleURL += "Android";
#else
        bundleURL += "IOS";
#endif

            Debug.Log("Requesting bundle at " + bundleURL);

            //request asset bundle
            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log("Network error");
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                if (bundle != null)
                {
                    string rootAssetPath = bundle.GetAllAssetNames()[0];
                    GameObject arObject = Instantiate(bundle.LoadAsset(rootAssetPath) as GameObject, bundleParent);
                    if (scale != 0)
                        arObject.transform.localScale = new Vector3(scale, scale, scale);
                    if (rotation != 0)
                        arObject.transform.localRotation = Quaternion.Euler(0, rotation, 0);
                    bundle.Unload(false);
                    callback(arObject);
                }
                else
                {
                    Debug.Log("Not a valid asset bundle");
                }
            }
        }
        else
        {
            Debug.Log("Asset name is empty");
        }

        
    }
}
