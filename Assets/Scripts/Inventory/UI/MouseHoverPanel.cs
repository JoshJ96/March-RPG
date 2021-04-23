using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHoverPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("On");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("Off");
    }
}
