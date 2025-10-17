using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
[RequireComponent (typeof(Image))]
public class MuteButton : MonoBehaviour
{
    [SerializeField] private Sprite _unmutedSprite;
    [SerializeField] private Sprite _mutedSprite;

    private Button _button;
    private Image _image;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleButtonClick);  
    }

    private void Start()
    {
        SwitchSprite();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
        SaveSystem.SavesData.IsSoundMuted = !SaveSystem.SavesData.IsSoundMuted;
        SaveSystem.Save();
        SwitchSprite();
    }

    private void SwitchSprite()
    {
        _image.sprite = SaveSystem.SavesData.IsSoundMuted ? _mutedSprite : _unmutedSprite;
        AudioListener.volume = SaveSystem.SavesData.IsSoundMuted ? 0f : 1f;
    }
}