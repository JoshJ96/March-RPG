using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveSlot : MonoBehaviour, IPointerClickHandler
{
    bool exists = false;
    public TextMeshProUGUI text;
    public int slot;
    GameData gameData;

    public void OnPointerClick(PointerEventData eventData)
    {
        //If there's already data, start the game
        if (!exists)
        {
            GameEvents.instance.SaveSlotClick(slot);
        }
        else
        {
            GameEvents.instance.StartFile(gameData);
        }
    }

    private void Start()
    {
        GameEvents.instance.loadSaveSlots += LoadSaveSlots;
    }

    private void LoadSaveSlots(Dictionary<int, GameData> data)
    {
        if (data.ContainsKey(slot))
        {
            exists = true;
            gameData = data[slot];
            text.text = $"File {slot}\n\nLevel {data[slot].level}\n$ {data[slot].currency}";
        }
    }
}
