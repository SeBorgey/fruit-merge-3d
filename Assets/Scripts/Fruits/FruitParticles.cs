using UnityEngine;

[RequireComponent (typeof(Fruit))]
public class FruitParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _mergeParticles;

    private Fruit _fruit;

    private void OnEnable()
    {
        _fruit = GetComponent<Fruit>();

        _fruit.OnMerged += HandleMerge;
    }

    private void OnDisable()
    {
        _fruit.OnMerged -= HandleMerge;
    }

    private void HandleMerge(int lvl)
    {
        Instantiate(_mergeParticles, transform.position, Quaternion.identity);
    }
}
