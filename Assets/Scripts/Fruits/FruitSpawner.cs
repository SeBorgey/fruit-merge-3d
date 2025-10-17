using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private Fruit _fruitPrefab;
    [SerializeField] private FruitsPool _fruitsPool;
    [SerializeField] private float _forceFactor = 0.25f;
    [SerializeField] private Transform _fruitHolder;
    [SerializeField] private float _throwCooldown = 0.25f;
    [SerializeField] private PlayerInput _input;

    private List<Fruit> _fruits = new();
    private int _nextFruitLevel;
    private GameObject _visualFruit;
    private Coroutine _coolDownCoroutine;

    public Action OnFruitThrow;
    public Action<int> OnFruitMerge;
    public static FruitSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        _input.OnSwipedUp += DropFruit;
        GameStateManager.OnStateChange += HandleGameStateChange;
    }

    private void OnDisable()
    {
        _input.OnSwipedUp -= DropFruit;
        GameStateManager.OnStateChange -= HandleGameStateChange;
    }

    private void Start()
    {
        SpawnVisualFruit();
    }

    private void HandleGameStateChange(GameState newState)
    {
        if (newState == GameState.Defeat)
        {
            if (_coolDownCoroutine != null)
                StopCoroutine(_coolDownCoroutine);
            StartCoroutine(HideFruit(_throwCooldown));
        }
        else if (newState == GameState.Game && GameStateManager.PreviousGameState != GameState.Pause)
        {
            SpawnVisualFruit();
        }
    }

    public void RemoveFruit(Fruit fruitToRemove)
    {
        _fruits.Remove(fruitToRemove);
        Destroy(fruitToRemove.gameObject);
    }

    public void FruitMerged(int lvl)
    {
        OnFruitMerge?.Invoke(lvl);
    }

    private void SpawnVisualFruit()
    {
        if(_visualFruit != null)
            Destroy(_visualFruit);

        _nextFruitLevel = UnityEngine.Random.Range(0, 3);
        _visualFruit = Instantiate(_fruitsPool.Fruits[_nextFruitLevel], transform);
        _visualFruit.transform.localPosition -= Vector3.up;

        if (_visualFruit.TryGetComponent(out Collider collider))
            collider.enabled = false;

        _coolDownCoroutine = StartCoroutine(Cooldown(_throwCooldown));
    }

    public void DropFruit()
    {
        if (_coolDownCoroutine != null) return;

        Fruit spawnedFruit = Instantiate(_fruitPrefab, transform.position, transform.rotation, _fruitHolder);

        spawnedFruit.FruitLevel = _nextFruitLevel;

        _fruits.Add(spawnedFruit);
        spawnedFruit.OnMerged += OnFruitMerge;

        if (spawnedFruit.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.AddRelativeForce(Vector3.forward * _forceFactor, ForceMode.VelocityChange);
        }
        OnFruitThrow?.Invoke();

        SpawnVisualFruit();
    }

    private IEnumerator Cooldown(float cooldown)
    {
        float progress = 0f;
        Vector3 startPos = _visualFruit.transform.localPosition;

        while (progress <= 1) 
        { 
            _visualFruit.transform.localPosition = Vector3.Lerp(startPos, Vector3.zero, progress);
            progress += Time.deltaTime / cooldown;
            yield return null;
        }
        _coolDownCoroutine = null;
    }

    private IEnumerator HideFruit(float cooldown)
    {
        float progress = 0f;
        Vector3 startPos = _visualFruit.transform.localPosition;

        while (progress <= 1)
        {
            _visualFruit.transform.localPosition = Vector3.Lerp(startPos, -Vector3.up, progress);
            progress += Time.deltaTime / cooldown;
            yield return null;
        }
    }
}
