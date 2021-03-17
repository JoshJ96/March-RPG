using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickIndicator : MonoBehaviour
{
    public Vector3 offset;
    bool active = true;
    void Start()
    {
        GameEvents.instance.navClick += NavClick;
        GameEvents.instance.onClickRelease += OnClickRelease;
        GameEvents.instance.changePlayerState += ChangePlayerState;
    }

    private void ChangePlayerState(PlayerController.States state)
    {
        switch (state)
        {
            case PlayerController.States.Normal:
                active = true;
                break;
            case PlayerController.States.Paused:
                gameObject.SetActive(false);
                active = false;
                break;
        }
    }

    private void NavClick(Vector3 point)
    {
        if (active)
        {
            gameObject.SetActive(true);
            transform.position = point + offset;
        }
    }
    private void OnClickRelease()
    {
        if (active)
        {
            gameObject.SetActive(false);
        }
    }
}
