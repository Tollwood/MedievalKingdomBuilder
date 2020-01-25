using System;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public GameObject optionsMenu;

    private Action onClose;

    public void OpenMenu(Action newOnClose)
    {
        optionsMenu.SetActive(true);
        onClose = newOnClose;
    }

    public void CloseMenu() {

        optionsMenu.SetActive(false);
        onClose();
    }
}
