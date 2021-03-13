using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    void Awake()
    {
        instance = this;
    }

    //Clicking an area on the navmesh
    public event Action<Vector3> navClick;
    public void NavClick(Vector3 point) => navClick?.Invoke(point);

    //Releasing mouse button press
    public event Action onClickRelease;
    public void OnClickRelease() => onClickRelease?.Invoke();
}
