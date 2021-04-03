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
    Vector3 offset = Vector3.zero;

    /*----------------------------
                Start
    -----------------------------*/
    void Start()
    {
        info = GetComponent<TextMeshProUGUI>();
        GameEvents.instance.showInteractableHoverText += ShowInteractableHoverText;
        GameEvents.instance.hideHoverText += HideHoverText;
        GameEvents.instance.showInventorySlotHoverText += ShowInventorySlotHoverText;
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
    private void HideHoverText()
    {
        transform.localScale = Vector3.zero;
    }

    private void ShowInteractableHoverText(Interactable hovered)
    {
        transform.localScale = Vector3.one;
        info.text = hovered.hoverText;
    }
    private void ShowInventorySlotHoverText(InventorySlot slot)
    {
        transform.localScale = Vector3.one;
        info.text = $"{slot.item.options[0]} {slot.item.itemName}";
    }
}
