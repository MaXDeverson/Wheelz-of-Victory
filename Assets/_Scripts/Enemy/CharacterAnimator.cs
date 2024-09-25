using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PlayDamageAnimation()
    {
        _animator.SetBool("Damage", true);
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("Damage", false);
    }
}
