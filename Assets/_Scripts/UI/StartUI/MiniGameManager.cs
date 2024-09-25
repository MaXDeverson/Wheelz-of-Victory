using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _getCoinWindow;
    [SerializeField] private List<GameObject> _getCoinSprites;
    [SerializeField] private Animator _shuffleAnimator;
    [SerializeField] private AnimationEventListener _shuffleListener;
    [SerializeField] private List<Button> _cardButtons;
    [SerializeField] private List<MiniGameCard> _cards;
    [SerializeField] private List<int> _coinsAmounts;
    [SerializeField] private MiniGameCard _lastRotatedCard;
    [SerializeField] private bool _canPlay = true;
    private bool _inGetCoinsMode;
    private void Awake()
    {
        _cardButtons.ForEach(x => { 
            x.interactable = false;
            x.gameObject.SetActive(false);
        });
        _shuffleListener.AnimationEventAction += AnimationEventHandler;
    }
    public void SetCanPlay(bool canPlay)
    {
        _canPlay = canPlay;
    }
    void Start()
    {
       
        int randomIndex = Random.RandomRange(0, 3);
        for(int i = 0;i< _cards.Count; i++)
        {
            _cards[randomIndex].SetActiveSpriteIndex(i,_coinsAmounts[i]);
            randomIndex++;
            if(randomIndex >= _cards.Count)
            {
                randomIndex = 0;
            }
        }
        StartCoroutine(AppearCards());
        _lastRotatedCard.OnFinishRotation += () =>
        {
            if(_canPlay )
            {
                _shuffleAnimator.SetBool("Shuffle", true);
            }
        };
        _cards.ForEach(x =>
        {
            x.OnFinishRotation += () =>
            {
                if (_inGetCoinsMode)
                {
                    _getCoinWindow.SetActive(true);
                    Debug.Log("Show Window");
                }
            };
        });

    }
    private IEnumerator AppearCards()
    {
        for(int i = 0;i< _cards.Count;i++)
        {
            yield return new WaitForSeconds(0.3f);
            _cards[i].gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    
    private void AnimationEventHandler(string animationTag)
    {
        switch(animationTag)
        {
            case "shuffleFinish":
                _cardButtons.ForEach(x => {
                    x.interactable = true;
                    x.GetComponent<Animator>().SetInteger("state",1);
                });
                break;
        }
    }

    private bool _isChosen = false;
    public void UserChoiceHandler(string choiceTag)
    {
        if (_isChosen) return;
        _isChosen = true;
        int cardIndex = 0;
        switch(choiceTag)
        {
            case "card_1":
                cardIndex = 0;
                break;
            case "card_2":
                cardIndex = 1;
                break;
            case "card_3":
                cardIndex = 2;
                break;
        }
        _inGetCoinsMode = true;
        MiniGameCard card = _cards[cardIndex];
        card.GetComponent<Animator>().SetInteger("state",2);
        //_cardButtons.ForEach(x=> x.interactable=false);
        GameManager.GetPlayerData.AddCoins(card.GetCoinsCount());
        _getCoinSprites[card.GetActiveSpriteIndex()].gameObject.SetActive(true);
    }
}
