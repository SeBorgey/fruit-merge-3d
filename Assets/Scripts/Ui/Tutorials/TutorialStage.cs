using System;
using UnityEngine;

[RequireComponent(typeof(UIPanelFader))]
public abstract class TutorialStage : MonoBehaviour
{
    public Action<TutorialStage> OnCompleted;
    private UIPanelFader _fader;

    public void ShowStage(bool show)
    {
        if(_fader == null)
            _fader = GetComponent<UIPanelFader>();

        _fader.FadePanel(show);
    }

    protected void CompleteStage()
    {
        OnCompleted?.Invoke(this);
    }
}
