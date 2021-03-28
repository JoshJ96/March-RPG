using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    public int maxSize = 20;

    private void Start()
    {
        GameEvents.instance.attemptAddItem += AttemptAddItem;
        GameEvents.instance.addItem += AddItem;
    }

    private void AttemptAddItem(Item item, int qty)
    {
        bool canAdd = CanAddItem(item, qty, out int slot);
    }

    public bool CanAddItem(Item item, int qty, out int slot)
    {
        slot = 1;

        //Stackable
        if (item.stackable)
        {
            return false;
        }
        //Non-stackable
        else
        {
            for (int i = 0; i < maxSize; i++)
            {

            }
            return true;
        }
    }

    private void AddItem(Item item, int qty, int slot)
    {
        throw new NotImplementedException();
    }
}