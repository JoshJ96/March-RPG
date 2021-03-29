using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUISlot : MonoBehaviour
{
    public int slot;
    public Image image;
    public TextMeshProUGUI qty;

    void Start()
    {
        GameEvents.instance.updateInventory += UpdateInventory;
    }

    private void UpdateInventory(List<InventorySlot> inventory)
    {
        InventorySlot thisSlot = inventory[slot];

        if (thisSlot.isEmpty)
        {
            /*
            image.gameObject.SetActive(false);
            qty.gameObject.SetActive(false);
            */
            return;
        }

        if (thisSlot.item.stackable)
        {
            image.gameObject.SetActive(true);
            qty.gameObject.SetActive(true);

            image.sprite = thisSlot.item.image;
            qty.text = thisSlot.qty.ToString();
        }
        else
        {
            image.gameObject.SetActive(true);
            image.sprite = thisSlot.item.image;
        }


    }
}
