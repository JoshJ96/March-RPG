using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Item/Equiptable")]
public class EquiptableItem : Item
{
    public enum Slot
    {
        RightHand,
        LeftHand
    }

    public Slot slot;

    public Mesh mesh;
}
