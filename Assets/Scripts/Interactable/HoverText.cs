using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HoverText : MonoBehaviour
{

    /*----------------------------
                Variables
    -----------------------------*/
    TextMeshProUGUI info;

    /*----------------------------
                Start
    -----------------------------*/
    void Start()
    {
        info = GetComponent<TextMeshProUGUI>();
        GameEvents.instance.showInteractableHoverText += ShowInteractableHoverText;
        GameEvents.instance.hideInteractableHoverText += HideInteractableHoverText;
    }

    /*----------------------------
                Update
    -----------------------------*/
    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    /*----------------------------
            Event Responses
    -----------------------------*/
    private void HideInteractableHoverText()
    {
        transform.localScale = Vector3.zero;
    }

    private void ShowInteractableHoverText(Interactable hovered)
    {
        transform.localScale = Vector3.one;
        info.text = hovered.hoverText;
    }
}
