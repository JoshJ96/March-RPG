using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentGUISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public EquiptableItem.Slot slot;
    EquiptableItem thisItem;
    public Image image;

    void Start()
    {
        GameEvents.instance.updateInventory += UpdateInventory;
    }

    private void UpdateInventory(List<InventorySlot> inventory, List<EquiptableItem> equipment)
    {

        thisItem = equipment.FirstOrDefault(x => x.slot == this.slot);

        if (thisItem != null)
        {
            image.gameObject.SetActive(true);
            image.sprite = thisItem.image;
        }
        else
        {
            image.gameObject.SetActive(false);
            image.sprite = null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (thisItem == null)
        {
            return;
        }

        GameEvents.instance.ShowUnequipOption(thisItem);
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
                if (thisItem != null)
                {
                    GameEvents.instance.AttemptUnequip(thisItem);
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
