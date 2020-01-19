using System;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject loadGamePenal;
    public TextMeshProUGUI title;
    public GameObject buttonPanel;

    public GameObject saveButton;
    public GameObject newButton;

    private SaveManager saveManager;
    private NewGameManager newGameManager;

    private void Start()
    {
        saveManager = transform.GetComponent<SaveManager>();
        newGameManager = transform.GetComponent<NewGameManager>();
    }

    private void Update()
    {

        saveButton.SetActive(SaveData.current.gameStarted);
        newButton.SetActive(!SaveData.current.gameStarted);

        if (Input.GetKeyDown(KeyCode.Escape) && SaveData.current.gameStarted)
        {
            title.gameObject.SetActive(!title.gameObject.activeSelf);
            title.text = "Kingdom Builder " + GetFamiliyName();
            buttonPanel.SetActive(!buttonPanel.activeSelf);
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
        CloseMainMenu();
        newGameManager.OpenMenu();
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
        title.gameObject.SetActive(true);
        title.text = "Kingdom Builder " + GetFamiliyName();
        buttonPanel.SetActive(true);
    }

    public void CloseMainMenu()
    {
        title.gameObject.SetActive(false);
        buttonPanel.SetActive(false);
    }
}
