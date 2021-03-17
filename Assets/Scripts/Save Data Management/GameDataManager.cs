using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;
    [HideInInspector]
    public GameData gameData;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        GameEvents.instance.saveData += SaveData;
    }

    private void SaveData(GameData gameData, string fileName)
    {
        string jsonString = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.persistentDataPath + $"/{fileName}.json", jsonString);
    }

    public void LoadSaveFiles()
    {
        //Try to load all 10 save slots
        Dictionary<int, GameData> savedSlots = new Dictionary<int, GameData>();

        for (int i = 0; i <= 10; i++)
        {
            string saveFile = Application.persistentDataPath + $"/SaveSlot{i}.json";
            if (File.Exists(saveFile))
            {
                savedSlots.Add(i, GameEvents.instance.LoadData(saveFile));
            }
        }

        GameEvents.instance.LoadSaveSlots(savedSlots);
    }

    public Vector3 GetSavedPlayerPosition()
    {
        if (gameData == null)
        {
            return new Vector3(1, 1, 1);
        }
        return new Vector3(
            gameData.playerPosition[0],
            gameData.playerPosition[1],
            gameData.playerPosition[2]
            );
    }

    public void SavePlayerPosition(Vector3 position)
    {
        gameData.playerPosition[0] = position.x;
        gameData.playerPosition[1] = position.y;
        gameData.playerPosition[2] = position.z;
        Save();
    }

    public void Save()
    {
        SaveData(gameData, gameData.fileName);
    }
}