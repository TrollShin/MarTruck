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
        Agents = new MNPCWalker[ParentAgents.childCount];
        Destinations = new Transform[ParentDestination.childCount];

        for (int i = 0; i < ParentDestination.childCount; i++)
        {
            Destinations[i] = ParentDestination.GetChild(i);
        }

        for (int i = 0; i < ParentAgents.childCount; i++)
        {
            Agents[i] = ParentAgents.GetChild(i).GetComponent<MNPCWalker>();            
        }

        Debug.Log("Length : " + Destinations.Length);

        for (int i = 0; i < Agents.Length; i++)
        {
            int idx = Random.Range(0, Destinations.Length);
            Debug.Log(Destinations[i].position + "   " + Agents[i].transform.position);

            Agents[i].Init(Agents[i].transform.position, Destinations[idx].position);

            Agents[i].Move();
        }
    }
}
