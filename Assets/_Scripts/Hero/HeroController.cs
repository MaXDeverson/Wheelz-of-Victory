using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public Action HeroDieAction;
    [SerializeField] private HPController _hpController;
    [SerializeField] private CharacterAnimator _animator;
    void Start()
    {
        _hpController.ReceiveDamageAction += ReceiveDamageHandler;
        _hpController.DieAction += Die;
    }

    private void Die(GameObject obj)
    {
        HeroDieAction?.Invoke();
    }

    private void ReceiveDamageHandler(int count, float value)
    {
        StartCoroutine(_animator.PlayDamageAnimation());
    }
    void Update()
    {

    }
}
