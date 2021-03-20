using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManagement : MonoBehaviour
{
    Animator animator;

    public enum States
    {
        Inactive,
        MainMenu,
        Inventory,
        Levels,
        Quest,
        Map,
        CollectionLog,
        Settings
    }

    States CurrentState;

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
                case States.Inactive:
                    Time.timeScale = 1;
                    animator.SetTrigger("Close");
                    break;
                case States.MainMenu:
                    Time.timeScale = 0;
                    animator.SetTrigger("Open");
                    break;
                default:
                    break;
            }
            CurrentState = value;
        }
    }


    void Start()
    {
        animator = GetComponent<Animator>();
        GameEvents.instance.changePlayerState += ChangePlayerState;
        GameEvents.instance.changePauseState += ChangePauseState;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == States.Inactive)
            {
                currentState = States.MainMenu;
            }
            else
            {
                currentState = States.Inactive;
            }
        }
    }

    private void ChangePauseState(States state)
    {
        this.currentState = state;
    }

    private void ChangePlayerState(PlayerController.States playerState)
    {
        switch (playerState)
        {
            case PlayerController.States.Normal:
                //this.currentState = States.Inactive;
                break;
        }
    }
}
