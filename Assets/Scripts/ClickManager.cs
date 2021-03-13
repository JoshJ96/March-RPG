using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickManager : MonoBehaviour
{
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                GameEvents.instance.NavClick(hit.point);
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            GameEvents.instance.OnClickRelease();
        }
    }
}
