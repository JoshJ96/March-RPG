using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item3DListener : MonoBehaviour
{
    MeshFilter meshFilter;
    public EquiptableItem.Slot slot;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    void Start()
    {
        GameEvents.instance.equipItem += EquipItem;
    }

    private void EquipItem(EquiptableItem item, EquiptableItem.Slot slot, int inventorySlot)
    {
        if (slot == this.slot)
        {
            meshFilter.mesh = item.mesh;
        }
    }
}
