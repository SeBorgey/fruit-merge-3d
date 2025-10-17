using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(FruitSpawner))]
public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip _throwClip;

    private AudioSource _audioSource;
    private FruitSpawner _fruitSpawner;

    private void OnEnable()
    {
        _fruitSpawner = GetComponent<FruitSpawner>();
        _audioSource = GetComponent<AudioSource>();
        _fruitSpawner.OnFruitThrow += HandleThrow;
    }

    private void OnDisable()
    {
        _fruitSpawner.OnFruitThrow -= HandleThrow;
    }

    private void HandleThrow()
    {
        _audioSource.PlayOneShot(_throwClip);
    }
}
