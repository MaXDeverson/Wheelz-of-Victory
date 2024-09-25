using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Action PlayerTapAction;
    public Action<MenuButtonType> PlayerMenuAction { get; set; }
    public Action<int> PlayerChooseLevelAction { get;set; }
    [SerializeField] UITapController _tapController;

    private void Start()
    {
        _tapController.OnTap += () =>
        {
            PlayerTapAction?.Invoke();
        };
    }

    public void ChooseLevel(int levelIndex)
    {
        PlayerChooseLevelAction?.Invoke(levelIndex);
    }
    public void PlayMenuAction(string type)
    {
        MenuButtonType buttonType = MenuButtonType.Map;
        switch (type)
        {
            case "Exit":
                buttonType = MenuButtonType.Exit;
                break;
            case "Info":
                buttonType = MenuButtonType.Info;
                break;
            case "FAQ":
                buttonType = MenuButtonType.FAQ;
                break;
            case "Bonus":
                buttonType = MenuButtonType.Bonus;
                break;
            case "Shop":
                buttonType = MenuButtonType.Shop;
                break;
            case "Map":
                buttonType = MenuButtonType.Map;
                break;
            case "Play":
                buttonType = MenuButtonType.Play;
                break;
            case "CloseWindow":
                buttonType = MenuButtonType.CloseWindow;
                break;
            case "CloseMiniGame":
                buttonType = MenuButtonType.CloseMiniGame;
                break;
            case "CloseComingSoon":
                buttonType = MenuButtonType.ComingSoonClose;
                break;
            case "UpgradeSkill":
                buttonType = MenuButtonType.UpgradeSkill;
                break;
            case "UpgradeWeapon":
                buttonType = MenuButtonType.UpgradeWeapon;
                break;

               

        }
        PlayerMenuAction?.Invoke(buttonType);
        //Debug.Log("Play Controller:" + buttonType);
    }
    public void Play()
    {
        
    }

}

public enum MenuButtonType
{
    Map,
    Info,
    FAQ,
    Shop,
    Bonus,
    Exit,
    Question,
    Sound,
    Play,
    CloseWindow,
    CloseMiniGame,
    UpgradeSkill,
    UpgradeWeapon,
    ComingSoonClose,
}
