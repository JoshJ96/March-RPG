using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HoverText : MonoBehaviour
{

    #region Hover State Machine

    public enum States
    {
        WaitingForHover,
        Inactive,
        Hover
    }

    public States HoverState;
    States hoverState
    {
        get
        {
            return HoverState;
        }
        set
        {
            switch (value)
            {
                case States.WaitingForHover:
                    transform.localScale = Vector3.zero;
                    break;
                case States.Inactive:
                    transform.localScale = Vector3.zero;
                    break;
                case States.Hover:
                    text.text = hoverText;
                    transform.localScale = Vector3.one;
                    break;
                default:
                    break;
            }

            HoverState = value;
        }
    }
    #endregion

    TextMeshProUGUI text;
    string hoverText;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        GameEvents.instance.showInteractableHoverText += ShowInteractableHoverText;
        GameEvents.instance.hideInteractableHoverText += HideInteractableHoverText;
        GameEvents.instance.changePlayerState += ChangePlayerState;
    }

    private void ChangePlayerState(PlayerController.States state)
    {
        switch (state)
        {
            case PlayerController.States.Normal:
                hoverState = States.WaitingForHover;
                break;
        }
    }

    private void HideInteractableHoverText()
    {
        hoverState = States.WaitingForHover;
    }

    private void ShowInteractableHoverText(Interactable hovered)
    {
        switch (hoverState)
        {
            case States.WaitingForHover:
                hoverText = hovered.hoverText;
                hoverState = States.Hover;
                break;
        }
    }

    private void Update()
    {
        switch (hoverState)
        {
            case States.Hover:
                transform.position = Input.mousePosition;
                break;
        }
    }
}
