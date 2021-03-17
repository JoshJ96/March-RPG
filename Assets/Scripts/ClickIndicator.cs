using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickIndicator : MonoBehaviour
{
    public Vector3 offset;
    void Start()
    {
        GameEvents.instance.navClick += NavClick;
        GameEvents.instance.onClickRelease += OnClickRelease;
    }

    private void NavClick(Vector3 point)
    {
        gameObject.SetActive(true);
        transform.position = point + offset;
    }
    private void OnClickRelease()
    {
        gameObject.SetActive(false);
    }
}
