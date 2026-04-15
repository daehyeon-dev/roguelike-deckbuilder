using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private const int initHandCount = 3;
    private const int handLimit = 5;
    //SerializeField
    [Header("CardList")]
    [SerializeField] private List<CardData> _handCardList = new List<CardData>();
    [SerializeField] private List<CardData> _discardCardList = new List<CardData>();
    [SerializeField] private List<CardData> _deckCardList = new List<CardData>();


    //private variables

    //private components

    //public property

    //Unity Lifecycle
    private void Awake()
	{
		
	}
	
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
	
	//region Public Methods
    public CardData GetHandCard(int index)
    {
        if (index >= _handCardList.Count || _handCardList[index] == null)
            return null;
        else
        {
            var card = _handCardList[index];
            return card;
        }
    }

    public CardData TakeHandCard(int index)
    {
        if (index >= _handCardList.Count || _handCardList[index] == null)
        {
            return null;
        }
            
        else
        {
            var card = _handCardList[index];
            _handCardList.RemoveAt(index);
            return card;
        }
    }

    public void DiscardFromHand(int index)
    {
        _discardCardList.Add(_handCardList[index]);
        _handCardList.RemoveAt(index);
    }

    public void DiscardCard(CardData card)
    {
        _discardCardList.Add(card);
    }

    public void DrawCards(int count)
    {
        for(int i = 0;  i < count; i++)
        {
            DrawCard();
        }
    }

    public void DrawHandCard()
    {
        int count = initHandCount - _handCardList.Count;
        if(count > 0)
            DrawCards(count);
    }
    //region Private Methods
    private void DrawCard()
    {
        if (_handCardList.Count >= handLimit)
        {
            return;
        }
        if (_deckCardList.Count == 0)
        {
            if (_discardCardList.Count == 0)
                return;
            fillCardToDeck();
            ShuffleCardList(_deckCardList);
        }
        var card = _deckCardList[0];
        _handCardList.Add(card);
        _deckCardList.RemoveAt(0);
    }


    private void ShuffleCardList(List<CardData> cardList)
    {
        for(int i = cardList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            var temp = cardList[i];
            cardList[i] = cardList[randomIndex];
            cardList[randomIndex] = temp;
        }
    }

    private void fillCardToDeck()
    {
        _deckCardList.AddRange(_discardCardList);
        _discardCardList.Clear();
    }
	//region Gizmos

}
