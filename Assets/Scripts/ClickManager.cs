using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickManager : MonoBehaviour
{
    RaycastHit clickedLocation;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out clickedLocation, 100))
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(clickedLocation.point, out hit, 0.1f, NavMesh.AllAreas))
                {
                    GameEvents.instance.NavClick(clickedLocation.point);
                }
                else
                {
                    GameEvents.instance.OnClickRelease();
                }
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            GameEvents.instance.OnClickRelease();
        }
    }
}
