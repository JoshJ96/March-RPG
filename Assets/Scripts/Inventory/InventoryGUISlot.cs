using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryGUISlot : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    //GFX
    public Image image;
    public TextMeshProUGUI qty;

    //Components
    public int slot;
    float hoverTimer = 0;
    float hoverTimerMax = 0.05f;
    InventoryManager inventoryManager;
    Vector3 startPosition;
    bool dragging = false;

    /*----------------------------
                Start
    -----------------------------*/
    private void Start()
    {
        inventoryManager = transform.parent.GetComponent<InventoryManager>();
        GameEvents.instance.updateInventory += UpdateInventory;
    }

    /*----------------------------
           Pointer Events
    -----------------------------*/
    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryManager.currentHoveredSlot = null;
        GameEvents.instance.HideHoverText();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventoryManager.inventory[slot] != null)
        {
            //If there's an item in that slot, show the hover text
            if (!inventoryManager.inventory[slot].isEmpty)
            {
                //Don't show it if an item is being dragged
                if (!inventoryManager.draggingAnItem)
                {
                    GameEvents.instance.ShowInventorySlotHoverText(inventoryManager.inventory[slot]);
                }
            }

            //Set the current hovered slot
            inventoryManager.currentHoveredSlot = inventoryManager.inventory[slot];
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                break;
            case PointerEventData.InputButton.Right:
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Don't perform use actions if dragging
        if (dragging || inventoryManager.draggingAnItem)
        {
            return;
        }

        //Don't perform use actions if no item
        if (inventoryManager.inventory[slot] == null)
        {
            return;
        }
        else
        {
            if (inventoryManager.inventory[slot].isEmpty)
            {
                return;
            }
        }

        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                GameEvents.instance.AttemptItemAction(inventoryManager.inventory[slot].item, inventoryManager.inventory[slot].item.options[0], slot);
                break;
            case PointerEventData.InputButton.Right:
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                ItemReleaseAction();
                break;
            case PointerEventData.InputButton.Right:
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                break;
        }
    }




    /*----------------------------
            Fixed Update
    -----------------------------*/
    private void FixedUpdate()
    {
        if (!dragging)
        {
            startPosition = image.transform.position;
        }
    }

    /*----------------------------
                Update
    -----------------------------*/
    private void Update()
    {
        if (!inventoryManager.draggingAnItem)
        {
            if (Input.GetMouseButton(0))
            {
                if (inventoryManager.currentHoveredSlot == inventoryManager.inventory[this.slot])
                {
                    if (!inventoryManager.inventory[slot].isEmpty)
                    {
                        hoverTimer += Time.deltaTime;
                        if (hoverTimer > hoverTimerMax)
                        {
                            if (!dragging)
                            {
                                StartItemDrag();
                            }
                        }
                    }
                }
            }
        }

        else
        {
            if (!Input.GetMouseButton(0))
            {
                ItemReleaseAction();
            }

            hoverTimer = 0;
        }

        if (this.dragging)
        {
            image.transform.position = Input.mousePosition;
        }
    }

    /*----------------------------
                Helpers
    -----------------------------*/
    private void StartItemDrag()
    {
        GameEvents.instance.HideHoverText();
        inventoryManager.draggingAnItem = true;
        this.dragging = true;
        inventoryManager.currentDraggedSlot = inventoryManager.inventory[this.slot];
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.25f);
    }

    private void EndItemDrag()
    {
        inventoryManager.draggingAnItem = false;
        this.dragging = false;
        image.transform.position = startPosition;
        inventoryManager.currentDraggedSlot = null;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1.00f);
        hoverTimer = 0;
    }

    private void ItemReleaseAction()
    {
        //If we're not dragging anything, exit
        if (!dragging || !inventoryManager.draggingAnItem)
        {
            return;
        }
        //If the slot desired is out of bounds, or if it didn't move at all. Do nothing
        if (inventoryManager.currentHoveredSlot == null || inventoryManager.currentHoveredSlot == inventoryManager.currentDraggedSlot)
        {
            EndItemDrag();
        }

        //Swap
        else
        {
            inventoryManager.SwapSlots(inventoryManager.currentDraggedSlot, inventoryManager.currentHoveredSlot);
            EndItemDrag();
        }
    }

    /*----------------------------
         Game Event Responses
    -----------------------------*/
    private void UpdateInventory(List<InventorySlot> inventory, List<EquiptableItem> equipment)
    {
        if (inventory[slot].isEmpty)
        {
            image.gameObject.SetActive(false);
            qty.gameObject.SetActive(false);
            return;
        }

        if (inventory[slot].item.stackable)
        {
            image.gameObject.SetActive(true);
            qty.gameObject.SetActive(true);

            image.sprite = inventory[slot].item.image;
            qty.text = inventory[slot].qty.ToString();
        }
        else
        {
            image.gameObject.SetActive(true);
            image.sprite = inventory[slot].item.image;
        }
    }
}
