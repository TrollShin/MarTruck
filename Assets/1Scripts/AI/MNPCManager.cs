using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNPCManager : MonoBehaviour
{
    public Transform ParentDestination;

    private GameObject[] Destinations;

    private void Awake()
    {
        Destinations = new GameObject[ParentDestination.childCount];

        for(int i = 0; i < ParentDestination.childCount; i++)
        {
            Destinations[i] = ParentDestination.GetChild(i).gameObject;
        }
    }

    private void 
}
