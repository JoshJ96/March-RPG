using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HoverText : MonoBehaviour
{
    bool active = true;
    Animator animator;
    public TextMeshProUGUI text;
    void Start()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<TextMeshProUGUI>();
        GameEvents.instance.hoverInteractable += HoverInteractable;
        GameEvents.instance.deHoverInteractable += DeHoverInteractable;
        GameEvents.instance.changePlayerState += ChangePlayerState;
    }

    private void ChangePlayerState(PlayerController.States state)
    {
        switch (state)
        {
            case PlayerController.States.Normal:
                active = true;
                break;
            case PlayerController.States.Paused:
                animator.SetTrigger("Close");
                active = false;
                break;
        }
    }

    private void DeHoverInteractable()
    {
        if (active)
        {
            animator.SetTrigger("Close");
        }
    }

    private void HoverInteractable(Interactable hovered)
    {
        if (active)
        {
            animator.SetTrigger("Open");
            text.text = hovered.hoverText;
        }
    }

    private void Update()
    {
        if (active)
        {
            transform.position = Input.mousePosition;
        }
    }
}
