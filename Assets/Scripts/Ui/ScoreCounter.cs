using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private CounterAnimation _animation;
    [SerializeField] private FruitSpawner _spawner;

    private GameManager _gameManager;

    private void OnEnable()
    { 
        _gameManager = GameManager.Instance;
        _spawner.OnFruitMerge += HandleFruitMerge;
        _counterText.text = _gameManager.CurrentScore.ToString();
    }

    private void OnDisable()
    {
        _spawner.OnFruitMerge -= HandleFruitMerge;
    }

    private void HandleFruitMerge(int lvl)
    {
        if (GameStateManager.CurrentGameState == GameState.Defeat) return;
        _gameManager.CurrentScore += lvl;
        _counterText.text = _gameManager.CurrentScore.ToString();
        _animation.Animate();
    }
}
