using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevToolsManager : MonoBehaviour
{
    public Animator animator;

    #region Dev Tools State Machine

    //State definition
    public enum State
    {
        Inactive,
        Active
    }

    //Back-end value
    State CurrentState;

    //Set manager
    State currentState
    {
        get
        {
            return CurrentState;
        }
        set
        {
            CurrentState = value;
            switch (value)
            {
                case State.Active:
                    animator.SetTrigger("Open");
                    break;
                case State.Inactive:
                    animator.SetTrigger("Close");
                    break;
                default:
                    break;
            }
        }
    }
    #endregion

    private void Start()
    {
        GameEvents.instance.devToolsButtonClick += DevToolsButtonClick;
    }

    private void DevToolsButtonClick()
    {
        switch (currentState)
        {
            case State.Active:
                currentState = State.Inactive;
                break;
            case State.Inactive:
                currentState = State.Active;
                break;
            default:
                break;
        }
    }
}