using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour
{
    /*----------------------------
              Variables
    -----------------------------*/

    #region Interact State Machine

    public enum InteractStates
    {
        Unfocused,     //No activity, but active
        Inactive,      //Game paused
        Hovered,       //Mouse hovered
        Buildingfocus, //Mouse being held on interactable
        Focused        //Ready to focus towards object
    }

    public InteractStates InteractState;
    InteractStates interactState
    {
        get
        {
            return InteractState;
        }
        set
        {
            switch (value)
            {
                case InteractStates.Inactive:
                    GameEvents.instance.HideInteractableHoverText();
                    ChangeOutline(0.0f, Color.white);
                    break;
                case InteractStates.Unfocused:
                    GameEvents.instance.HideInteractableHoverText();
                    ChangeOutline(0.0f, Color.white);
                    break;
                case InteractStates.Hovered:
                    GameEvents.instance.ShowInteractableHoverText(this);
                    ChangeOutline(4.0f, Color.white);
                    break;
                case InteractStates.Buildingfocus:
                    GameEvents.instance.ShowInteractableHoverText(this);
                    ChangeOutline(4.0f, Color.white);
                    break;
                case InteractStates.Focused:
                    GameEvents.instance.ShowInteractableHoverText(this);
                    ChangeOutline(4.0f, Color.yellow);
                    break;
                default:
                    break;
            }
            InteractState = value;
        }
    }

    #endregion

    //Interactable object properties
    public string objectName;
    public string hoverText;
    Outline outline;

    /*----------------------------
                Start
    -----------------------------*/
    private void Start()
    {
        //Event subscriptions
        GameEvents.instance.changePlayerState += ChangePlayerState;
        GameEvents.instance.interactableClicked += InteractableClicked;
        GameEvents.instance.navClick += NavClick;

        //Outline component
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 0f;
    }

    /*----------------------------
            Mouse Actions
    -----------------------------*/
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        switch (interactState)
        {
            case InteractStates.Unfocused:
                interactState = InteractStates.Hovered;
                break;
            case InteractStates.Focused:
                GameEvents.instance.ShowInteractableHoverText(this);
                break;
        }
    }

    private void OnMouseExit()
    {
        switch (interactState)
        {
            case InteractStates.Hovered:
                interactState = InteractStates.Unfocused;
                break;
            case InteractStates.Focused:
                GameEvents.instance.HideInteractableHoverText();
                break;
        }
    }

    private void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (interactState != InteractStates.Inactive)
        {
            GameEvents.instance.InteractableClicked(this);
            interactState = InteractStates.Focused;
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //Nothing here yet
    }

    /*----------------------------
                 GFX
    -----------------------------*/
    void ChangeOutline(float width, Color color)
    {
        outline.OutlineWidth = width;
        outline.OutlineColor = color;
    }

    /*----------------------------
            Event Responses
    -----------------------------*/
    private void InteractableClicked(Interactable obj)
    {
        //De-focus this interactable if a different one was clicked
        if (obj != this)
        {
            interactState = InteractStates.Unfocused;
        }
    }

    private void ChangePlayerState(PlayerController.States state)
    {
        //Become unfocused if the game is paused
        switch (state)
        {
            case PlayerController.States.Normal:
                interactState = InteractStates.Unfocused;
                break;
        }
    }

    private void NavClick(Vector3 point)
    {
        //Disable the interactable if user clicks away
        interactState = InteractStates.Unfocused;
    }
}
