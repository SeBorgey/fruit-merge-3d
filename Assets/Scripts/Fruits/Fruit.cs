using System;
using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float _scaleStep = 0.25f;
    [SerializeField] private FruitsPool _fruitsPool;
    [SerializeField] private bool _applyModelOnEnable;
    [SerializeField] private float _mergeTime = 0.2f;

    private int _fruitLevel;
    private FruitSpawner _spawner;

    public Rigidbody Rigidbody { get; private set; }
    public Transform Model { get; private set; }
    public bool IsCollided { get; private set; }
    public Action<int> OnMerged;

    public int FruitLevel
    {
        get => _fruitLevel;
        set 
        { 
            _fruitLevel = value;
            UpdateFruit();
        }
    }

    private void UpdateFruit()
    {
        float levelFactor = 1f + (_fruitLevel * _scaleStep);
        Rigidbody.mass = levelFactor;
        Rigidbody.velocity = Vector3.zero;
        UpdateModel();
    }

    private void OnEnable()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _spawner = FruitSpawner.Instance;
        if (_applyModelOnEnable)
            FruitLevel = 2;
    }


    private void UpdateModel()
    {
        if (Model != null)
            Destroy(Model.gameObject);
        Model = Instantiate(_fruitsPool.Fruits[_fruitLevel], transform).transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckCollision(collision);
    }

    private void CheckCollision(Collision collision)
    {
        if (GameStateManager.CurrentGameState == GameState.Defeat) return;
        if (IsCollided) return;

        if (collision.transform.TryGetComponent(out Fruit otherFruit))
        {
            if (otherFruit.IsCollided) return;
            if (FruitLevel >= _fruitsPool.Fruits.Count - 1) return;
            if (otherFruit.FruitLevel != _fruitLevel) return;
            otherFruit.IsCollided = true;
            IsCollided = true;
            _fruitLevel++;
            StartCoroutine(MergeCoroutine(otherFruit));
        }
    }

    private IEnumerator MergeCoroutine(Fruit otherFruit)
    {
        float progress = 0f;
        otherFruit.Rigidbody.isKinematic = true;
        Transform otherModel = otherFruit.Model;
        otherModel.GetComponent<Collider>().enabled = false;

        while (progress <= 1 && otherModel != null)
        {
            progress += Time.deltaTime / _mergeTime;
            
            otherModel.position = Vector3.Lerp(otherModel.position, transform.position, progress);
            otherModel.rotation = Quaternion.Lerp(otherModel.rotation, transform.rotation, progress);

            yield return null;
        }
        IsCollided = false;
        UpdateFruit();
        OnMerged?.Invoke(FruitLevel);
        _spawner.RemoveFruit(otherFruit);
    }
}
