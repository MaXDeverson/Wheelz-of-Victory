using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private List<UIWeaponObject> _weaponObjects;
    private UIWeaponObject _currentWeaponObject { get => _weaponObjects[_currentWeaponObjectIndex]; }
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
        for(int i = 0; i < _weaponObjects.Count; i++)
        {
            if (GameManager.GetPlayerData.BoughtWeapon.Contains(_weaponObjects[i].Type))
            {
                _weaponObjects[i].Buy();
            }
            if (_weaponObjects[i].Type == GameManager.GetPlayerData.CurrentWeapon)
            {
                _weaponObjects[i].SetUse(true);
            }
        }
        _weaponObjects.ForEach(x=>x.gameObject.SetActive(false));
        UpdateUI(_currentWeaponObject);
        _currentWeaponObject.ShowObj();
        _currentBalanceText.text = GameManager.GetPlayerData.CoinsCount + "";
        Debug.Log("Init Current Balance:" + GameManager.GetPlayerData.CoinsCount);
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
    private void UpdateUI(UIWeaponObject obj)
    {
        _buyButton.SetActive(!obj.IsBought);
        _useButton.SetActive(obj.IsBought && !obj.IsUsed);
        _priceText.text = obj.Price + "";
        _currentPriceView.SetActive(!obj.IsBought);
    }
    private void NextObject()
    {
        _currentWeaponObject.HideObj();
        _currentWeaponObjectIndex++;
        if(_currentWeaponObjectIndex>= _weaponObjects.Count)
        {
            _currentWeaponObjectIndex = 0;
        }
        _currentWeaponObject.ShowObj();
        UpdateUI(_currentWeaponObject);
    }

    private void PreviousObject()
    {
        _currentWeaponObject.HidePrevious();
        _currentWeaponObjectIndex--;
        if(_currentWeaponObjectIndex < 0)
        {
            _currentWeaponObjectIndex = _weaponObjects.Count - 1;
        }
        _currentWeaponObject.ShowPrevious();
        UpdateUI(_currentWeaponObject);

    }

    private bool TryBuyObject()
    {
        if (_currentWeaponObject.AffordAble(GameManager.GetPlayerData.CoinsCount))
        {

            if (GameManager.GetPlayerData.SpendCoins(_currentWeaponObject.Price))
            {
                GameManager.GetPlayerData.AddBoughtWeapon(_currentWeaponObject.Type);
                GameManager.SaveState();
                _currentWeaponObject.Buy();
                UpdateUI(_currentWeaponObject);
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
        Debug.Log("buy");

        return true;
    }

    private void UseObject()
    {
        if (_currentWeaponObject.IsBought)
        {
            _weaponObjects.ForEach(x => x.SetUse(false));
            _currentWeaponObject.SetUse(true);
            UpdateUI(_currentWeaponObject);
            GameManager.GetPlayerData.SetCurrentWeapon(_currentWeaponObject.Type);
            GameManager.SaveState();
        }
    }
}
