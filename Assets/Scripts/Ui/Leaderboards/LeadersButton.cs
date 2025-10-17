using UnityEngine;
using UnityEngine.UI;

public class LeadersButton : MonoBehaviour
{
    [SerializeField] private Button _leadersButton;
    [SerializeField] private Button _backButton;

    private void OnEnable()
    {
        LeaderboardManager.OnEntriesLoaded += EnableLeadersButton;
        _leadersButton.onClick.AddListener(HandleLeadersButtonClick);
        _backButton.onClick.AddListener(HandleBackButtonClick);
    }

    private void OnDisable()
    {
        LeaderboardManager.OnEntriesLoaded -= EnableLeadersButton;
        _leadersButton.onClick.RemoveListener(HandleLeadersButtonClick);
        _backButton.onClick.RemoveListener(HandleBackButtonClick);
    }

    private void HandleLeadersButtonClick()
    {
        GameStateManager.SetState(GameState.Pause);
    }

    private void HandleBackButtonClick()
    {
        GameStateManager.SetState(GameState.Game);
    }

    private void EnableLeadersButton()
    {
        _leadersButton.interactable = true;
    }
}
