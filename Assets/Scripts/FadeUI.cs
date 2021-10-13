using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FadeUI : MonoBehaviour
{
    CanvasGroup uiElement;

    public float endTime = 0.5f;

    void Awake()
    {
        uiElement = GetComponent<CanvasGroup>();
    }

    private void Start()
    {

    }

    public void ShowPanel()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, .5f));
        uiElement.blocksRaycasts = true;
    }

    public void ShowPanelWithoutAnimation()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, .01f));
        uiElement.blocksRaycasts = true;
    }

    public void HidePanel()
    {
        if(gameObject.activeInHierarchy)
            StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, endTime));
        uiElement.blocksRaycasts = false;
    }

    public void HideWithoutAnimation()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, 0.01f));
        uiElement.blocksRaycasts = false;
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();
        }

    }

    IEnumerator AutoFade()
    {
        yield return new WaitForSeconds(2f);
        HidePanel();
    }
}
