using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryGUISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int slot;
    public Image image;
    public TextMeshProUGUI qty;
    InventorySlot thisSlot;

    void Start()
    {
        GameEvents.instance.updateInventory += UpdateInventory;
    }

    private void UpdateInventory(List<InventorySlot> inventory)
    {
        thisSlot = inventory[slot];

        if (thisSlot.isEmpty)
        {
            image.gameObject.SetActive(false);
            qty.gameObject.SetActive(false);
            return;
        }

        if (thisSlot.item.stackable)
        {
            image.gameObject.SetActive(true);
            qty.gameObject.SetActive(true);

            image.sprite = thisSlot.item.image;
            qty.text = thisSlot.qty.ToString();
        }
        else
        {
            image.gameObject.SetActive(true);
            image.sprite = thisSlot.item.image;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (thisSlot == null)
        {
            return;
        }

        if (!thisSlot.isEmpty)
        {
            GameEvents.instance.ShowInventorySlotHoverText(thisSlot);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameEvents.instance.HideHoverText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                if (thisSlot != null)
                {
                    if (!thisSlot.isEmpty)
                    {
                        GameEvents.instance.AttemptItemAction(thisSlot.item, thisSlot.item.options[0], slot);
                    }
                }
                break;
            case PointerEventData.InputButton.Right:
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                break;
        }
    }
}
