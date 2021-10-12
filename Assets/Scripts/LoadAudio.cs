using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class LoadAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [HideInInspector]
    public Coroutine audioCoroutine;
    AudioClip myClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    [System.Obsolete]
    public IEnumerator GetAudioClip(string url)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = myClip;
                audioSource.Play();
            }
        }
    }

    public void StopAudio()
    {
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);
        }
        
        audioSource.Stop();
    }
}
