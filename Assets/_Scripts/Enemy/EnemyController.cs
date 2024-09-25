using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyHPController _hpController;
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private EnemyMoveLogic _moveLogic;
    [SerializeField] private Drop _drop;
    [Header("Attack stats")]
    [SerializeField] private int _damage;
    [SerializeField] private float _gapTime;
    private HPController _heroHpController;
    private bool _inAttackState = false;
    void Start()
    {
        _hpController.ReceiveDamageAction += ReceiveDamageHandler;
        _hpController.DieAction += Die;
    }

    private void Die(GameObject obj)
    {
        _drop.DropCoins(transform.position);
        Destroy(gameObject);
    }

    private void ReceiveDamageHandler(int count, float value)
    {
        StartCoroutine(_animator.PlayDamageAnimation());
    }
    void Update()
    {
        
    }

    private IEnumerator AttackHero()
    {
        _heroHpController.ReceiveDamage(_damage);
        Debug.Log("Attack");
        yield return new WaitForSeconds(_gapTime);
        StartCoroutine(AttackHero());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_inAttackState) return;
        if (collision.CompareTag(Tag.Player))
        {
            _moveLogic.Move(false);
            _inAttackState = true;
            _heroHpController = collision.gameObject.GetComponent<HPController>();
            StartCoroutine(AttackHero());
            
        }
    }
}
