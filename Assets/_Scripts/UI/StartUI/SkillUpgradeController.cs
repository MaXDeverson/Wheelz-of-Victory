using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeController : MonoBehaviour
{
    [SerializeField] private List<UISkillObject> _skillObjects;
    private UISkillObject _currentSkillObject { get => _skillObjects[_currentWeaponObjectIndex]; }
    private int _currentWeaponObjectIndex = 0;
    [Header("UI elements")]
    [SerializeField] GameObject _buyButton;
    [SerializeField] GameObject _useButton;
    [SerializeField] Text _priceText;
    [SerializeField] Text _currentBalanceText;
    [SerializeField] GameObject _currentPriceView;
    [SerializeField] GameObject _noEnoughMoneyScreen;
    // Update is called once per frame
    private void Start()
    {

    }
    private void OnEnable()
    {
        for(int i = 0;i < _skillObjects.Count;i++)
        {
            if (GameManager.GetPlayerData.BoughtSkill.Contains(_skillObjects[i].Type))
            {
                _skillObjects[i].Buy();
            }
            if (_skillObjects[i].Type == GameManager.GetPlayerData.CurrentSkill)
            {
                _skillObjects[i].SetUse(true);
            }
        }

        _skillObjects.ForEach(x => x.gameObject.SetActive(false));
        UpdateUI(_currentSkillObject);
        _currentSkillObject.ShowObj();
        _currentBalanceText.text = GameManager.GetPlayerData.CoinsCount + "";
    }
    public void UserActionHandler(string actionTag)
    {
        switch (actionTag)
        {
            case "next":
                NextObject();
                break;
            case "previous":
                PreviousObject();
                break;
            case "buy":
                TryBuyObject();
                break;
            case "use":
                UseObject();
                break;
            case "closeNoEnoughMoney":
                _noEnoughMoneyScreen.SetActive(false);
                break;
        }
    }
    private void UpdateUI(UISkillObject obj)
    {
        _buyButton.SetActive(!obj.IsBought);
        _useButton.SetActive(obj.IsBought && !obj.IsUsed);
        _priceText.text = obj.Price + "";
        _currentPriceView.SetActive(!obj.IsBought);
    }
    private void NextObject()
    {
        _currentSkillObject.HideObj();
        _currentWeaponObjectIndex++;
        if (_currentWeaponObjectIndex >= _skillObjects.Count)
        {
            _currentWeaponObjectIndex = 0;
        }
        _currentSkillObject.ShowObj();
        UpdateUI(_currentSkillObject);
    }

    private void PreviousObject()
    {
        _currentSkillObject.HidePrevious();
        _currentWeaponObjectIndex--;
        if (_currentWeaponObjectIndex < 0)
        {
            _currentWeaponObjectIndex = _skillObjects.Count - 1;
        }
        _currentSkillObject.ShowPrevious();
        UpdateUI(_currentSkillObject);

    }

    private bool TryBuyObject()
    {
        if (_currentSkillObject.AffordAble(GameManager.GetPlayerData.CoinsCount))
        {

            if (GameManager.GetPlayerData.SpendCoins(_currentSkillObject.Price))
            {
                GameManager.GetPlayerData.AddBoughtSkill(_currentSkillObject.Type);
                GameManager.SaveState();
                _currentSkillObject.Buy();
                UpdateUI(_currentSkillObject);
            }
            else
            {
                _noEnoughMoneyScreen.SetActive(true);
            }
        }
        else
        {
            _noEnoughMoneyScreen.SetActive(true);
        }
        _currentBalanceText.text = GameManager.GetPlayerData.CoinsCount + "";
        return true;
    }

    private void UseObject()
    {
        if (_currentSkillObject.IsBought)
        {
            _skillObjects.ForEach(x => x.SetUse(false));
            _currentSkillObject.SetUse(true);
            UpdateUI(_currentSkillObject);
            GameManager.GetPlayerData.SetCurrentSkill(_currentSkillObject.Type);
            GameManager.SaveState();
        }
    }
}
