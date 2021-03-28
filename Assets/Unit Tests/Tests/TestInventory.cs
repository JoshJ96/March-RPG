using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestInventory
{
    private InventoryManager inventory;

    [SetUp]
    public void Setup()
    {
        //Initialize inventory obj
        GameObject invObject = new GameObject();
        invObject.AddComponent<InventoryManager>();
        inventory = invObject.GetComponent<InventoryManager>();
    }

    [UnityTest]
    public IEnumerator Test_InitializeInventory()
    {
        inventory.InitializeInventory(20);

        //The inventory is creating 20 empty slots
        Assert.AreEqual(inventory.inventory.Count, 20);

        bool isAllEmpty = true;
        for (int i = 0; i < inventory.inventory.Count; i++)
        {
            if (!inventory.inventory[i].isEmpty)
            {
                isAllEmpty = false;
            }
        }

        Assert.IsTrue(isAllEmpty);

        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_CanAddItem_NonStackable()
    {
        //Initialize inventory obj
        GameObject invObject = new GameObject();
        invObject.AddComponent<InventoryManager>();
        inventory = invObject.GetComponent<InventoryManager>();

        //Sample items
        inventory.InitializeInventory(20);
        Item item1 = (Item) ScriptableObject.CreateInstance("Item");

        //Item1 definition
        item1.stackable = false;

        //Simulate adding 10 items
        for (int i = 0; i < 11; i++)
        {
            inventory.inventory[i].isEmpty = false;
            inventory.inventory[i].item = item1;
            inventory.inventory[i].qty = 1;
        }

        bool canAdd = inventory.CanAddItem(item1, 1, out int slot);

        //Test conditions
        Assert.IsTrue(canAdd);
        Assert.AreEqual(slot, 11);


        //Test when the inventory is full
        inventory.InitializeInventory(20);

        //Simulate adding 20 items
        for (int i = 0; i < 20; i++)
        {
            inventory.inventory[i].isEmpty = false;
            inventory.inventory[i].item = item1;
            inventory.inventory[i].qty = 1;
        }


        canAdd = inventory.CanAddItem(item1, 1, out slot);

        //Test conditions
        Assert.IsFalse(canAdd);

        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_CanAddItem_Stackable()
    {
        //Initialize inventory obj
        GameObject invObject = new GameObject();
        invObject.AddComponent<InventoryManager>();
        inventory = invObject.GetComponent<InventoryManager>();

        //Sample items
        inventory.InitializeInventory(20);
        Item item1 = (Item)ScriptableObject.CreateInstance("Item");

        //Item1 definition
        item1.stackable = true;

        //Simulate adding the item
        inventory.inventory[0].isEmpty = false;
        inventory.inventory[0].item = item1;
        inventory.inventory[0].qty = 1;

        //Try to add another item
        bool canAdd = inventory.CanAddItem(item1, 12, out int slot);

        Assert.IsTrue(canAdd);
        Assert.AreEqual(slot, 0);

        yield return null;
    }
}