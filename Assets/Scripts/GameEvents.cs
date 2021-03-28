using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    void Awake()
    {
        instance = this;
    }

    #region Game Environment Events

    //Clicking an area on the navmesh
    public event Action<Vector3> navClick;
    public void NavClick(Vector3 point) => navClick?.Invoke(point);

    //Releasing mouse button press
    public event Action onClickRelease;
    public void OnClickRelease() => onClickRelease?.Invoke();

    #endregion

    #region Save Data Events

    //Save game data
    public event Action<GameData, string> saveData;
    public void SaveData(GameData data, string fileName) => saveData?.Invoke(data, fileName);

    //Load game data
    public GameData LoadData(string fileName)
    {
        string json = File.ReadAllText(fileName);
        return JsonUtility.FromJson<GameData>(json);
    }

    #endregion

    #region Player Events
    
    public event Action<PlayerController.States> changePlayerState;
    public void ChangePlayerState(PlayerController.States state) => changePlayerState?.Invoke(state);

    #endregion

    #region Interactable Events
    
    //Interactable object hovered
    public event Action<Interactable> showInteractableHoverText;
    public void ShowInteractableHoverText(Interactable hovered) => showInteractableHoverText?.Invoke(hovered);

    //Interactable object de-hovered
    public event Action hideInteractableHoverText;
    public void HideInteractableHoverText() => hideInteractableHoverText?.Invoke();

    //Interactable object clicked
    public event Action<Interactable> interactableClicked;
    public void InteractableClicked(Interactable obj) => interactableClicked?.Invoke(obj);

    //Interactable object de-focused
    public event Action interactableDefocused;
    public void InteractableDefocused() => interactableDefocused?.Invoke();

    #endregion

    #region Inventory Events

    //Attempt to add an item to the inventory
    public event Action<Item, int> attemptAddItem;
    public void AttemptAddItem(Item item, int qty) => attemptAddItem?.Invoke(item, qty);

    //Add an item to the inventory
    public event Action<Item, int, int> addItem;
    public void AddItem(Item item, int qty, int slot) => addItem?.Invoke(item, qty, slot);

    //Remove an item from inventory
    public event Action<Item, int, int> removeItemFromSlot;
    public void RemoveItemFromSlot(Item item, int qty, int slot) => removeItemFromSlot?.Invoke(item, qty, slot);

    //Remove an item from inventory (first element found)
    public event Action<Item, int> removeItem;
    public void RemoveItem(Item item, int qty) => removeItem?.Invoke(item, qty);

    //Update the inventory (mainly used for UI)
    public event Action<Dictionary<Item, int>> updateInventory;
    public void UpdateInventory(Dictionary<Item, int> inventory) => updateInventory?.Invoke(inventory);

    #endregion

    #region UI Events: Title Screen

    //Save slot clicked
    public event Action<int> saveSlotClick;
    public void SaveSlotClick(int slot) => saveSlotClick?.Invoke(slot);

    //Reload save files
    public event Action<Dictionary<int, GameData>> loadSaveSlots;
    public void LoadSaveSlots(Dictionary<int, GameData> data) => loadSaveSlots?.Invoke(data);

    //Start the save slot
    public event Action<GameData> startFile;
    public void StartFile(GameData file) => startFile?.Invoke(file);

    #endregion

    #region UI Events: Pause Menu

    public event Action<PauseMenuManagement.States> changePauseState;
    public void ChangePauseState(PauseMenuManagement.States state) => changePauseState?.Invoke(state);

    #endregion
}