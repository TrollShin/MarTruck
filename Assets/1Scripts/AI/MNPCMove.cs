using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MNPCMove : MonoBehaviour
{
    private Transform Destination;
    
    private NavMeshAgent Agent;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        Destination = new GameObject().transform;
    }

    public void Init(Transform AgentPosition, Transform Destination)
    {
        transform.position = AgentPosition.position;
        this.Destination = Destination;

        Move();
    }

    public void Init(Vector3 AgentPosition, Vector3 Destination)
    {
        transform.position = AgentPosition;
        this.Destination.position = Destination;
    }

    private void Move()
    {
        Agent.SetDestination(Destination.position);
    }
}
