using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
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
            return _handCardList[index];
    }

    public void DiscardFromHand(int index)
    {
        _discardCardList.Add(_handCardList[index]);
        _handCardList.RemoveAt(index);
    }
	//region Private Methods

	//region Gizmos

}
