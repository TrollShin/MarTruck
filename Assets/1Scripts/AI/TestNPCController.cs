using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNPCController : MonoBehaviour
{
    public Transform ParentDestination;

    public Transform ParentAgents;

    private MNPCWalker[] Agents;
    private Transform[] Destinations;

    // Start is called before the first frame update
    void Start()
    {

    }
}
