using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFruitsPool", menuName = "ScriptableObjects/FruitsPool", order = 1)]
public class FruitsPool : ScriptableObject
{
    [SerializeField] private List<GameObject> _fruits;

    public List<GameObject> Fruits => _fruits;
}
