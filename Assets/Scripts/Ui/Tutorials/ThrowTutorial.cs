using UnityEngine;

public class ThrowTutorial : TutorialStage
{
    [SerializeField] private PlayerInput _playerInput;

    private void OnEnable()
    {
        _playerInput.OnSwipedUp += CompleteStage;
    }

    private void OnDisable()
    {
        _playerInput.OnSwipedUp -= CompleteStage;
    }
}
