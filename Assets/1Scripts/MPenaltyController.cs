using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPenaltyController : MonoBehaviour
{
    private bool IsCanPenalty = true;
    private WaitForSecondsRealtime DelayTime = new WaitForSecondsRealtime(3f);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Agent"))
        {
            if (IsCanPenalty)
            {
                StartCoroutine(GetPenalty());
                //StartCoroutine(CameraShake(.3f, 0.15f));
            }
        }
    }

    IEnumerator GetPenalty()
    {
        IsCanPenalty = false;

        MPopupUI.GetInstance().ShowPopup("교통사고\n-3");
        CUserInfo.GetInstance().Penalty(3);

        yield return DelayTime;

        IsCanPenalty = true;
    }

    /*IEnumerator CameraShake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-0.1f, 1f) * magnitude;
            float y = Random.Range(-0.1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }*/
}
