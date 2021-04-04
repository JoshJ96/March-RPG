using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public enum Options
    {
        Use,
        Equip,
        Unequip,
        Drop,
        Place,
    }

    public Sprite image;
    public string itemName;
    public string itemDescription;
    public bool stackable;


    public List<Options> options;
}