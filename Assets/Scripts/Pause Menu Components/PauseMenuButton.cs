using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuButton : MonoBehaviour
{
    public PauseMenuManagement.States containerToOpen;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Pressed);
    }
    private void Pressed()
    {
        GameEvents.instance.ChangePauseState(containerToOpen);
    }
}
