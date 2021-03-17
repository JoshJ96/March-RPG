using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    bool showOutline = true;
    public string objectName;
    public string hoverText;
    Outline outline;
    private void Awake()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 0f;
    }

    private void Start()
    {
        GameEvents.instance.changePlayerState += ChangePlayerState;
    }

    private void ChangePlayerState(PlayerController.States state)
    {
        switch (state)
        {
            case PlayerController.States.Normal:
                showOutline = true;
                break;
            case PlayerController.States.Paused:
                HideOutline();
                showOutline = false;
                break;
        }
    }

    private void OnMouseEnter()
    {
        if (showOutline)
        {
            ShowOutline();
            GameEvents.instance.HoverInteractable(this);
        }
    }
    private void OnMouseExit()
    {
        if (showOutline)
        {
            HideOutline();
            GameEvents.instance.DeHoverInteractable();
        }
    }

    void ShowOutline()
    {
        outline.OutlineWidth = 4f;
    }

    void HideOutline()
    {
        outline.OutlineWidth = 0f;
    }
}
