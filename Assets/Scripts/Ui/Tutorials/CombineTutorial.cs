using System.Collections;
using UnityEngine;

public class CombineTutorial : TutorialStage
{
    [SerializeField] private FruitSpawner _spawner;

    private void OnEnable()
    {
        _spawner.OnFruitMerge += CompleteStage;
    }

    private void OnDisable()
    {
        _spawner.OnFruitMerge -= CompleteStage;
    }

    private void CompleteStage(int lvl)
    {
        _spawner.OnFruitMerge -= CompleteStage;
        CompleteStage();
    }

}
