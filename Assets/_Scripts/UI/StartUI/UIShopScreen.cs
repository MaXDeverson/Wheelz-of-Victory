using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopScreen : MonoBehaviour
{
    [SerializeField] private Text _currentBalanceText;
    private void OnEnable()
    {
        _currentBalanceText.text = GameManager.GetPlayerData.CoinsCount + "";
    }
    // Update is called once per frame
    void Update()
    {

    }
}
