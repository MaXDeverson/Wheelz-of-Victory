using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Action AllEnemiesDie;
    public Action<GameObject> EnemyDie;
    public Action<int, float> CurrentEnemyHPChange;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private int _setCurrentWeaponIndex = -1;
    [SerializeField] private Transform _startPoint;
    private List<Transform> _enemies = new List<Transform>();
    private Weapon _currentWeapon;
    private bool _active = true;
    private int _currentWeaponIndex;

    private Transform _currentEnemyTarget;
    private int _currentEnemyIndex = 0;

    public void BoostSpeed()
    {
        _currentWeapon.IncreaseAttackSpeed(2f);
    }
    public void AddEnemyInQueue(Transform enemy)
    {
        _enemies.Add(enemy);
        enemy.GetComponent<EnemyHPController>().DieAction += (obj) =>
        {
            EnemyDie?.Invoke(obj);
        };
        if (_currentEnemyTarget == null)
        {
            CurrentEnemyInit();
            StopAllCoroutines();
            StartCoroutine(UseWeapon(_currentWeapon.UseRate));
            _active = true;

        }

    }
    public void PrepareObject()
    {
        _currentWeaponIndex = _setCurrentWeaponIndex>=0? _setCurrentWeaponIndex: (int)GameManager.GetPlayerData.CurrentWeapon;
        Debug.Log("Current Weapon:" + GameManager.GetPlayerData.CurrentWeapon + "index:" + _currentWeaponIndex);
        _currentWeapon = _weapons[_currentWeaponIndex];
        
    }
    void Start()
    {
        if(_currentEnemyTarget != null)
        {
            StartCoroutine(UseWeapon(_currentWeapon.UseRate));
        }
    }

    private IEnumerator UseWeapon(float gapTime)
    {
        yield return new WaitForSeconds(gapTime);
        if (_active)
        {
            GameObject damageObj = Instantiate(_currentWeapon.GetDamageObj().gameObject, _startPoint.position, Quaternion.identity);
            Vector3 direction = CalculateShootingDirection();
            damageObj.GetComponent<Rigidbody2D>().velocity = direction.normalized * _currentWeapon.FlyingSpeed;
            float angle = Mathf.Atan(direction.x/ direction.y) * Mathf.Rad2Deg;
            if(direction.y < 0)
            {
                angle += 180;
            }
            damageObj.transform.eulerAngles = new Vector3(0,0, -angle );
            StartCoroutine(UseWeapon(_currentWeapon.UseRate));
        }

    }

    private void CurrentEnemyDieHandler(GameObject corpse)
    {
        _currentEnemyIndex++;
        if(_currentEnemyIndex >= _enemies.Count)
        {
           // AllEnemiesDie?.Invoke();
            _active = false;
        }
        else
        {
            CurrentEnemyInit();
        }

    }

    private void CurrentEnemyInit()
    {
        _currentEnemyTarget = _enemies[_currentEnemyIndex];
        _currentEnemyTarget.GetComponent<EnemyHPController>().DieAction += CurrentEnemyDieHandler;
        _currentEnemyTarget.GetComponent<EnemyHPController>().ReceiveDamageAction += (x, y) =>
        {
            CurrentEnemyHPChange?.Invoke(x, y);
        };
    }
    private Vector2 CalculateShootingDirection()
    {
        return _currentEnemyTarget.position - _startPoint.position;
    }
}
