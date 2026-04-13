using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUIObject : MonoBehaviour, IPointerClickHandler
{
	//SerializeField
	[Header("CardData")]
	[SerializeField] private CardData _cardData;
	[SerializeField] private int _cardIndex;
	//private variables

	//private components
	//Event
	public event Action<int> OnCardClicked;
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
	public void SetCardData(CardData cardData, int cardIndex)
	{
		_cardData = cardData;
		_cardIndex = cardIndex;
		DrawCardUI();
	}
    //region Private Methods
	private void DrawCardUI()
	{

	}

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCardClicked?.Invoke(_cardIndex);
    }
    //region Gizmos

}
