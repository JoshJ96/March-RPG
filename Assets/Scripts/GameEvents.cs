using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    void Awake()
    {
        instance = this;
    }

    /*----------------------------
        Game Environment Events
     -----------------------------*/

    //Clicking an area on the navmesh
    public event Action<Vector3> navClick;
    public void NavClick(Vector3 point) => navClick?.Invoke(point);

    //Releasing mouse button press
    public event Action onClickRelease;
    public void OnClickRelease() => onClickRelease?.Invoke();

    /*----------------------------
        Save Data Events
    -----------------------------*/

    //Save game data
    public event Action<GameData, string> saveData;
    public void SaveData(GameData data, string fileName) => saveData?.Invoke(data, fileName);

    //Load game data
    public GameData LoadData(string fileName)
    {
        string json = File.ReadAllText(fileName);
        return JsonUtility.FromJson<GameData>(json);
    }

    /*----------------------------
        Player Events
    -----------------------------*/
    public event Action<PlayerController.States> changePlayerState;
    public void ChangePlayerState(PlayerController.States state) => changePlayerState?.Invoke(state);

    /*----------------------------
        Interactable Events
    -----------------------------*/

    //Interactable object hovered
    public event Action<Interactable> showInteractableHoverText;
    public void ShowInteractableHoverText(Interactable hovered) => showInteractableHoverText?.Invoke(hovered);

    //Interactable object de-hovered
    public event Action hideInteractableHoverText;
    public void HideInteractableHoverText() => hideInteractableHoverText?.Invoke();

    //Interactable object clicked
    public event Action<Interactable> interactableClicked;
    public void InteractableClicked(Interactable obj) => interactableClicked?.Invoke(obj);


    /*----------------------------
        UI Events: Title Screen
    -----------------------------*/

    //Save slot clicked
    public event Action<int> saveSlotClick;
    public void SaveSlotClick(int slot) => saveSlotClick?.Invoke(slot);

    //Reload save files
    public event Action<Dictionary<int, GameData>> loadSaveSlots;
    public void LoadSaveSlots(Dictionary<int, GameData> data) => loadSaveSlots?.Invoke(data);

    //Start the save slot
    public event Action<GameData> startFile;
    public void StartFile(GameData file) => startFile?.Invoke(file);

    /*----------------------------
        UI Events: Pause Menu
    -----------------------------*/
    public event Action<PauseMenuManagement.States> changePauseState;
    public void ChangePauseState(PauseMenuManagement.States state) => changePauseState?.Invoke(state);

    /*----------------------------
        Dev Tools Events
    -----------------------------*/
    //Dev tools button click
    public event Action devToolsButtonClick;
    public void DevToolsButtonClick() => devToolsButtonClick?.Invoke();

    public event Action<DevToolsManager.State> changeDevToolsState;
    public void ChangeDevToolsState(DevToolsManager.State state) => changeDevToolsState?.Invoke(state);
}