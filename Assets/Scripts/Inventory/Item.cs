using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string UID;
    private void OnValidate()
    {
    #if UNITY_EDITOR
        if (UID == "")
        {
            UID = GUID.Generate().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    #endif
    }

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