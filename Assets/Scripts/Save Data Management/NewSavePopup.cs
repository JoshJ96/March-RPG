using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewSavePopup : MonoBehaviour
{
    public TextMeshProUGUI prompt;
    public Button yes, no;
    int slot;

    private void Start()
    {
        GameEvents.instance.saveSlotClick += SaveSlotClick;
        yes.onClick.AddListener(OnClickYes);
        no.onClick.AddListener(OnClickNo);
    }

    private void OnClickYes()
    {
        string fileName = $"SaveSlot{slot}";
        GameEvents.instance.SaveData(new GameData(fileName), fileName);
        GameDataManager.instance.LoadSaveFiles();
    }

    private void OnClickNo()
    {
        print("how dare you");
    }

    private void SaveSlotClick(int slot)
    {
        this.slot = slot;
        prompt.text = $"Create a new save file at slot {slot}?";
    }
}