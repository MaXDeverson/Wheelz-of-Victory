
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public Action<int> ChangeCoinsCountAction;
    public int CurrentLevelIndex { get => _currentLevel; }
    public int CoinsCount { get => _coinsCount; }
    public WeaponType CurrentWeapon { get => _currentWeaponType; }
    public SkillType CurrentSkill { get => _currentSkill; }
    public (int,int) TimeToReward()
    {
        TimeSpan d = DateTime.Now - _lastRewardTime;
        if(d.Days> 0)
        {
            return (0, 0);
        }
        else
        {
            Debug.Log("Get Time Game" + d.ToString() + "minutes:" + d.TotalMinutes);
            return (23 - d.Hours, 59 - d.Minutes);
        }
    }
    public bool CanPalyMiniGame { get {
            return true;
            TimeSpan d = DateTime.Now - _lastRewardTime;
            if (d.Days > 0)
            {
                RecordPlayGameTime();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public List<WeaponType> BoughtWeapon { get => _boughtWeapon; }
    public DateTime LastRewardTime { get => _lastRewardTime; }
    public List<SkillType> BoughtSkill { get => _boughSkill; }
    private List<WeaponType> _boughtWeapon = new List<WeaponType>() { WeaponType.Axe};
    private List<SkillType> _boughSkill = new List<SkillType>();
    private int _coinsCount = 200;
    private DateTime _lastRewardTime = DateTime.MinValue;
    private int _currentLevel;
    private WeaponType _currentWeaponType = WeaponType.Axe;
    private SkillType _currentSkill = SkillType.None;
    private bool _2xCoinsMode;

    public PlayerData(int coinsCount, WeaponType weaponType, SkillType skillType, List<WeaponType> boughtWeapon, List<SkillType> boughtSkill, DateTime lastRewardTime)
    {
        _coinsCount = coinsCount;
        _currentWeaponType = weaponType;
        _currentSkill = skillType;

        _boughtWeapon = boughtWeapon;
        _boughSkill = boughtSkill;
        _lastRewardTime = lastRewardTime;
    }
    public void AddBoughtWeapon(WeaponType type)
    {
        if (!_boughtWeapon.Contains(type))
        {
            _boughtWeapon.Add(type);
        }
    }
    public void AddBoughtSkill(SkillType type)
    {
        if (!_boughSkill.Contains(type))
        {
            _boughSkill.Add(type);
        }
    }
    public void ExpireCurrentSkill()
    {
        _boughSkill.Remove(_currentSkill);
        _currentSkill = SkillType.None;
      
    }
    public void RecordPlayGameTime()
    {
        _lastRewardTime = DateTime.Now;
    }
    public void SetCurrentLevel(int levelIndex)
    {
        _currentLevel= levelIndex;
    }
    public void Set2XCoinsMode(bool enabled)
    {
        _2xCoinsMode = enabled;
    }
    public PlayerData()
    {
        //Debug.Log("Create New Player Data");
    }
    public void SetCurrentWeapon(WeaponType weapon)
    {
        _currentWeaponType = weapon;
    }
    public void SetCurrentSkill(SkillType skill)
    {
        _currentSkill = skill;
    }
    public void AddCoins(int amount)
    {
        _coinsCount += _2xCoinsMode? amount * 2: amount;
        ChangeCoinsCountAction?.Invoke(_coinsCount);
    }
    public bool SpendCoins(int amount)
    {
        if(_coinsCount < amount)
        {
            return false;
        }
        else
        {
            _coinsCount-= amount;
            return true;
        }
     
    }
}

public enum WeaponType 
{ 
    Axe,
    Bow,
    CrossBow,
    Spear,
    SpearUpgraded,
    SpearTop,
}

public enum SkillType
{
    None,
    Health,
    AttackSpeed,
}

