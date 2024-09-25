using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Animation _fadeAnimation;
    [SerializeField] private GameObject _tapController;
    [SerializeField] private GameObject _mainMenuScreen;
    [SerializeField] private GameObject _mapMenuScreen;
    [SerializeField] private GameObject _infoMenuScreen;
    [SerializeField] private GameObject _faqMenuScreen;
    [SerializeField] private GameObject _miniGameScreen;
    [SerializeField] private GameObject _shopScreen;
    [SerializeField] private GameObject _upgradeSkillScreen;
    [SerializeField] private GameObject _upgradeWeaponScreen;
    [SerializeField] private GameObject _bonusScreen;
    [SerializeField] AnimationEventListener _uiAnimationEventListener;
    [SerializeField] private Text _bonusTimerText;
    [SerializeField] private List<GameObject> _splashObjects;
    [Header("Sub Windows")]
    [SerializeField] private GameObject _comingSoon;
    [SerializeField] private MiniGameManager _bonusMiniGame;
    [SerializeField] private List<Animator> _cardAnimators;
    [Header("butons animation")]
    [SerializeField] private List<Transform> _levelButtons;
    [SerializeField] private float _defaultScale = 0.65f;
    [SerializeField] private float _chooseScale = 1.0f;
    private UIScreenType _nextType;
    public bool InProcess { get => _inProcess; }
    private bool _inProcess;


    private void Start()
    {
        _uiAnimationEventListener.AnimationEventAction += AnimationEventHandler;
        if (GameManager.SplashWasPlayed)
        {
            SplashSetActive(false);
            _mainMenuScreen.SetActive(true);
        }
        else
        {
            SplashSetActive(true);
            GameManager.SplashWasPlayed = true;
        }
    }

    private void SplashSetActive(bool active)
    {
        Debug.Log("Active:" + active);
        _tapController.SetActive(active);
        foreach (var item in _splashObjects)
        {
            item.SetActive(active);
        }
    }
    public void UpdateMinMapLevels(int level)
    {
        _levelButtons.ForEach(x =>
        {
            x.localScale = new Vector3(_defaultScale, _defaultScale, _defaultScale);
        });
        _levelButtons[level].localScale = new Vector3(_chooseScale, _chooseScale, _chooseScale);
       
    }
    public void ShowComingSoon(bool show)
    {
        _comingSoon.SetActive(show);
    }

    public void PlayScreen(UIScreenType type)
    {
        if (_fadeAnimation.isPlaying)
        {
            return;
        }
        _fadeAnimation.Play();
        _inProcess = true;
        _nextType = type;
    }

    private void AnimationEventHandler(string tag)
    {
        CloseAllWindow();
        switch (tag)
        {
            case "FadePeak":
                switch (_nextType)
                {
                    case UIScreenType.StartScreen:
                        _tapController.SetActive(true);
                        break;
                    case UIScreenType.MenuScreen:
                        _mainMenuScreen.SetActive(true);
                        _tapController.SetActive(false);
                        _inProcess = false;
                        break;
                    case UIScreenType.MapScreen:
                        _tapController.SetActive(false);
                        _mapMenuScreen.SetActive(true);
                        _inProcess = false;
                        break;
                    case UIScreenType.ShopScreen:
                        _tapController.SetActive(false);
                        _shopScreen.SetActive(true);
                        _inProcess = false;
                        break;
                    case UIScreenType.InfoScreen:
                        _tapController.SetActive(false);
                        _infoMenuScreen.SetActive(true);
                        _inProcess = false;
                        Debug.Log("INFO");
                        break;
                    case UIScreenType.FAQScreen:
                        _tapController.SetActive(false);
                        _faqMenuScreen.SetActive(true);
                        _inProcess = false;
                        break;
                    case UIScreenType.NextScene:
                        SceneManager.LoadScene(1);
                        break;
                    case UIScreenType.MiniGameScreen:
                        _miniGameScreen.SetActive(true);
                        _tapController.SetActive(false);
                        _inProcess = false;
                        break;
                    case UIScreenType.UpgradeSkill:
                        _upgradeSkillScreen.SetActive(true);
                        _tapController.SetActive(false);
                        _inProcess = false;
                        break;
                    case UIScreenType.UpgradeWeapon:
                        _upgradeWeaponScreen.SetActive(true);
                        _tapController.SetActive(false);
                        _inProcess = false;
                        break;
                    case UIScreenType.BonusScreen:
                        _bonusScreen.SetActive(true);
                        _inProcess = false;
                        _bonusMiniGame.SetCanPlay(GameManager.GetPlayerData.CanPalyMiniGame);
                        if (!GameManager.GetPlayerData.CanPalyMiniGame)
                        {
                            foreach (var item in _cardAnimators)
                            {
                                item.enabled = false;
                                item.gameObject.SetActive(true);
                            }
                        }
                        (int, int) time = GameManager.GetPlayerData.TimeToReward();
                        _bonusTimerText.text = string.Format("{0:00}:{1:00}", time.Item1, time.Item2);
                        break;
                }
             break;
        }
    }

    private void CloseAllWindow()
    {
        _mainMenuScreen.SetActive(false);
        _mapMenuScreen.SetActive(false);
        _infoMenuScreen.SetActive(false);
        _faqMenuScreen.SetActive(false);
        _miniGameScreen.SetActive(false);
        _shopScreen.SetActive(false);
        _upgradeSkillScreen.SetActive(false);
        _upgradeWeaponScreen.SetActive(false);
        _bonusScreen.SetActive(false);
    }

    private void PlayMenuScreen()
    {

    }

}

public enum UIScreenType
{
    StartScreen,
    MenuScreen,
    MapScreen,
    ShopScreen,
    FAQScreen,
    InfoScreen, 
    NextScene,
    MiniGameScreen,
    UpgradeSkill,
    UpgradeWeapon,
    BonusScreen,
}
