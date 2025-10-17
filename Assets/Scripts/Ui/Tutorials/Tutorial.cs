using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private bool ShowTutorialOnlyInFirstGame = false;
    [SerializeField] private List<TutorialStage> _stages;

    private void OnEnable()
    {
        foreach (var stage in _stages)
        {
            stage.OnCompleted += OnStageCompleted;
        }
    }

    private void OnDisable()
    {
        foreach (var stage in _stages)
        {
            stage.OnCompleted -= OnStageCompleted;
        }
    }

    private void Start()
    {
        if (SaveSystem.SavesData.WasTutorialEnded && ShowTutorialOnlyInFirstGame)
        {
            Debug.Log("Tutorial was completed");
            return;
        }

        if (_stages.Count < 1)
        {
            Debug.Log("No tutorial stages to show !");
            return;
        }

        _stages[0].ShowStage(true);
    }

    private void OnStageCompleted(TutorialStage stage)
    {
        stage.ShowStage(false);
        int indexOfStage = _stages.IndexOf(stage);
        stage.OnCompleted -= OnStageCompleted;
        if (indexOfStage >= _stages.Count - 1)
        {
            CompleteTutorial();
            return;
        }
        _stages[indexOfStage + 1].ShowStage(true);
    }

    private void CompleteTutorial()
    {
        SaveSystem.SavesData.WasTutorialEnded = true;
        SaveSystem.Save();
    }
}

