using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPenaltyController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Agent"))
        {
            CUserInfo.GetInstance().Money -= 3;
        }
    }
}
