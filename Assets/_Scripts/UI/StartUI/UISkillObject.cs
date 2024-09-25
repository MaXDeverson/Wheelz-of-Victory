using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillObject : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] private SkillType _skillType;
    [SerializeField] private int _objectCost;
    [SerializeField] private bool _isBought;
    [SerializeField] private bool _isUsed;

    public bool IsBought => _isBought;
    public bool IsUsed => _isUsed;
    public int Price => _objectCost;
    public bool AffordAble(int coinsCount)
    {
        return (coinsCount - _objectCost) >= 0;
    }
    public SkillType Type => _skillType;
    public void Buy()
    {
        _isBought = true;
    }
    public void SetUse(bool isUse)
    {
        _isUsed = isUse;
    }
    public void HideObj()
    {
        //gameObject.SetActive(false);
        _animator.SetInteger("state", 1);
    }
    public void ShowObj()
    {
        _animator.SetInteger("state", 0);
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void ShowPrevious()
    {

        gameObject.SetActive(false);
        gameObject.SetActive(true);
        _animator.SetInteger("state", -1);
    }
    public void HidePrevious()
    {
        _animator.SetInteger("state", 2);
    }
}
