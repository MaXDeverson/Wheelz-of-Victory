using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField] private int _damageCount;
    public int GetDamageCount() => _damageCount;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if( collision.CompareTag(Tag.Enemy))
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
