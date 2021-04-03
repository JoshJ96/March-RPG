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
    //Definitions
    public enum InteractStates
    {
        Unfocused,
        Hovered,
        Focused
    }

    //Fields
    private InteractStates interactState;

    //Properties
    public InteractStates InteractState
    {
        get
        {
            return interactState;
        }
        set
        {
            switch (value)
            {
                case InteractStates.Unfocused:
                    GameEvents.instance.HideHoverText();
                    ChangeOutline(0.0f, Color.white);
                    break;
                case InteractStates.Hovered:
                    GameEvents.instance.ShowInteractableHoverText(this);
                    ChangeOutline(3.0f, Color.white);
                    break;
                case InteractStates.Focused:
                    GameEvents.instance.ShowInteractableHoverText(this);
                    ChangeOutline(3.0f, Color.yellow);
                    break;
                default:
                    break;
            }
            interactState = value;
        }
    }
    public List<Vector3> InteractPoints => GetInteractablePoints();
    public string objectName;
    public string hoverText;
    public Transform interactionTransform;
    public float interactionRadius;

    //Unity Components
    Outline outline;

    bool CanDisplay = true;
    public bool canDisplay
    {
        get 
        {
            return CanDisplay;
        }
        set
        {
            CanDisplay = value;
            if (value == false)
            {
                ChangeOutline(0.0f, Color.white);
            }
        }
    }


    private void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            canDisplay = false;
        }
        else
        {
            canDisplay = true;
        }
    }

    /*----------------------------
                Start
    -----------------------------*/
    private void Start()
    {
        //Event subscriptions
        GameEvents.instance.changePlayerState += ChangePlayerState;
        GameEvents.instance.interactableClicked += InteractableClicked;
        GameEvents.instance.navClick += NavClick;
        GameEvents.instance.interactableDefocused += InteractableDefocused;

        //Outline component
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 0f;
    }

    /*----------------------------
             Mouse Events
    -----------------------------*/
    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            switch (InteractState)
            {
                case InteractStates.Unfocused:
                    InteractState = InteractStates.Hovered;
                    break;
            }

            GameEvents.instance.ShowInteractableHoverText(this);
        }
    }

    private void OnMouseExit()
    {
        GameEvents.instance.HideHoverText();

        switch (InteractState)
        {
            case InteractStates.Hovered:
                InteractState = InteractStates.Unfocused;
                break;
        }
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
                    GameEvents.instance.InteractableClicked(this);
        InteractState = InteractStates.Focused;
        }

    }

    /*----------------------------
                Methods
    -----------------------------*/
    public virtual void Interact()
    {
        print($"Interacting with {this.objectName}.");
    }

    public List<Vector3> GetInteractablePoints()
    {
        float x = interactionTransform.position.x;
        float y = interactionTransform.position.y;
        float z = interactionTransform.position.z;
        float n = interactionRadius;
        return new List<Vector3>
        {        
            //Right
            new Vector3(x + n, y, z),
            //Left
            new Vector3(x - n, y, z),
            //Up
            new Vector3(x, y, z + n),
            //Down
            new Vector3(x, y, z - n),
            //UpRight
            new Vector3(x + n, y, z + n),
            //UpLeft        
            new Vector3(x - n, y, z + n),
            //DownRight     
            new Vector3(x + n, y, z - n),
            //DownLeft      
            new Vector3(x - n, y, z - n)
        };
    }

    /*----------------------------
             Game Events
    -----------------------------*/
    private void InteractableClicked(Interactable obj)
    {
        //De-focus this interactable if a different one was clicked
        if (obj != this)
        {
            InteractState = InteractStates.Unfocused;
        }
    }

    private void ChangePlayerState(PlayerController.States state)
    {
        //Become unfocused if the game is paused
        switch (state)
        {
            case PlayerController.States.Normal:
                InteractState = InteractStates.Unfocused;
                break;
        }
    }

    private void NavClick(Vector3 point)
    {
        //Disable the interactable if user clicks away
        InteractState = InteractStates.Unfocused;
    }

    private void InteractableDefocused()
    {
        InteractState = InteractStates.Unfocused;
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
                Gizmos
    -----------------------------*/
    private void OnDrawGizmos()
    {
        if (interactionTransform != null)
        {
            Gizmos.DrawWireSphere(interactionTransform.position, interactionRadius);
        }

        foreach (var item in InteractPoints)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(item, Vector3.one);
        }
    }
}
