using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;
    private void Start()
    {
        GameEvents.instance.navClick += NavClick;
        agent = GetComponent<NavMeshAgent>();
    }

    private void NavClick(Vector3 point)
    {
        agent.SetDestination(point);
    }
}
