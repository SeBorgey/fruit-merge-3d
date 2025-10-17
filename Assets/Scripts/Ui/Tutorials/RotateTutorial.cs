using UnityEngine;

public class RotateTutorial : TutorialStage
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _angleToComplete = 45;
    private float _rotatedAngle;

    private void OnEnable()
    {
        _playerInput.OnSwipedSideways += CountAngle;
    }

    private void OnDisable()
    {
        _playerInput.OnSwipedSideways -= CountAngle;
    }

    private void CountAngle( float angle )
    {
        _rotatedAngle += Mathf.Abs(angle);

        if (_rotatedAngle >= _angleToComplete)
        {
            _playerInput.OnSwipedSideways -= CountAngle;
            CompleteStage();
        }
    }
}
