using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TranslatableText : MonoBehaviour
{
    internal TextMeshProUGUI text;
    public string translationKey;

    internal virtual void OnEnable()
    {
        if(text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        text.text = TranslationService.current.getTranslation(translationKey);
    }
}
