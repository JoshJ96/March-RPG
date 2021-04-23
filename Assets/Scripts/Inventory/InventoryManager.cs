using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> inventory;
    public List<EquiptableItem> equipment;

    public int maxSize = 20;
    public GameObject invPanel, eqpPanel;
    public enum States
    {
        ShowInventory,
        ShowEquipment
    }
    private States CurrentState = States.ShowInventory;
    public States currentState
    {
        get
        {
            return CurrentState;
        }
        set
        {
            switch (value)
            {
                case States.ShowInventory:
                    invPanel.transform.localScale = Vector3.one;
                    eqpPanel.transform.localScale = Vector3.zero;
                    break;
                case States.ShowEquipment:
                    invPanel.transform.localScale = Vector3.zero;
                    eqpPanel.transform.localScale = Vector3.one;
                    break;
                default:
                    break;
            }
            CurrentState = value;
        }
    }

    public bool draggingAnItem = false;

    public InventorySlot currentDraggedSlot;
    public InventorySlot currentHoveredSlot;

    /*----------------------------
                Start
    -----------------------------*/
    private void Start()
    {
        print(this.gameObject.name);
        InitializeInventory(maxSize);

        //Event subscriptions
        GameEvents.instance.attemptAddItem += AttemptAddItem;
        GameEvents.instance.addItem += AddItem;
        GameEvents.instance.attemptItemAction += AttemptItemAction;
        GameEvents.instance.equipItem += EquipItem;
        GameEvents.instance.switchInvEqpDisplay += SwitchInvEqpDisplay;
        GameEvents.instance.attemptUnequip += AttemptUnequip;
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

        GameEvents.instance.UpdateInventory(inventory, equipment);
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
        //Null inv slot
        inventory[inventorySlot].item = null;
        inventory[inventorySlot].qty = 0;
        inventory[inventorySlot].isEmpty = true;

        //Unequip current item (if necessary)
        EquiptableItem equippedItem = equipment.FirstOrDefault(x => x.slot == slot);
        if (equippedItem != null)
        {
            InventorySlot addTo = FindFirstAvailableSlot();
            addTo.item = equippedItem;
            addTo.isEmpty = false;
            equipment.Remove(equipment.FirstOrDefault(x => x.itemName == item.itemName));
        }

        //Add to equipment
        equipment.Add(item);

        GameEvents.instance.UpdateInventory(inventory, equipment);
    }

    private void SwitchInvEqpDisplay(InvEqpSwitcherBtn.ButtonType button)
    {
        switch (button)
        {
            case InvEqpSwitcherBtn.ButtonType.Inventory:
                currentState = States.ShowInventory;
                break;
            case InvEqpSwitcherBtn.ButtonType.Equipment:
                currentState = States.ShowEquipment;
                break;
            default:
                break;
        }
    }

    private void AttemptUnequip(EquiptableItem item)
    {
        if (FindFirstAvailableSlot() != null)
        {

            InventorySlot addTo = FindFirstAvailableSlot();
            addTo.item = item;
            addTo.isEmpty = false;
            equipment.Remove(equipment.FirstOrDefault(x => x.itemName == item.itemName));
        }

        GameEvents.instance.UpdateInventory(inventory, equipment);
    }

    /*----------------------------
               Helpers
    -----------------------------*/
    public void SwapSlots(InventorySlot dragged, InventorySlot hovered)
    {
        //Perform the swap!
        InventorySlot temp_hovered = new InventorySlot(hovered.item, hovered.qty, hovered.isEmpty);
        InventorySlot temp_dragged = new InventorySlot(dragged.item, dragged.qty, dragged.isEmpty);

        hovered.item = temp_dragged.item;
        hovered.qty = temp_dragged.qty;
        hovered.isEmpty = temp_dragged.isEmpty;

        dragged.item = temp_hovered.item;
        dragged.qty = temp_hovered.qty;
        dragged.isEmpty = temp_hovered.isEmpty;

        GameEvents.instance.UpdateInventory(inventory, equipment);
    }

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

    public InventorySlot FindFirstAvailableSlot()
    {
        for (int i = 0; i < maxSize; i++)
        {
            if (inventory[i].isEmpty)
            {
                return inventory[i];
            }
        }

        return null;
    }
}