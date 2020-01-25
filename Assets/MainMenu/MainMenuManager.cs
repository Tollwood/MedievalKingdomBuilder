using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public GameObject buttonPanel;

    public Button saveButton;
    public Button newButton;

    private SaveManager saveManager;
    private NewGameManager newGameManager;
    private OptionsManager optionsManager;

    private void Start()
    {
        saveManager = transform.GetComponent<SaveManager>();
        newGameManager = transform.GetComponent<NewGameManager>();
        optionsManager = transform.GetComponent<OptionsManager>();
    }

    private void Update()
    {
        saveButton.interactable = SaveData.current.gameState != GameState.NEW_GAME;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SaveData.current.gameState == GameState.PLAYING)
            {
                OpenMainMenu();
            }
            else if( SaveData.current.gameState == GameState.PAUSED)
            {
                CloseMainMenu();
            }
            
        }

    }

    private string GetFamiliyName()
    {
        return SaveData.current.familyName == "" ? "" : " - " + SaveData.current.familyName;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenNewGameMenu()
    {
        SaveData.Reset();
        CloseMainMenu();
        newGameManager.OpenMenu(OpenMainMenu);
    }

    public void OpenLoadMenu()
    {
        CloseMainMenu();
        saveManager.OpenLoadMenu(OpenMainMenu);
    }

    public void OpenSaveMenu()
    {
        CloseMainMenu();
        saveManager.OpenSaveMenu(OpenMainMenu);
    }

    public void OpenMainMenu()
    {
        SaveData.current.prevGameState = SaveData.current.gameState;
        SaveData.current.gameState = GameState.PAUSED;
        title.gameObject.SetActive(true);
        title.text = "Kingdom Builder " + GetFamiliyName();
        buttonPanel.SetActive(true);
    }

    public void CloseMainMenu()
    {
        SaveData.current.gameState = SaveData.current.prevGameState;
        title.gameObject.SetActive(false);
        buttonPanel.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        CloseMainMenu();
        optionsManager.OpenMenu(OpenMainMenu);
    }
}
