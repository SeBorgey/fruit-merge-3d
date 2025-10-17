using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIPanelFader _startPanel;
    [SerializeField] private UIPanelFader _gameoverPanel;
    [SerializeField] private UIPanelFader _finishPanel;
    [SerializeField] private UIPanelFader _inGameUi;
    [SerializeField] private UIPanelFader _leadersPanel;

    private UIPanelFader _activePannel;
    private List<UIPanelFader> _unactivePanels;

    private void OnEnable() => GameStateManager.OnStateChange += ManagePanels;

    private void OnDisable() => GameStateManager.OnStateChange -= ManagePanels;

    private void Awake()
    {
        _unactivePanels = new List<UIPanelFader> { _startPanel, _gameoverPanel, _finishPanel, _inGameUi};
        HideAllPanels();
    }

    private void ManagePanels(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                SetActivePannel(_startPanel);
                break;
            case GameState.Game:
                SetActivePannel(_inGameUi);
                break;
            case GameState.Defeat:
                SetActivePannel(_gameoverPanel);
                break;
            case GameState.Finish:
                SetActivePannel(_finishPanel);
                break;
            case GameState.Pause:
                SetActivePannel(_leadersPanel);
                break;
        }
    }

    private void SetActivePannel(UIPanelFader panel)
    {
        if(panel == null)
        {
            Debug.LogWarning("No such pannel !");
            return;
        }

        DisableActivePanel();
        _unactivePanels.Remove(panel);
        _activePannel = panel;
        panel.gameObject.SetActive(true);
        panel.FadePanel(true);
    }

    private void HideAllPanels()
    {
        foreach (UIPanelFader panel in _unactivePanels)
            if(panel != null) panel.gameObject.SetActive(false);
    }

    private void DisableActivePanel()
    {
        if (_activePannel == null) return;
        _unactivePanels.Add(_activePannel);
        _activePannel.FadePanel(false);
        _activePannel = null;
    }
}
