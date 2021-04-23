using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform toLookAt;

    void Update()
    {
        transform.LookAt(toLookAt);
    }
}
