using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNPCManager : MonoBehaviour
{
    [SerializeField]
    private MNPCArea DestinationArea;

    [SerializeField]
    private Transform ParentAgentObject;

    [SerializeField]
    private MTrafficLightSignal TrafficLightSignal;

    private MNPCWalker[] MNPCWalkers;

    private void Start()
    {
        DestinationArea.StartCoroutine();

        TrafficLightSignal.OnSignalChanged += OnSignalChange;

        MNPCWalkers = new MNPCWalker[ParentAgentObject.childCount];

        for (int i = 0; i < MNPCWalkers.Length; i++)
        {
            MNPCWalkers[i] = ParentAgentObject.GetChild(i).GetComponent<MNPCWalker>();
            MNPCWalkers[i].OnAgentArrive += ChangeNpcDestination;
        }
    }

    private void OnDestroy()
    {
        TrafficLightSignal.OnSignalChanged -= OnSignalChange;        
    }

    private Transform GetDestination()
    {
        Transform[] Destinaitons = DestinationArea.GetActivedDestinations();
        return Destinaitons[Random.Range(0, Destinaitons.Length)];
    }

    private void ChangeNpcDestination(MNPCWalker _Walker)
    {        
        //NPC가 NPCArea 밖에 있다면 안으로 가지고옴
        if(Vector3.Distance(_Walker.transform.position, DestinationArea.transform.position) > DestinationArea.Radius)
        {
            _Walker.transform.position = GetDestination().position;
        }

        _Walker.SetDestination(GetDestination(), (Random.Range(0,4)) < 1);
    }

    //신호는 차량기준
    private void OnSignalChange(ETrafficLightState _Signal)
    {
        if (_Signal == ETrafficLightState.Red)
        {
            ChangeAllAgentsAreaMask(EAreaMask.Walkable_CrossWalk);
        }
        else
        {
            ChangeAllAgentsAreaMask(EAreaMask.Walkable);
        }
    }

    //신호위반 하는 친구는 이 함수에서 변하지않음
    private void ChangeAllAgentsAreaMask(EAreaMask _AreaMask)
    {
        for(int i = 0; i < MNPCWalkers.Length; i++)
        {
            MNPCWalkers[i].ChangeAreaMask(_AreaMask);
        }
    }
}