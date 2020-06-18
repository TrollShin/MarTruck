using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MNPCWalker : MonoBehaviour
{
    public delegate Transform NPCEvent(Transform NPC);

    public event NPCEvent OnAgentArrive;

    private NavMeshAgent Agent;
    private WaitForSeconds CheckDelayTime;
    private Coroutine CheckCoroutine;
    

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        CheckDelayTime = new WaitForSeconds(2.5f);

        CheckCoroutine = StartCoroutine(CheckAgentArrive());
    }

    public void SetDestination(Transform _Transform)
    {
        Debug.Log(_Transform.position);
        Agent.SetDestination(_Transform.position);
    }

    IEnumerator CheckAgentArrive()
    {
        yield return CheckDelayTime;

        if(Vector3.Distance(transform.position, Agent.destination) < 3)
        {
            SetDestination(OnAgentArrive?.Invoke(transform));
        }

        CheckCoroutine = StartCoroutine(CheckAgentArrive());
    }
}
