using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryGUISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
{
    public int slot;
    public Image image;
    public TextMeshProUGUI qty;
    InventorySlot thisSlot;
    float hoverTimer = 0;
    Transform start;
    Rect parentRect;

    public enum State
    {
        Normal,
        Hovering,
        ReadyToMove
    }

    private State CurrentState;
    public State currentState
    {
        get
        {
            return CurrentState;
        }
        set
        {
            switch (value)
            {
                case State.Normal:
                    image.transform.position = start.position;
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
                    hoverTimer = 0;
                    break;
                case State.Hovering:
                    break;
                case State.ReadyToMove:
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0.4f);
                    break;
                default:
                    break;
            }

            CurrentState = value;
        }
    }

    void Start()
    {
        start = this.transform;
        GameEvents.instance.updateInventory += UpdateInventory;
        parentRect = transform.parent.GetComponent<RectTransform>().rect;
    }

    private void Update()
    {
        switch (currentState)
        {
            //Build hover time on left click hold
            case State.Hovering:
                hoverTimer += Time.deltaTime;
                print($"Hover timer: {hoverTimer}");
                if (hoverTimer > 0.1f)
                {
                    currentState = State.ReadyToMove;
                }
                break;

            //Drag the item with mouse input (within bounding box)
            case State.ReadyToMove:
                print("Mouse Width: " + Input.mousePosition.x + " ||| Bounding Box Width: " + parentRect.width / 2);

                if (Input.mousePosition.x < (parentRect.width/2))
                {
                    image.transform.position = Input.mousePosition;
                }
                break;
        }
    }

    private void UpdateInventory(List<InventorySlot> inventory, List<EquiptableItem> equipment)
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

                //If dragging an item, we probably don't want to equip it
                if (currentState == State.ReadyToMove)
                {
                    currentState = State.Normal;
                    return;
                }

                //Perform left click action
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

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                currentState = State.Hovering;
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