using System;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public Action<int, bool, Vector3> OnDrop { get; set; }
    [SerializeField] private float _dropChanceCoin;
    [SerializeField] private float _dropChance2X;
    [SerializeField] private List<int> _coinsCount;

    public void DropCoins(Vector3 position)
    {
        if(UnityEngine.Random.Range(0f,1f) < _dropChanceCoin)
        {
            bool with2X = UnityEngine.Random.Range(0f, 1f) < _dropChance2X;

            OnDrop?.Invoke(_coinsCount[new System.Random().Next(0, _coinsCount.Count)], with2X, position);
        }
    }
}
