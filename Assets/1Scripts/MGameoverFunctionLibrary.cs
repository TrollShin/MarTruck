using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MGameOverFunctionLibrary : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI DisplayText;
    public float FadeDuration;
    public float TypeDelay;

    private bool IsFaded = false;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0f;
        StartCoroutine(Fade(canvasGroup, 1f, FadeDuration));

        string content = DisplayText.text;
        DisplayText.text = "";
        StartCoroutine(InvokeFunction(()=>
        {
            IsFaded = true;
            CSaveGame.GetInstance().ResetData();
            StartCoroutine(TypeText(DisplayText, content, TypeDelay));
        }, FadeDuration));
    }

    IEnumerator InvokeFunction(Action action, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        action();
    }

    IEnumerator Fade(CanvasGroup canvasGroup, float resultAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while(elapsed <= duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = MapInRange(0f, duration, startAlpha, resultAlpha, elapsed);
            yield return new WaitForEndOfFrame();
        }
    }

    float MapInRange(float inA, float inB, float outA, float outB, float value)
    {
        float maxIn = Mathf.Max(inA, inB);
        float minIn = Mathf.Min(inA, inB);
        float maxOut = Mathf.Max(outA, outB);
        float minOut = Mathf.Min(outA, outB);
        float outOffset = maxOut - minOut;
        float ratio = (value - minIn) / (maxIn - minIn);
        return minOut + ratio * outOffset;
    }

    IEnumerator TypeText(TextMeshProUGUI Text, string content, float typeDelay)
    {
        for (int i = 1; i <= content.Length; i++)
        {
            Text.text = content.Substring(0, i);
            yield return new WaitForSecondsRealtime(typeDelay);
        }
    }

    public void GoToTitle()
    {
        if(IsFaded)
            CSceneFunctionLibrary.LoadTitle();
    }
}
