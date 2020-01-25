using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewGameManager : MonoBehaviour
{

    public TMP_InputField familyNameText;
    public GameObject newGameMenu;
    public Button continueBttn;

    private Action onAbortFn;
    private void Start()
    {
        continueBttn.onClick.AddListener(() => {
            SaveData.current.familyName = familyNameText.text;
            newGameMenu.SetActive(false);
            SaveData.current.gameState = GameState.PLAYING;
        });
    }

    private void Update()
    {
        continueBttn.interactable = familyNameText.text != "";
    }

    public void OpenMenu(Action onAbort)
    {
        onAbortFn = onAbort;
        newGameMenu.SetActive(true);
    }


    public void Abort()
    {
        newGameMenu.SetActive(false);
        onAbortFn();
    }
}
