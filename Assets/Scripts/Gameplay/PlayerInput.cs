using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _swipeStraightness = 0.7f;
    [SerializeField] private float _keyboardRotationSpeed = 120f;

    private Vector3 _mouseDeltaPos;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private float _swipeTimer;

    public float Sensitivity;
    public Action OnSwipedUp;
    public Action<float> OnSwipedSideways;
    public bool EnableInput = true;

    private void Start()
    {
        _mouseDeltaPos = Input.mousePosition;
    }

    void Update()
    {
        if (GameStateManager.CurrentGameState == GameState.Defeat)
        {
            OnSwipedSideways?.Invoke(-45f * Time.deltaTime);
            return;
        }
        else if (GameStateManager.CurrentGameState != GameState.Game) return;
        if(EnableInput)
        {
            HandleKeyboardInput();
            HandleMouseInput();
        }
    }

    private void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnSwipedUp?.Invoke();

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            OnSwipedSideways?.Invoke(-_keyboardRotationSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            OnSwipedSideways?.Invoke(_keyboardRotationSpeed * Time.deltaTime);
    }

    private void HandleMouseInput()
    {
        Vector3 swipeVector = Input.mousePosition - _startPos;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButton(0))
        {
            _mouseDeltaPos = Input.mousePosition - _endPos;
            _swipeTimer += Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                _mouseDeltaPos = Vector3.zero;
                _startPos = Input.mousePosition;
            }

            if (_mouseDeltaPos.x != 0 && _mouseDeltaPos.y <= 3f)
            {
                float screenWidth = Screen.width;
                float aspectRatio = screenWidth / Screen.height;
                float newAngle = (_mouseDeltaPos.x / Screen.width) * 360f * Sensitivity * aspectRatio;

                OnSwipedSideways?.Invoke(newAngle);
            }

        }
        _endPos = Input.mousePosition;
        
        if (Input.GetMouseButtonUp(0))
        {
            if (swipeVector.y > 40f &&swipeVector.normalized.y > _swipeStraightness && _swipeTimer < 1f)
                OnSwipedUp?.Invoke();
            
            _swipeTimer = 0f;
        }
    }
}
