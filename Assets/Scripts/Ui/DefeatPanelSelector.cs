using UnityEngine;

public class DefeatPanelSelector : MonoBehaviour
{
    [SerializeField] private DefeatPanel _resultsPanel;

    private void OnEnable()
    {
        _resultsPanel.GetComponent<UIPanelFader>().FadePanel(true);
    }
}
