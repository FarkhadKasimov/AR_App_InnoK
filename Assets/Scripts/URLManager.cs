using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLManager : MonoBehaviour
{
    [SerializeField]
    private string facebook;
    [SerializeField]
    private string youtube;
    [SerializeField]
    private string instagram;

    public void LoadFB()
    {
        Application.OpenURL(facebook);
    }

    public void LoadInst()
    {
        Application.OpenURL(instagram);
    }

    public void LoadYouTube()
    {
        Application.OpenURL(youtube);
    }
}
