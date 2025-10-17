using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private PlayerInput _input;

    private float _targetAngle;

    private void OnEnable()
    {
        _input.OnSwipedSideways += RotateCamera;
    }

    private void OnDisable()
    {
        _input.OnSwipedSideways -= RotateCamera;
    }

    private void RotateCamera(float angle)
    {
        _targetAngle += angle;
    }

    private void FixedUpdate()
    {
        MoveAround();
    }

    public void MoveAround()
    {
        Quaternion targetRotation = Quaternion.Euler(0, _targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 30); ;
    }
}
