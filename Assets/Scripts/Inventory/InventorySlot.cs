using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public int slotNumber;

    //Item and qty. if not stackable, qty is 1 and dont show text
    public Item item;
    public int qty;

    private void Start()
    {
        //Maybe, maybe not
        //string hoverText = item.options[0].ToString();
    }
}
