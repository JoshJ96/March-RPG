using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> inventory;
    public int maxSize = 20;

    /*----------------------------
                Start
    -----------------------------*/
    private void Start()
    {
        InitializeInventory(maxSize);

        //Event subscriptions
        GameEvents.instance.attemptAddItem += AttemptAddItem;
        GameEvents.instance.addItem += AddItem;
        GameEvents.instance.attemptItemAction += AttemptItemAction;
        GameEvents.instance.equipItem += EquipItem;
    }

    /*----------------------------
             Game Events
    -----------------------------*/
    private void AttemptAddItem(Item item, int qty)
    {
        //Can the item be added?
        bool canAdd = CanAddItem(item, qty, out int slot);

        //Add it to the slot
        if (canAdd)
        {
            GameEvents.instance.AddItem(item, qty, slot);
        }
        else
        {
            //GUI event (on item add failure?)
        }
    }

    private void AddItem(Item item, int qty, int slot)
    {
        if (item.stackable)
        {

        }
        else
        {
            inventory[slot].isEmpty = false;
            inventory[slot].item = item;
            inventory[slot].qty = qty;
        }

        GameEvents.instance.UpdateInventory(inventory);
    }

    private void AttemptItemAction(Item item, Item.Options option, int inventorySlot)
    {
        //Todo: checks n stuff

        switch (option)
        {
            case Item.Options.Use:
                break;
            case Item.Options.Equip:
                EquiptableItem e_item = (EquiptableItem) item;
                GameEvents.instance.EquipItem(e_item, e_item.slot, inventorySlot);
                break;
            case Item.Options.Drop:
                break;
            case Item.Options.Place:
                break;
            default:
                break;
        }
    }

    private void EquipItem(EquiptableItem item, EquiptableItem.Slot slot, int inventorySlot)
    {
        inventory[inventorySlot].item = null;
        inventory[inventorySlot].qty = 0;
        inventory[inventorySlot].isEmpty = true;
        GameEvents.instance.UpdateInventory(inventory);
    }

    /*----------------------------
               Helpers
    -----------------------------*/
    public void InitializeInventory(int size)
    {
        inventory = new List<InventorySlot>(size);
        for (int i = 0; i < size; i++)
        {
            inventory.Add(new InventorySlot(true));
        }
    }

    public bool CanAddItem(Item item, int qty, out int slot)
    {
        slot = 0;

        //Stackable
        if (item.stackable)
        {
            for (int i = 0; i < maxSize; i++)
            {
                if (inventory[i].item == item)
                {
                    return true;
                }
            }
        }
        //Non-stackable
        else
        {
            for (int i = 0; i < maxSize; i++)
            {
                if (inventory[i].isEmpty)
                {
                    slot = i;
                    return true;
                }
            }
        }
        return false;
    }
}