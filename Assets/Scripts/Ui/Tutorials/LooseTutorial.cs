using System.Collections;
using UnityEngine;

public class LooseTutorial : TutorialStage
{
    [SerializeField] private float _wait = 4f;

    private void OnEnable()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_wait);
        CompleteStage();
    }
}
