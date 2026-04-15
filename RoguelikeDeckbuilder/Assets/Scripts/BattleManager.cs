using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	//enum
	public enum BattleTurnState
	{
		PlayerTurn,
		EnemyTurn,
		Busy,
		BattleEnd
	}

	//SerializeField
	[Header("BattleUnitBase")]
	[SerializeField] private BattleUnitBase _playerUnit;
    [SerializeField] private UnitAttack _playerUnitAttack;
    [SerializeField] private BattleUnitBase _enemyUnit;
	[SerializeField] private UnitAttack _enemyUnitAttack;
	[Header("UI")]
	//[SerializeField] private List<CardData> hand = new List<CardData>();
	[SerializeField] private List<CardUIObject> handUI = new List<CardUIObject>();
	[Header("Manager")]
	[SerializeField] private CardManager _cardManager;
	//private variables
	private BattleTurnState _currentTurn;
    private int _maxEnergy = 3;
	private int _currentEnergy;
	//private components

	//Events
	public event Action OnTurnChanged;
	public event Action<int, int> OnEnergyChanged;
	//public property
	public BattleTurnState CurrentTurn
	{
		get { return _currentTurn; }
		set { _currentTurn = value;
			OnTurnChanged?.Invoke();
		}
	}

	public int CurrentEnergy
	{
		get { return _currentEnergy; }
		set
		{
			_currentEnergy = value;
			OnEnergyChanged?.Invoke(CurrentEnergy, _maxEnergy);
		}
	}
	//Unity Lifecycle
	private void Awake()
	{

	}

    private void OnEnable()
    {
        InputManager.Instance.OnCardSlot1Pressed += () => UseCard(0);
        InputManager.Instance.OnCardSlot2Pressed += () => UseCard(1);
		InputManager.Instance.OnCardSlot3Pressed += () => UseCard(2);
		InputManager.Instance.OnCardSlot4Pressed += () => UseCard(3);
		InputManager.Instance.OnCardSlot4Pressed -= () => UseCard(4);
    }

    private void Start()
    {
        foreach (var handCard in handUI)
		{
			handCard.OnCardClicked += UseCard;
		}
		StartPlayerTurn();
    }

    private void Update()
    {
        
    }
	
	//region Public Methods
	public void CallPlayerTurnEnd()
	{
		PlayerTurnEnd();
	}

	//region Private Methods
	private void PlayerAttack(int damage)
	{
		if (!_playerUnit.IsDead && !_enemyUnit.IsDead)
		{
			CurrentTurn = BattleTurnState.Busy;
			_playerUnitAttack.Attack(_enemyUnit, damage);
		}
	}
	
	private void PlayerHeal(int healAmount)
	{
		if(!_playerUnit.IsDead)
		{
			CurrentTurn = BattleTurnState.Busy;
			_playerUnit.Heal(healAmount);
		}
	}

	private void EnemyAttackRoutine()
	{
		if (CurrentTurn != BattleTurnState.EnemyTurn)
			return;
        if (!_playerUnit.IsDead && !_enemyUnit.IsDead)
		{
			CurrentTurn = BattleTurnState.Busy;
			StartCoroutine(EnemyTurnRoutine());
        }
	}

	private void StartPlayerTurn()
	{
		if(CurrentTurn != BattleTurnState.PlayerTurn)
		{
			CurrentTurn = BattleTurnState.PlayerTurn;
		}
		SetCurrentEnergy();
		DrawHandCard();
    }

	private void StartEnemyTurn()
	{
		if(CurrentTurn != BattleTurnState.EnemyTurn)
		{
			CurrentTurn = BattleTurnState.EnemyTurn;
            EnemyAttackRoutine();
        }
	}

	private void BattleEnd()
	{
		if(CurrentTurn != BattleTurnState.BattleEnd)
		{
			CurrentTurn = BattleTurnState.BattleEnd;
		}
	}

	private IEnumerator EnemyTurnRoutine()
	{
		yield return new WaitForSeconds(3);
        _enemyUnitAttack.Attack(_playerUnit);
        if (_playerUnit.IsDead)
        {
            BattleEnd();
        }
        else
        {
            StartPlayerTurn();
        }
    }

	private void UseCard(int index)
	{
        if (CurrentTurn != BattleTurnState.PlayerTurn)
            return;

        var card = _cardManager.GetHandCard(index);

		if(card == null)
		{
			return;
		}
        CurrentTurn = BattleTurnState.Busy;

        if (card.cost > CurrentEnergy)
        {
            Debug.Log("에너지가 부족합니다.");
			CurrentTurn = BattleTurnState.PlayerTurn;
            return;
        }


		card = _cardManager.TakeHandCard(index);

        if (card.damage > 0)
		{
			PlayerAttack(card.damage);
		}

		if(card.healAmount > 0)
		{
			PlayerHeal(card.healAmount);
        }

		if(card.drawAmount > 0)
		{
			DrawCard(card.drawAmount);
		}

		CurrentEnergy -= card.cost;

		Discard(card);

        if (_enemyUnit.IsDead)
        {
            BattleEnd();
			return;
        }


		CurrentTurn = BattleTurnState.PlayerTurn;

		if(_cardManager.IsHandEmpty())
		{
			PlayerTurnEnd();
			return;
		}
    }

	private void RefreshHandUI()
	{
        for (int i = 0; i < handUI.Count; i++)
        {
			var hand = _cardManager.GetHandCard(i);
			if (hand != null)
			{
                handUI[i].SetCardData(hand, i);
			}
			else
			{
                //Debug.Log($"{i} hand is Null.");
				handUI[i].Hide();
            }
        }
    }

	private void PlayerTurnEnd()
	{
        if (CurrentTurn != BattleTurnState.PlayerTurn)
        {
			return;
        }
        if (!_enemyUnit.IsDead)
		{
			DiscardAllHand();
            StartEnemyTurn();
        }
	}

	//private void Discard(int index)
	//{
	//	_cardManager.DiscardFromHand(index);
	//	RefreshHandUI();
	//}

	private void Discard(CardData cardData)
	{
		_cardManager.DiscardCard(cardData);
		RefreshHandUI();
	}

	private void DiscardAllHand()
	{
		while(_cardManager.IsHandEmpty() == false)
		{
            _cardManager.DiscardFromHand(0);
        }
		RefreshHandUI() ;
	}

	private void DrawCard(int count)
	{
		_cardManager.DrawCards(count);
		RefreshHandUI() ;
	}

	private void DrawHandCard()
	{
		_cardManager.DrawHandCard();
		RefreshHandUI() ;
	}

	private void SetCurrentEnergy()
	{
        CurrentEnergy = _maxEnergy;
    }
    //region Gizmos

}
