using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentGUISlot : MonoBehaviour
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
    }
}
