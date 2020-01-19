using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewGameManager : MonoBehaviour
{

    public TMP_InputField familyNameText;
    public GameObject newGameMenu;
    public Button continueBttn;


    private void Start()
    {
        continueBttn.onClick.AddListener(() => {
            SaveData.current.familyName = familyNameText.text;
            newGameMenu.SetActive(false);
            SaveData.current.gameStarted = true;
        });
    }

    private void Update()
    {
        continueBttn.interactable = familyNameText.text != "";
    }

    public void OpenMenu()
    {
        newGameMenu.SetActive(true);
    }

}
