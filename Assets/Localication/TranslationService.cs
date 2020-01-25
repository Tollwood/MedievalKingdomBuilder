using System.Collections.Generic;
using UnityEngine;

public class TranslationService
{
    private static TranslationService _current;

    public static TranslationService current {
        get {

            if (_current == null)
            {
                _current = new TranslationService();
            }

            return _current;
        }
    }

    public string[] supportedLanguages
    {
        get
        {
            return new string[] { "en", "de" };
        }
    }

    private char lineSeperator = '\n';
    private char valueSeperator = '=';

    public Dictionary<string, Dictionary<string, string>> languages = new Dictionary<string, Dictionary<string, string>>();

    public TranslationService()
    {
        LoadTranslationsFromFile();
    }

    private void LoadTranslationsFromFile()
    {
       foreach(string language in supportedLanguages)
        {
            TextAsset file = Resources.Load<TextAsset>("translation_" + language);
            string[] rows = file.text.Split(lineSeperator);
            Dictionary<string,string> dictionary = new Dictionary<string, string>();
            languages.Add(language, dictionary);
            foreach(string row in rows)
            {
                string[] keyValue = row.Split(valueSeperator);
                dictionary.Add(keyValue[0], keyValue[1]);
            }
        }
    }

    public string getTranslation( string key)
    {
        Dictionary<string, string> translations;
        languages.TryGetValue(GetCurrentLanguage(), out translations);
        string translation;
        translations.TryGetValue(key, out translation);
        return translation ?? key;
    }

    private string GetCurrentLanguage()
    {
        int languageIndex = PlayerPrefs.GetInt("language", 0);
        return supportedLanguages[languageIndex];
    }

    public string SwitchLanguage(string language)
    {
        for(int i = 0; i < supportedLanguages.Length; i++)
        {
            if(supportedLanguages[i] == language)
            {
                PlayerPrefs.SetInt("language", i);
                PlayerPrefs.Save();
                Debug.Log("language switched to  " + supportedLanguages[i]);
                return language;
            }
        }
        return GetCurrentLanguage();
    }

}
