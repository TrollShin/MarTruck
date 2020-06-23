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

    private void ChangeNpcDestination(MNPCWalker Walker)
    {        
        if(Vector3.Distance(Walker.transform.position, DestinationArea.transform.position) > DestinationArea.Radius)
        {
            Walker.transform.position = GetDestination().position;
        }

        Walker.SetDestination(GetDestination(), (Random.Range(0,4)) < 1);
    }
}
