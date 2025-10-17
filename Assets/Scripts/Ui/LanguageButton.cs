using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class LanguageButton : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _text;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleButtonClick);
    }

    private void Start()
    {
        _text.text = SaveSystem.SavesData.Language;
        Debug.Log("Language changed to =>" + SaveSystem.SavesData.Language);
    }

    private void HandleButtonClick()
    {
        if (SaveSystem.SavesData.Language == "ru") SaveSystem.SavesData.Language = "en";
        else if (SaveSystem.SavesData.Language == "en") SaveSystem.SavesData.Language = "ru";
        _text.text = SaveSystem.SavesData.Language;
        Debug.Log("Language changed to =>" + SaveSystem.SavesData.Language);
        SaveSystem.Save();
    }
}
