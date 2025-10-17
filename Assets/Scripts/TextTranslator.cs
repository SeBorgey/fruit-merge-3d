using TMPro;
using UnityEngine;

[RequireComponent (typeof(TextMeshProUGUI))]
public class TextTranslator : MonoBehaviour
{
    [SerializeField] private TranslatedText _translation;

    private TextMeshProUGUI _textMeshPro;

    private void OnEnable()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        SaveSystem.OnSave += UpdateText;
    }

    private void OnDisable()
    {
        SaveSystem.OnSave -= UpdateText;
    }

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        _textMeshPro.text = _translation.Text;
    }
}
