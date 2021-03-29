using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GotItemManager : MonoBehaviour
{
    //Prefab
    public GameObject gotItemCard;

    void Start()
    {
        GameEvents.instance.addItem += AddItem;
    }

    private void AddItem(Item item, int qty, int slot)
    {
        GameObject card = Instantiate(gotItemCard);
        card.transform.parent = this.transform;
        card.GetComponent<GotItemCard>().image.sprite = item.image;
        card.GetComponent<GotItemCard>().itemName.text = item.itemName;
        card.GetComponent<GotItemCard>().qty.text = qty.ToString();
    }
}
