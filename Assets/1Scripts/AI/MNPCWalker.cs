using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MNPCWalker : MonoBehaviour
{
    private Vector3 Destination;

    private NavMeshAgent Agent;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Debug.Log(Agent);

        Destination = new Vector3();  
    }

    public void Init(Transform AgentPosition, Transform Destination)
    {
        transform.position = AgentPosition.position;
        this.Destination = Destination.position;
    }

    public void Init(Vector3 AgentPosition, Vector3 Destination)
    {
        transform.position = AgentPosition;
        this.Destination = Destination;
    }

    public void SetDestination(Vector3 Destination)
    {
        this.Destination = Destination;
    }

    public void Move()
    {
        Agent.SetDestination(Destination);
    }
}
