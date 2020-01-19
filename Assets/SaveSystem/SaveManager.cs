using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    private const string PLACEHOLDER = "placeholder";
    private const string IMAGE_FOLDER = "/images/";
    private const string SAVE_FOLDER = "/saves/";
    private const string SAVE_FILE_EXTENSION = ".save";


    private Action onClose; 
    private Button[] saveItems;
    private string[] saveFiles;

    public Canvas canvas;
    public GameObject saveMenu;

    void Start()
    {
        SaveData.Reset();
        saveItems = saveMenu.transform.Find("SaveItems").GetComponentsInChildren<Button>();
        UpdateSaveItems();
    }

    public void OpenSaveMenu(Action newOnClose )
    {
        saveMenu.SetActive(true);
        SetTitle("Save Game");
        UpdateSaveItems();
        RegisterSaveAction();
        onClose = newOnClose;
    }

    public void OpenLoadMenu(Action newOnClose)
    {
        saveMenu.SetActive(true);
        SetTitle("Load Game");
        UpdateSaveItems();
        RegisterLoadAction();
        onClose = newOnClose;
    }

    public void CloseMenu()
    {
        saveMenu.SetActive(false);
        onClose();
    }


    private void UpdateSaveItems()
    {
        SetSaveFiles();
        for (int i = 0; i < saveFiles.Length; i++)
        {
            string saveName = GetSaveNameFromIndex(i);

            if (saveName != PLACEHOLDER)
            {
                saveItems[i].transform.Find("Name").GetComponentInChildren<TextMeshProUGUI>().text = saveName;
                saveItems[i].transform.Find("Date").GetComponent<TextMeshProUGUI>().text = File.GetLastWriteTime(saveFiles[i]).ToString("dd.MM.yyyy - hh:mm:ss");
                RawImage image = saveItems[i].transform.Find("Image").GetComponent<RawImage>();
                string fileName = i +"_" + saveName;
                StartCoroutine(GetTextureFromFile( image, fileName));
            }
        }
    }

    private IEnumerator GetTextureFromFile(RawImage image, string fileName)
    {
        Texture2D tex = new Texture2D(220, 180);
        string path = Application.persistentDataPath + IMAGE_FOLDER + fileName;
        if (!File.Exists(path))
        {
            yield return null;
        }
        tex.LoadImage(File.ReadAllBytes(path));
        image.texture = tex;

    }

    public void SetSaveFiles()
    {
        saveFiles = Directory.GetFiles(Application.persistentDataPath + SAVE_FOLDER);
    }

    private void CreateDirectoryAndFiles()
    {
        Directory.CreateDirectory(Application.persistentDataPath + IMAGE_FOLDER);
        Directory.CreateDirectory(Application.persistentDataPath + SAVE_FOLDER);
        SetSaveFiles();
        if (saveFiles.Length != saveItems.Length)
        {
            foreach(string path in saveFiles)
            {
                File.Delete(path);
            }
            for (int i = 0; i < saveItems.Length; i++)
            {
                File.Create(Application.persistentDataPath + SAVE_FOLDER + i + "_" + PLACEHOLDER + SAVE_FILE_EXTENSION);
            }
        }
        SetSaveFiles();
    }

    private void RegisterSaveAction()
    {
        for (int i = 0; i < saveItems.Length; i++)
        {
            int index = i;
            saveItems[i].onClick.RemoveAllListeners();
            saveItems[i].onClick.AddListener(() => {
                string fileName = index + "_" + SaveData.current.familyName;
                File.Delete(saveFiles[index]);
                StartCoroutine("SaveWithScreenshot", fileName);
            });
        }
    }

    private string GetSaveNameFromIndex(int index)
    {
        string pathToSaveFiles = Application.persistentDataPath + SAVE_FOLDER;
        return saveFiles[index].Substring(pathToSaveFiles.Length + 2, saveFiles[index].Length - pathToSaveFiles.Length - SAVE_FILE_EXTENSION.Length - 2);
    }

    public IEnumerator SaveWithScreenshot(string fileName)
    {
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        canvas.enabled = false;

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        // Take screenshot
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + IMAGE_FOLDER + fileName);

        // Show UI after we're done
        canvas.enabled = true;
        string savePath = Application.persistentDataPath + SAVE_FOLDER + fileName + SAVE_FILE_EXTENSION;
        SerializationManager.Save(savePath, SaveData.current);
        UpdateSaveItems();
    }

    private void RegisterLoadAction()
    {
        for (int i = 0; i < saveItems.Length; i++)
        {
            saveItems[i].onClick.RemoveAllListeners();
            string saveName = GetSaveNameFromIndex(i);

            if (saveName != PLACEHOLDER)
            {
                int index = i;
                saveItems[i].onClick.AddListener(() => {
                    string savePath = Application.persistentDataPath + SAVE_FOLDER + index + "_" + saveName + SAVE_FILE_EXTENSION;
                    SaveData.Update(SerializationManager.Load(savePath) as SaveData);
                    CloseMenu();
                });
            }
        }
    }

    private void SetTitle(string title)
    {
        saveMenu.transform.Find("TitlePanel").GetComponentInChildren<TextMeshProUGUI>().text = title;
    }
}
