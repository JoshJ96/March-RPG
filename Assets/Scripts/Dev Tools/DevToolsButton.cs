using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevToolsButton : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Pressed);
    }

    private void Pressed()
    {
        GameEvents.instance.DevToolsButtonClick();
    }
}
