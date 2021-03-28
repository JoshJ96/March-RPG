using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public enum Options
    {
        Use,
        Equip,
        Drop
    }

    public Sprite image;
    public string itemName;
    public string itemDescription;
    public bool stackable;


    public List<Options> options;
}