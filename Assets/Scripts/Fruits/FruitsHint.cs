using UnityEngine;

public class FruitsHint : MonoBehaviour
{
    [SerializeField] private FruitsPool _fruitsPool;
    [SerializeField] private Transform _container;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private LayerMask _layerMask;

    private void OnEnable()
    {
        SpawnVisualFruits();
    }

    private void SpawnVisualFruits()
    {
        for (int i = 0; i < _fruitsPool.Fruits.Count; i++)
        {
            GameObject fruit = Instantiate(_fruitsPool.Fruits[i], _container.position, Quaternion.identity, _container);
            fruit.GetComponent<Collider>().enabled = false;
            fruit.transform.localPosition = _offset * i;
            fruit.layer = 5;
        }
    }
}
