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
}
