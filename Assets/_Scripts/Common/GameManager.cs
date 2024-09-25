

using System.Diagnostics;
using UnityEngine;

public class GameManager
{
    public static bool SplashWasPlayed
    {
        get
        {
            return _splashWasPlayed;
        }
        set { _splashWasPlayed = value; }
    }
    private static bool _splashWasPlayed;

    public static PlayerData GetPlayerData => _playerData;

    private static PlayerData _playerData;

    static GameManager()
    {
        if(_playerData == null)
        {
            _playerData = Serializator.GetPlayerData();
        }
    }

    public static void SaveState()
    {
        Serializator.SavePlayerData( _playerData);
        //PlayerPrefs.DeleteAll();
    }
}
