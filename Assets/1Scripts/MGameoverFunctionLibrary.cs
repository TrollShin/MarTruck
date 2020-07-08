using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MGameoverFunctionLibrary : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI DisplayText;
    public float FadeDuration;
    public float TypeDelay;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0f;
        StartCoroutine(Fade(canvasGroup, 1f, FadeDuration));
        StartCoroutine(TypeText(DisplayText, TypeDelay));
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

        CSaveGame.GetInstance().ResetData();
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

    IEnumerator TypeText(TextMeshProUGUI Text, float typeDelay)
    {
        string contents = Text.text;
        Text.text = "";
        yield return new WaitForSecondsRealtime(FadeDuration);
        for (int i = 1; i <= contents.Length; i++)
        {
            Text.text = contents.Substring(0, i);
            yield return new WaitForSecondsRealtime(typeDelay);
        }
    }

    public void GoToTitle()
    {
        CSceneFunctionLibrary.LoadTitle();
    }
}
