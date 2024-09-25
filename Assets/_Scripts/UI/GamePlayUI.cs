using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private GameObject _enemyLineHp;
    [SerializeField] private GameObject _heroLineHp;
    [SerializeField] private Slider _enemySlider;
    [SerializeField] private Slider _heroSlider;
    [SerializeField] private List<GameObject> _weaponIcons;
    [SerializeField] private List<GameObject> _skillIcons;
    [Header("Windows")]
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _looseWindow;
    [SerializeField] private GameObject _pauseWindow;
    [SerializeField] private GameObject _fadeEffect;
    [SerializeField] private Image _winLevelImage;
    [SerializeField] private Image _looseLevelImage;
    [SerializeField] private List<Sprite> _levelSprites;
    [Header("GamePlay")]
    [SerializeField] private Text _coinsCount;
    [SerializeField] private GameObject _additionalTime;
    [Header("Drop Animation")]
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _2xCoin;
    [SerializeField] private Transform _coinAnimationPosition;
    [SerializeField] private Transform _2xCoinAnimationPosition;
    [SerializeField] private Canvas _canvas;
    private bool _is2xCoinMode;
    private Vector2 _screenSize;

    void Start()
    {
       
    }
    public void Init()
    {
        Sprite levelSprite = _levelSprites[GameManager.GetPlayerData.CurrentLevelIndex];
        _winLevelImage.sprite = levelSprite;
        _looseLevelImage.sprite = levelSprite;
        _screenSize = new Vector2(Screen.width, Screen.height);
        int currentWeaponIndex = (int)GameManager.GetPlayerData.CurrentWeapon;
        int currentSkillIndex = (int)GameManager.GetPlayerData.CurrentSkill;
        _weaponIcons[currentWeaponIndex].SetActive(true);
        _skillIcons[currentSkillIndex].SetActive(true);
        _coinsCount.text = GameManager.GetPlayerData.CoinsCount.ToString();
    }
    public void ShowUIWindow(GameplayUIWindow windowType)
    {
        switch(windowType)
        {
            case GameplayUIWindow.Win:
                _winWindow.SetActive(true);
                break;
            case GameplayUIWindow.Loose:
                _looseWindow.SetActive(true);
                break;
            case GameplayUIWindow.Pause:
                _pauseWindow.SetActive(true);
                break;
            case GameplayUIWindow.NextLevel:
                _fadeEffect.transform.SetSiblingIndex(transform.childCount - 1);
                _fadeEffect.SetActive(true);
                break;

        }
    }
    public void UpdateCoinsCount(int count)
    {
        _coinsCount.text = count + "";
    }
    public void HideWindow(GameplayUIWindow windowType)
    {
        switch (windowType)
        {
            case GameplayUIWindow.Pause:
                _pauseWindow.SetActive(false);
                break;
        }
    }
    public void DisposeAdditionalTime()
    {
        _additionalTime.SetActive(false);
    }
    public void UpdateEnemyHPValue(int count,float value)
    {
        _enemySlider.value = value;
        _enemyLineHp.SetActive(value > 0);
    }

    public  void UpdateHeroHPValue(int count, float value)
    {
        _heroSlider.value = value;
        _heroLineHp.SetActive(value > 0);
    }

    public void Turn2xCoinsMode(Vector3 position)
    {
        if (!_is2xCoinMode)
        {
            _is2xCoinMode = true;
            StartCoroutine(Play2xModeAnimation(position));
        }
       
    }
    private IEnumerator Play2xModeAnimation(Vector3 position)
    {
        Vector3 uiPosition = _camera.WorldToViewportPoint(position);
        Transform coin2X = Instantiate(_2xCoin, Vector2.zero, Quaternion.identity, _canvas.transform).transform;
        RectTransform rectTransform = coin2X.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(_screenSize.x * uiPosition.x, _screenSize.y * uiPosition.y) - (_screenSize / 2);
        coin2X.DOMove(_2xCoinAnimationPosition.position, 0.5f);
        StartCoroutine(DelayAddCoins());
        yield return new WaitForSeconds(0.7f);
        GameManager.GetPlayerData.Set2XCoinsMode(true);
    }
    public void DropCoin(int count,Vector3 position)
    {
        StartCoroutine(DelayDropCoin(count,position));
    }
    private IEnumerator DelayDropCoin(int count, Vector3 position)
    {
        for(int i = 0; i < count; i++)
        {
            Vector3 uiPosition = _camera.WorldToViewportPoint(position);
            Transform coin = Instantiate(_coin, Vector2.zero, Quaternion.identity, _canvas.transform).transform;
            RectTransform rectTransform = coin.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector2(_screenSize.x * uiPosition.x, _screenSize.y * uiPosition.y) - (_screenSize / 2);
            coin.DOMove(_coinAnimationPosition.position, 0.5f);
            StartCoroutine(DelayAddCoins());
            yield return new WaitForSeconds(0.1f);
        }

    }
    private IEnumerator DelayAddCoins()//Very bad practice ):
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.GetPlayerData.AddCoins(1);
    }
}

public enum GameplayUIWindow
{
    Win,
    Loose,
    Pause,
    NextLevel,
}
