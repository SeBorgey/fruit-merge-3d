using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Translation", menuName = "Localization/TranslatedText", order = 1)]
[Serializable]
public class TranslatedText : ScriptableObject
{
    [SerializeField] private string _ru, _en;

    public string Ru => _ru;
    public string En => _en;

    public string Text => SwitchLanguage(SaveSystem.SavesData.Language);

    public string SwitchLanguage(string lang)
    {
        switch (lang)
        {
            case "ru": return _ru;
            case "en": return _en;
            default: return _en;
        }    
    }
}
