using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public enum States
    {
        Normal,
        Paused
    }

    States CurrentState = States.Normal;

    public States currentState
    {
        get
        {
            return CurrentState;
        }
        private set
        {
            GameEvents.instance.ChangePlayerState(value);
            CurrentState = value;
        }
    }

    NavMeshAgent agent;
    private void Start()
    {
        //transform.position = GameDataManager.instance.GetSavedPlayerPosition();
        GameEvents.instance.navClick += NavClick;
        agent = GetComponent<NavMeshAgent>();
    }

    private void NavClick(Vector3 point)
    {
        switch (CurrentState)
        {
            case States.Normal:
                agent.SetDestination(point);
                break;
            case States.Paused:
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case States.Normal:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    currentState = States.Paused;
                }
                break;
            case States.Paused:
                agent.SetDestination(transform.position);
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    currentState = States.Normal;
                }
                break;
            default:
                break;
        }
    }
}
