using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string objectName;
    public string hoverText;
    Outline outline;
    private void Awake()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 0f;
    }
    private void OnMouseEnter()
    {
        outline.OutlineWidth = 3f;
        GameEvents.instance.HoverInteractable(this);
    }
    private void OnMouseExit()
    {
        outline.OutlineWidth = 0f;
        GameEvents.instance.DeHoverInteractable();
    }
}
