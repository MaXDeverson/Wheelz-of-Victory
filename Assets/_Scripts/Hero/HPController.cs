using System;
using UnityEngine;

public class HPController : MonoBehaviour
{
    public Action<int, float> ReceiveDamageAction { get; set; }
    public Action<GameObject> DieAction;
    [SerializeField] private int _startHpCount;
    private int _currentHpCount;

    private void Awake()
    {
        _currentHpCount = _startHpCount;

    }
    public void ReceiveDamage(int damageCount)
    {
        Debug.Log("ReceiveDamage");
        _currentHpCount -= damageCount;
        if (_currentHpCount <= 0)
        {
            DieAction?.Invoke(gameObject);
        }
        ReceiveDamageAction?.Invoke(damageCount, (float)_currentHpCount / _startHpCount);
    }
    public void BoostHp()
    {
        Debug.Log("Hp increased");
        _startHpCount += 5;
        _currentHpCount += 5;
    }
}
