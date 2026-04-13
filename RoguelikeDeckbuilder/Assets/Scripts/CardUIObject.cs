using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUIObject : MonoBehaviour, IPointerClickHandler
{
	//SerializeField
	[Header("CardData")]
	[SerializeField] private CardData _cardData;
	[SerializeField] private int _cardIndex;
	[SerializeField] private TMP_Text _cardText;
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
		_cardText.text = $"{_cardData.name}\nCost: {_cardData.cost}";
		if(_cardData.damage > 0)
		{
			_cardText.text += $"\nDeal {_cardData.damage} Damage";
		}
		if(_cardData.healAmount > 0)
		{
			_cardText.text += $"\nHeal {_cardData.healAmount} Self";
		}
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCardClicked?.Invoke(_cardIndex);
    }
    //region Gizmos

}
