using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HoverText : MonoBehaviour
{
    Animator animator;
    public TextMeshProUGUI text;
    void Start()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<TextMeshProUGUI>();
        GameEvents.instance.hoverInteractable += HoverInteractable;
        GameEvents.instance.deHoverInteractable += DeHoverInteractable;
    }

    private void DeHoverInteractable()
    {
        animator.SetTrigger("Close");
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    private void HoverInteractable(Interactable hovered)
    {
        animator.SetTrigger("Open");
        text.text = hovered.hoverText;
    }
}
