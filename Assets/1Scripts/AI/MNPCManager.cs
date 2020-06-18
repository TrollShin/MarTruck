using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNPCManager : MonoBehaviour
{
    public MNPCArea DestinationArea;

    public Transform ParentAgentObject;

    private void Start()
    {
        DestinationArea.StartCoroutine();

        MNPCWalker Walker;

        for (int i = 0; i < ParentAgentObject.childCount; i++)
        {
            Walker = ParentAgentObject.GetChild(i).GetComponent<MNPCWalker>();
            Walker.OnAgentArrive += ChangeNpcDestination;
        }
    }

    private Transform GetDestination()
    {
        Transform[] Destinaitons = DestinationArea.GetActivedDestinations();
        return Destinaitons[Random.Range(0, Destinaitons.Length)];
    }

    private Transform ChangeNpcDestination(Transform NPC)
    {        
        if(Vector3.Distance(NPC.position, DestinationArea.transform.position) > DestinationArea.Radius)
        {
            NPC.position = GetDestination().position;
        }
             
        return GetDestination();
    }
}
