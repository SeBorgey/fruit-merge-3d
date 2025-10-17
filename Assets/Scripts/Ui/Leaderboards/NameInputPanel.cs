using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIPanelFader))]
public class NameInputPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField _usernameInputField;
    [SerializeField] private Button _submitButton;

    private UIPanelFader _panelFader;
    public UIPanelFader PanelFader => _panelFader;
    public Action OnNameSubmitted;

    private void OnEnable()
    {
        _panelFader = GetComponent<UIPanelFader>();
        _submitButton.onClick.AddListener(HanddleSubmitButtonClick);
    }

    private void OnDisable()
    {
        _submitButton.onClick.RemoveListener(HanddleSubmitButtonClick);
    }

    private void HanddleSubmitButtonClick()
    {
        if(_usernameInputField.text != "")
        {
            SaveSystem.SavesData.UserName = _usernameInputField.text;
            OnNameSubmitted?.Invoke();
        }
    }
}
