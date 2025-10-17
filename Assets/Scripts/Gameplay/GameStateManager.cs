using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameState _defaultState;

    public static GameState CurrentGameState { get; private set; }
    public static GameState PreviousGameState { get; private set; }

    public static event Action<GameState> OnStateChange;

    void Start()
    {
        CurrentGameState = _defaultState;
        SetState(_defaultState);
    }

    public static void SetState(GameState state)
    {
        PreviousGameState = CurrentGameState;
        CurrentGameState = state;
        Debug.Log("State changed to -> " + state);
        OnStateChange?.Invoke(state);
    }
}

public enum GameState
{
    Menu = 0,
    Game = 1,
    Defeat = 2,
    Finish = 3,
    Pause = 4
}
