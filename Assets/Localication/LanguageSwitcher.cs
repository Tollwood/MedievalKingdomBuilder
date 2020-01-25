using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Dropdown))]
public class LanguageSwitcher : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        foreach (string lang in TranslationService.current.supportedLanguages)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
            data.text = TranslationService.current.getTranslation(lang);
            dropdown.options.Add(data);
        }
    }

    public void OnValueChanged(int index)
    {
        TranslationService.current.SwitchLanguage(TranslationService.current.supportedLanguages[index]);
    }
}
