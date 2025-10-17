using UnityEngine;
using UnityEngine.UI;

public class LBPanel : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboard;
    [SerializeField] private GameObject _loginWarning;
    [SerializeField] private Button _loginButton;

    private void OnEnable()
    {
        //_loginWarning.gameObject.SetActive(!YandexGame.auth);
        //_leaderboard.gameObject.SetActive(YandexGame.auth);
        _loginButton.onClick.AddListener(OnLoginButtonClick);
    }

    private void OnDisable()
    {
        _loginButton.onClick?.RemoveListener(OnLoginButtonClick);
    }

    private void OnLoginButtonClick()
    {
        //
    }
}
