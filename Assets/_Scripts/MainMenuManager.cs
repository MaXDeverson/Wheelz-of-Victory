using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] InputController _inputController;
    [SerializeField] UIController _uiController;
    private UIScreenType _currentState = UIScreenType.StartScreen;

    private int _currentLevel = 0;
    private void Awake()
    {
        Time.timeScale = 1f;
       // PlayerPrefs.DeleteAll();
        if (GameManager.GetPlayerData.CanPalyMiniGame)
        {
           _currentState = UIScreenType.MiniGameScreen;
        }
    }
    void Start()
    {
        _inputController.PlayerMenuAction += UserMenuActionHandler;
        _inputController.PlayerTapAction += UserTapHandler;
        _inputController.PlayerChooseLevelAction += UserLevelChooseHandler;
    }
    
    private void UserLevelChooseHandler(int level)
    {
        _uiController.UpdateMinMapLevels(level);
        _currentLevel = level;
    }
    private void UserMenuActionHandler(MenuButtonType type)
    {
        if (_uiController.InProcess)
        {
            return;
        }
        switch (type)
        {
            case MenuButtonType.Map:
                _uiController.PlayScreen(UIScreenType.MapScreen);
                break;
            case MenuButtonType.Play:
                // SceneManager.LoadScene(1);
                if (_currentLevel < 4) {
                    _uiController.PlayScreen(UIScreenType.NextScene);
                    GameManager.GetPlayerData.SetCurrentLevel(_currentLevel);
                }
                else
                {
                    _uiController.ShowComingSoon(true);
                }
                break;
            case MenuButtonType.CloseWindow:
                _uiController.PlayScreen(UIScreenType.MenuScreen);
                break;
            case MenuButtonType.Info:
                _uiController.PlayScreen(UIScreenType.InfoScreen);
                break;
            case MenuButtonType.Exit:
                Application.Quit();
                break;
            case MenuButtonType.FAQ:
                _uiController.PlayScreen(UIScreenType.FAQScreen);
                break;
            case MenuButtonType.CloseMiniGame:
                _uiController.PlayScreen(UIScreenType.MenuScreen);
                break;
            case MenuButtonType.Shop:
                _uiController.PlayScreen(UIScreenType.ShopScreen);
                break;
            case MenuButtonType.UpgradeSkill:
                _uiController.PlayScreen(UIScreenType.UpgradeSkill);
                break;
            case MenuButtonType.UpgradeWeapon:
                _uiController.PlayScreen(UIScreenType.UpgradeWeapon);
                break;
            case MenuButtonType.Bonus:
                _uiController.PlayScreen(UIScreenType.BonusScreen);
                break;
            case MenuButtonType.ComingSoonClose:
                _uiController.ShowComingSoon(false);
                break;
        }
       
    }
    private void UserTapHandler()
    {
        if (_uiController.InProcess)
        {
            return;
        }
        switch (_currentState) 
        {
            case UIScreenType.StartScreen:
                _uiController.PlayScreen(UIScreenType.MenuScreen);
                break;
            case UIScreenType.MiniGameScreen:
                _uiController.PlayScreen(UIScreenType.MiniGameScreen);
                break;
        }
    }

    private void OnDestroy()
    {
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
