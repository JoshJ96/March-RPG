using System.Collections;
using System.Collections.Generic;

public class InventorySlot
{
    public InventorySlot(Item item, int qty, bool isEmpty)
    {
        this.item = item;
        this.qty = qty;
        this.isEmpty = isEmpty;
    }

    public InventorySlot(bool isEmpty)
    {
        this.isEmpty = isEmpty;
    }


    public bool isEmpty;
    public Item item;
    public int qty;
}
