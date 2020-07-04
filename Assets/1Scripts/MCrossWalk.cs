using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MCrossWalk : MonoBehaviour
{
    //public MPlayerInfo PlayerInfo;
    public MTrafficLightSignal TraffickLight;

    [Header("Penalty")]
    [SerializeField]
    [Tooltip("벌금")]
    private int Penalty;
    //public delegate void CrossWalkEvent();
    //public event CrossWalkEvent OnPassCrossWalk;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == nameof(ETag.Player))
        {
            if (TraffickLight.GetSignal() == ETrafficLightState.Red)
            {
                CUserInfo.GetInstance().Money -= Penalty;
            }
            //OnPassCrossWalk?.Invoke();
        }
    }
}
