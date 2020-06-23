using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MNPCWalker : MonoBehaviour
{
    public delegate void NPCEvent(MNPCWalker Walker);

    public event NPCEvent OnAgentArrive;

    private NavMeshAgent Agent;
    private WaitForSeconds CheckDelayTime;
    private Coroutine CheckCoroutine;

    private EAreaMask OriginalyAreaMask;
    
    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        OriginalyAreaMask = EAreaMask.Walkable;

        CheckDelayTime = new WaitForSeconds(2.5f);

        CheckCoroutine = StartCoroutine(CheckAgentArrive());
    }

    public void SetDestination(Transform _Transform)
    {
        Agent.areaMask = (int)OriginalyAreaMask;

        Agent.SetDestination(_Transform.position);
    }

    public void SetDestination(Transform _Transform, bool isJaywalker)
    {
        if(isJaywalker)
        {
            OriginalyAreaMask = EAreaMask.JayWalker;
            Agent.speed = 5.5f;
        }
        else
        {
            OriginalyAreaMask = EAreaMask.Walkable;
            Agent.speed = 3.5f;
        }

        Agent.areaMask = (int)OriginalyAreaMask;

        Agent.SetDestination(_Transform.position);
    }

    IEnumerator CheckAgentArrive()
    {
        yield return CheckDelayTime;

        if(Vector3.Distance(transform.position, Agent.destination) < 3f)
        {
            Debug.Log(Vector3.Distance(transform.position, Agent.destination).ToString());

            OnAgentArrive?.Invoke(this);            
        }

        CheckCoroutine = StartCoroutine(CheckAgentArrive());
    }
}
