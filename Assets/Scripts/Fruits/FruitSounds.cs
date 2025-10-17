using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Fruit))]
public class FruitSounds : MonoBehaviour
{
    [SerializeField] private AudioClip _mergeClip;
    [SerializeField] private float[] _notes;

    private AudioSource _audioSource;
    private Fruit _fruit;

    private void OnEnable()
    {
        _fruit = GetComponent<Fruit>();
        _audioSource = GetComponent<AudioSource>();
        _fruit.OnMerged += HandleMerge;
    }

    private void OnDisable()
    {
        _fruit.OnMerged -= HandleMerge;
    }

    private void HandleMerge(int lvl)
    {
        _audioSource.pitch = _notes[lvl - 1] * 1.2f;
        _audioSource.PlayOneShot(_mergeClip);
    }
}
