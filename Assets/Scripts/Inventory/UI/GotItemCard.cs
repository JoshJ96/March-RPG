using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GotItemCard : MonoBehaviour
{
    public Image image;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI qty;

    //Animation event
    void AnimationEnd()
    {
        Destroy(this.gameObject);
    }
}
