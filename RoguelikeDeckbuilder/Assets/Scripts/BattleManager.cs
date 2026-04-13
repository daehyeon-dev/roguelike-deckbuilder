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
	//private components

	//Events
	public event Action OnTurnChanged;
	//public property
	public BattleTurnState CurrentTurn
	{
		get { return _currentTurn; }
		set { _currentTurn = value;
			OnTurnChanged?.Invoke();
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
    }

    private void Start()
    {
		RefreshHandUI();
        CurrentTurn = BattleTurnState.PlayerTurn;
		foreach(var handCard in handUI)
		{
			handCard.OnCardClicked += UseCard;
		}
    }

    private void Update()
    {
        
    }
	
	//region Public Methods

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

	private void TurnEnd()
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
            TurnEnd();
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

		if(card.damage > 0)
		{
			PlayerAttack(card.damage);
		}

		if(card.healAmount > 0)
		{
			PlayerHeal(card.healAmount);
        }

		Discard(index);

        if (_enemyUnit.IsDead)
        {
            TurnEnd();
        }
        else
        {
            StartEnemyTurn();
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
                Debug.Log($"{i} hand is Null.");
				handUI[i].Hide();
            }
        }
    }

	private void Discard(int index)
	{
		_cardManager.DiscardFromHand(index);
		RefreshHandUI();
	}

	private void DrawCard()
	{
		_cardManager.DrawCard();
		RefreshHandUI() ;
	}

	private void DrawHandCard()
	{
		_cardManager.DrawHandCard();
		RefreshHandUI() ;
	}
    //region Gizmos

}
