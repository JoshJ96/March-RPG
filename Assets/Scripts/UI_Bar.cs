using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Bar : MonoBehaviour
{
    public GameObject statusBar;
    public GameObject canvas;
    public GameObject barUI;
    public Transform barLocation;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        barUI = Instantiate(statusBar);
        barUI.transform.parent = canvas.transform;
        barUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        barUI.transform.position = Camera.main.WorldToScreenPoint(barLocation.position);
    }
}