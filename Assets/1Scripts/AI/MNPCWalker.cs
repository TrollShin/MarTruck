﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MNPCWalker : MonoBehaviour
{
    public delegate void NPCEvent(MNPCWalker _Walker);

    public event NPCEvent OnAgentArrive;

    private NavMeshAgent Agent;
    private WaitForSeconds CheckDelayTime;
    private Coroutine CheckCoroutine;

    private EAreaMask OriginalyAreaMask;

    private bool IsJayWalker;
    
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

    public void SetDestination(Transform _Transform, bool _IsJaywalker)
    {
        if(_IsJaywalker)
        {
            OriginalyAreaMask = EAreaMask.Walkable_CrossWalk;
            Agent.speed = 5.5f;
        }
        else
        {
            OriginalyAreaMask = EAreaMask.Walkable;
            Agent.speed = 3.5f;
        }

        IsJayWalker = _IsJaywalker;

        Agent.areaMask = (int)OriginalyAreaMask;

        Agent.SetDestination(_Transform.position);
    }

    //AreaMask에 대한 정보는 EAreaMask에서
    public void ChangeAreaMask(EAreaMask _AreaMask)
    {
        if (IsJayWalker) return;

        OriginalyAreaMask = _AreaMask;
        Agent.areaMask = (int)OriginalyAreaMask;

        Agent.SetDestination(Agent.destination);
    }

    //NPC가 목적지에 도착했는지 확인하는 코루틴
    IEnumerator CheckAgentArrive()
    {
        yield return CheckDelayTime;

        if(Vector3.Distance(transform.position, Agent.destination) < 3f)
        {
            OnAgentArrive?.Invoke(this);            
        }

        CheckCoroutine = StartCoroutine(CheckAgentArrive());
    }
}
