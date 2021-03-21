using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    RaycastHit clickedLocation;

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out clickedLocation, 100))
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(clickedLocation.point, out hit, 1.0f, NavMesh.AllAreas))
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
