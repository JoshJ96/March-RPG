using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvEqpSwitcherBtn : MonoBehaviour
{
    public enum ButtonType
    {
        Inventory,
        Equipment
    }
    public ButtonType type;

    Button button;
    public void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Pressed);
    }

    void Pressed() => GameEvents.instance.SwitchInvEqpDisplay(type);
}
