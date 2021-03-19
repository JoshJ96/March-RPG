using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    /*----------------------------
          Variables
    -----------------------------*/

    #region Player State Machine

    public enum States
    {
        Normal,
        Paused,
        FocusedInteractable
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
            switch (value)
            {
                case States.Normal:
                    break;
                case States.Paused:
                    break;
                case States.FocusedInteractable:
                    break;
                default:
                    break;
            }
            GameEvents.instance.ChangePlayerState(value);
            CurrentState = value;
        }
    }

    #endregion

    NavMeshAgent agent;
    Interactable Focus;
    Interactable focus
    {
        get
        {
            return Focus;
        }
        set
        {
            if (value != null)
            {
                agent.SetDestination(value.transform.position);
            }
            Focus = value;
        }
    }

    /*----------------------------
                Start
    -----------------------------*/
    private void Start()
    {
        //todo: set spawn from room transfer/save loading
        //transform.position = GameDataManager.instance.GetSavedPlayerPosition();

        //Event subscriptions
        GameEvents.instance.navClick += NavClick;
        GameEvents.instance.interactableClicked += InteractableClicked;

        //Components
        agent = GetComponent<NavMeshAgent>();
    }

    /*----------------------------
            Update
    -----------------------------*/
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
            case States.FocusedInteractable:
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

    /*----------------------------
            Event Responses
    -----------------------------*/
    private void NavClick(Vector3 point)
    {
        switch (currentState)
        {
            case States.Normal:
                agent.SetDestination(point);
                focus = null;
                break;
            case States.FocusedInteractable:
                agent.SetDestination(point);
                focus = null;
                break;
        }
    }

    private void InteractableClicked(Interactable obj)
    {
        switch (currentState)
        {
            case States.Normal:
                focus = obj;
                break;
            case States.FocusedInteractable:
                focus = obj;
                break;
        }
    }
}