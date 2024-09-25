using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] private Transform _damageObj;
    [SerializeField] private float _flyingSpeed;
    [SerializeField] private float _useRate;

    public float FlyingSpeed => _flyingSpeed;
    public float UseRate => _useRate;
    public Transform GetDamageObj() => _damageObj;
    void Start()
    {
        
    }
    public void IncreaseAttackSpeed(float value)
    {
        Debug.Log("Attack Speed Increased");
        _useRate /= value;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
