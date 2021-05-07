using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Int_ChopTree : Interactable
{
    UI_Bar statusBar;
    InventoryManager inventoryManager;

    public override void Start()
    {
        base.Start();
        statusBar = GetComponent<UI_Bar>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        GameEvents.instance.interactableDefocused += InteractableDefocused;
    }

    public override void Interact()
    {
        List<InventorySlot> axes = new List<InventorySlot>();
        bool playerHasAxe = inventoryManager.InventoryContainsItemWithCategory(Item.Category.Hatchet, out axes);
        if (playerHasAxe)
        {
            print("Found");
            statusBar.barUI.SetActive(true);
        }
        else
        {
            print("Not Found");
        }

        base.Interact();
    }

    void InteractableDefocused()
    {
        statusBar.barUI.SetActive(false);
    }
}