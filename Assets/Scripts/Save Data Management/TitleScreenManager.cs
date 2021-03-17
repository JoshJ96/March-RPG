using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    Animator animator;
    GameData gameData;

    enum States
    {
        Normal,
        SaveSlotsScreen,
        OptionScreen
    }
    States currentState
    {
        get
        {
            return CurrentState;
        }
        set
        {
            switch (value)
            {
                case States.Normal:
                    titleScreenNormal.transform.localScale = Vector3.one;
                    saveSlotsScreen.transform.localScale = Vector3.zero;
                    break;
                case States.SaveSlotsScreen:
                    titleScreenNormal.transform.localScale = Vector3.zero;
                    saveSlotsScreen.transform.localScale = Vector3.one;
                    GameDataManager.instance.LoadSaveFiles();
                    break;
                case States.OptionScreen:
                    break;
                default:
                    break;
            }
        }
    }

    States CurrentState = States.Normal;

    public GameObject 
        titleScreenNormal,
        saveSlotsScreen,
        popupNewSave,
        optionScreen;

    public Button newGame;
    public Button backButton;
    public List<Button> saveSlots;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GameEvents.instance.saveSlotClick += SaveSlotClick;
        GameEvents.instance.startFile += StartFile;
        GameEvents.instance.saveData += SaveData;
        newGame.onClick.AddListener(NewGameClick);
        backButton.onClick.AddListener(BackButton);

        GameDataManager.instance.LoadSaveFiles();

    }

    private void StartFile(GameData data)
    {
        GameDataManager.instance.gameData = data;
        SceneManager.LoadScene("DevTest");
    }

    private void BackButton() => currentState = States.Normal;

    private void NewGameClick() => currentState = States.SaveSlotsScreen;

    private void SaveData(GameData data, string file)
    {
        animator.SetTrigger("NewSaveClose");
    }

    private void SaveSlotClick(int slot)
    {
        animator.SetTrigger("NewSavePopup");
    }
}