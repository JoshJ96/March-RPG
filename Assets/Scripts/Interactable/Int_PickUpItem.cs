using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Int_PickUpItem : Interactable
{
    public Item itemToGive;
    public int qty;
    public bool unlimited = false;

    public override void Interact()
    {
        print($"Picking up item {this.objectName}.");
        GameEvents.instance.AttemptAddItem(itemToGive, qty);
        base.Interact();
        GameEvents.instance.InteractableDefocused();
        if (!unlimited)
        {
            Destroy(this.gameObject);
        }
    }
}
