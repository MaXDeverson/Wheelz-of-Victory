using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    public Action<int, float> ReceiveDamageAction { get; set; }
    public Action<GameObject> DieAction;
    [SerializeField] private int _startHpCount;
    [SerializeField] private bool _isInvulnerable;
    private int _currentHpCount;

    private void Awake()
    {
        _currentHpCount = _startHpCount;

    }
    void Start()
    {
        //_currentHpCount = _startHpCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_isInvulnerable) { return; }
        if (other.CompareTag(Tag.DamageObj))
        {
            ReceiveDamage(other.gameObject.GetComponent<DamageObject>().GetDamageCount());
        }
    }

    private void ReceiveDamage(int damageCount)
    {
        _currentHpCount -= damageCount;
        if(_currentHpCount <= 0)
        {
            DieAction?.Invoke(gameObject);
        }
        ReceiveDamageAction?.Invoke(damageCount, (float)_currentHpCount / _startHpCount);
    }
}

public static class Tag
{
    public static string DamageObj = "DamageObj";
    public static string Enemy = "Enemy";
    public static string Player = "Player";
}