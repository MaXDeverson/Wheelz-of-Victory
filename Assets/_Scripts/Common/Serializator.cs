using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Serializator
{
    private static string[] fieldsNames = { "CoinsCount", "CurrentWeapon","CurrentSkill","BoughtWeapon","BoughtSkill","LastRewardTime" };
    public static void SavePlayerData(PlayerData playerData)
    {
        PlayerPrefs.SetInt(fieldsNames[(int)FieldsType.CoinsCount], playerData.CoinsCount);
        PlayerPrefs.SetInt(fieldsNames[(int)FieldsType.CurrentWeapon],(int) playerData.CurrentWeapon);
        PlayerPrefs.SetInt(fieldsNames[(int)FieldsType.CurrentSkill],(int) playerData.CurrentSkill);

        List<int> weaponByInt = new List<int>();
        for(int i = 0;i< playerData.BoughtWeapon.Count; i++)
        {
            weaponByInt.Add((int)playerData.BoughtWeapon[i]);
        }
        List<int> skillByInt = new List<int>();
        for(int i = 0; i< playerData.BoughtSkill.Count; i++)
        {
            skillByInt.Add((int)playerData.BoughtSkill[i]);
        }
        PlayerPrefs.SetString(fieldsNames[(int)FieldsType.BoughtWeapon], GetSaveString(weaponByInt));
        PlayerPrefs.SetString(fieldsNames[(int)FieldsType.BoughtSkill], GetSaveString(skillByInt));
        PlayerPrefs.SetString(fieldsNames[(int)FieldsType.LastRewardTime], playerData.LastRewardTime.ToString());
    }
    
    public static PlayerData GetPlayerData()
    {
        int coinsCount =  PlayerPrefs.GetInt(fieldsNames[(int)FieldsType.CoinsCount]);
        WeaponType weapon = (WeaponType)PlayerPrefs.GetInt(fieldsNames[(int)FieldsType.CurrentWeapon]);
        SkillType skill = (SkillType)PlayerPrefs.GetInt(fieldsNames[(int)FieldsType.CurrentSkill]);
        DateTime lastRewardTime = DateTime.MinValue;
        try
        {
            lastRewardTime = DateTime.Parse(PlayerPrefs.GetString(fieldsNames[(int)FieldsType.LastRewardTime]));
        }
        catch
        {
            
        }
        

        string weaponString = PlayerPrefs.GetString(fieldsNames[(int)FieldsType.BoughtWeapon]);
        List<int> weaponByInt = GetSaveList(PlayerPrefs.GetString(fieldsNames[(int)FieldsType.BoughtWeapon]));
        List<int> skillByInt = GetSaveList(PlayerPrefs.GetString(fieldsNames[(int)FieldsType.BoughtSkill]));
        List<WeaponType> boughWeapon = new List<WeaponType>();
        List<SkillType> boughtSkill = new List<SkillType>();
        for(int i = 0;i< weaponByInt.Count; i++)
        {
            boughWeapon.Add((WeaponType)weaponByInt[i]);
        }
        for (int i = 0; i < skillByInt.Count; i++)
        {
            boughtSkill.Add((SkillType)skillByInt[i]);
        }
        if (boughWeapon.Count == 0)
        {
            boughWeapon = new List<WeaponType>() { WeaponType.Axe };
        }
        PlayerData playerData = new PlayerData(coinsCount, weapon,skill, boughWeapon, boughtSkill, lastRewardTime);
        return playerData;
    }
    private static string GetSaveString(List<int> array)
    {
        string res = string.Empty;
        foreach (int i in array)
        {
            res += i.ToString() + "|";
        }
        return res;
    }

    private static List<int> GetSaveList(string array)
    {
        List<int> list = new List<int>();
        string[] splited = array.Split('|');
        for(int i = 0; i < splited.Length - 1; i++)
        {
            string s = splited[i];
            if (int.TryParse(s, out int value))
            {
                list.Add(value);
            }
            else
            {
                return new List<int>();
            }
        }
        return list;
    }
    private enum FieldsType
    {
        CoinsCount,
        CurrentWeapon,
        CurrentSkill,

        BoughtWeapon,
        BoughtSkill,
        LastRewardTime,

    }
}
