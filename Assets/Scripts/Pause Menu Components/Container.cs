using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public PauseMenuManagement.States openWhen;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameEvents.instance.changePauseState += ChangePauseState;
    }

    private void ChangePauseState(PauseMenuManagement.States state)
    {
        if (state == openWhen)
        {
            transform.localScale = Vector3.one;
            //gameObject.SetActive(true);
            //animator.SetTrigger("Open");
        }
        else
        {
            transform.localScale = Vector3.zero;
            //gameObject.SetActive(false);
            //animator.SetTrigger("Close");
        }
    }
}
