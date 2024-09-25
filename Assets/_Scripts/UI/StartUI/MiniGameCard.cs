using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCard : MonoBehaviour
{
    public Action OnFinishRotation;
    [SerializeField] private List<GameObject> _coinSprites;
    [SerializeField] private int _coinsCount;
    private int _activeCoinSpriteIndex = 0;
    private bool _hideRotation = true;
    void Start()
    {
        
    }
    public void SetActiveSpriteIndex(int index, int coinsCount)
    {
        _activeCoinSpriteIndex = index;
        _coinsCount = coinsCount;

    }

    public void AnimationEventHandler(string tag)
    {
        switch (tag)
        {
            case "onRotate":
                if (_hideRotation)
                {
                    _coinSprites.ForEach(x => x.SetActive(false));
                    _hideRotation = false;
                }
                else
                {
                    _coinSprites[_activeCoinSpriteIndex].SetActive(true);
                }
                
                break;
            case "placed":
                _coinSprites[_activeCoinSpriteIndex].SetActive(true);
                break;
            case "onRotateFinish":
                OnFinishRotation?.Invoke();
                _coinSprites.ForEach(x=> x.GetComponent<Animator>().enabled = false);
                break;
        }
    }

    public int GetCoinsCount() => _coinsCount;
    public int GetActiveSpriteIndex() => _activeCoinSpriteIndex;
    // Update is called once per frame
    void Update()
    {
        
    }
}
