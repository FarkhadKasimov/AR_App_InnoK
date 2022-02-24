using System;
using UnityEngine;

public class ContentController : MonoBehaviour
{

    public API api;
    [SerializeField]
    private Transform imageTraget;

    private void Start()
    {

    }

    [Obsolete]
    public void LoadContent(string name, float scale, float rotation, string url)
    {
        DestroyAllChildren();
        api.GetBundleObject(name, OnContentLoaded, imageTraget, scale, rotation, url);
    }

    void OnContentLoaded(GameObject content)
    {
        //do something cool here
        Debug.Log("Loaded: " + content.name);
    }

    void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}