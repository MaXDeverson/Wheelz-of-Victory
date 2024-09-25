using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    public Action WinAction;
    public Action LooseAction;
    [SerializeField] private List<LevelData> _levels;
    [SerializeField] GamePlayUI _ui;
    [SerializeField] AnimationEventListener _fadeEffectEvent;
    [SerializeField] Timer _timer;
    [SerializeField] private int _setCurrentLevel = -1;
    [Header("Hero")]
    [SerializeField] WeaponController _weaponController;
    [SerializeField] HeroController _heroController;
    [SerializeField] HPController _heroHpController;
    
    private LevelData _currentLevel;
    private int _currentLevelIndex;
    private bool _gameFinish;
    private void Awake()
    {
        GamePlayInit();
        UIInit();
        
    }
    private void Start()
    {
    

    }
    private void GamePlayInit()
    {
        PlayerData playerData = GameManager.GetPlayerData;
        _currentLevelIndex = _setCurrentLevel>=0? _setCurrentLevel: playerData.CurrentLevelIndex;
        Debug.Log("Current Player level:" + playerData.CurrentLevelIndex);
        _currentLevel = _levels[_currentLevelIndex];
        _currentLevel.gameObject.SetActive(true);
        _timer.SetTime(_currentLevel.LevelTime);
        _currentLevel.OnEnemySpawnAction += (Transform enemy) =>
        {
            if (_gameFinish)
            {
                _currentLevel.StopAllEnemies();
                _currentLevel.StopAllCoroutines();
                _weaponController.StopAllCoroutines();
                return;
            }
            _weaponController.AddEnemyInQueue(enemy);
            enemy.GetComponent<Drop>().OnDrop += (int coins, bool is2x, Vector3 position) =>
            {
                _ui.DropCoin(coins, position);
                if (is2x)
                {
                    _ui.Turn2xCoinsMode(position);
                }
            };
        };
        _timer.TimeElapsedAction += () =>
        {
            WinAction?.Invoke();

        };
        _weaponController.EnemyDie += (x) =>
        {
            if (_gameFinish) return;
            _timer.AddTime(3);
        };
        _weaponController.PrepareObject(); 
        HeroStatsInit();
    }
    private void HeroStatsInit()
    {
        Debug.Log("Heero init current skill:" + GameManager.GetPlayerData.CurrentSkill);
        switch (GameManager.GetPlayerData.CurrentSkill)
        {
            case SkillType.None:
                break;
            case SkillType.Health:
                _heroHpController.BoostHp();
                break;
            case SkillType.AttackSpeed:
                _weaponController.BoostSpeed();
                break;
        }
    }

    private void UIInit()
    {
        _ui.Init();
        GameManager.GetPlayerData.ChangeCoinsCountAction += _ui.UpdateCoinsCount; 
        _weaponController.CurrentEnemyHPChange += (count, value) =>
        {
            _ui.UpdateEnemyHPValue(count,value);
        };
        _heroHpController.ReceiveDamageAction += (count, value) =>
        {
            _ui.UpdateHeroHPValue(count, value);
        };
        WinAction += () =>
        {
            _ui.ShowUIWindow(GameplayUIWindow.Win);
            _currentLevel.StopAllEnemies();
            _currentLevel.StopAllCoroutines();
            _weaponController.StopAllCoroutines();
            _gameFinish = true;
        };
        LooseAction += () =>
        {
            _gameFinish = true;
            _currentLevel.StopAllEnemies();
            _currentLevel.StopAllCoroutines();
            _weaponController.StopAllCoroutines();
        };
        //_weaponController.AllEnemiesDie += () =>
        //{
        //    _ui.ShowUIWindow(GameplayUIWindow.Win);
        //};
        _fadeEffectEvent.AnimationEventAction += (str) =>
        {
            SceneManager.LoadScene(0);
        };
        _heroController.HeroDieAction += () =>
        {
            LooseAction?.Invoke();
            _ui.ShowUIWindow(GameplayUIWindow.Loose);
        };
        GameManager.GetPlayerData.ExpireCurrentSkill();

    }

    public void UICommandHandler(string tag)
    {
        switch (tag)
        {
            case "close":
                Time.timeScale = 1;
                _ui.ShowUIWindow(GameplayUIWindow.NextLevel);
                break;
            case "pause":
                _ui.ShowUIWindow(GameplayUIWindow.Pause);
                Time.timeScale = 0;
                break;
            case "pause continue":
                Time.timeScale = 1;
                _ui.HideWindow(GameplayUIWindow.Pause);
                break;
            case "addTime":
                _timer.AddTime(15);
                _ui.DisposeAdditionalTime();
                break;
        }
    }

    private void OnDestroy()
    {
        GameManager.GetPlayerData.ChangeCoinsCountAction -= _ui.UpdateCoinsCount;
        GameManager.SaveState();
    }

    private void OnApplicationQuit()
    {
        GameManager.SaveState();
    }
    private void OnApplicationFocus(bool focus)
    {
        GameManager.SaveState();
    }
}
