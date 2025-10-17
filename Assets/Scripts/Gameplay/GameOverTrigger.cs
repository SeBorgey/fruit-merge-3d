using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.TryGetComponent(out Fruit otherFruit))
        {
            if (GameStateManager.CurrentGameState == GameState.Defeat) return;
            Debug.Log("Fruit has fallen ! " + otherFruit.Model.name);
            GameStateManager.SetState(GameState.Defeat);
        }
    }
}
