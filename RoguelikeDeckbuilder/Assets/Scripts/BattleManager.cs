using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
	[SerializeField] private List<CardData> hand = new List<CardData>();
	[SerializeField] private List<CardUIObject> handUI = new List<CardUIObject>();
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
		SetHandData();
        CurrentTurn = BattleTurnState.PlayerTurn;
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
		if (index >= hand.Count)
			return;

		if (hand[index] == null)
			return;

        if (CurrentTurn != BattleTurnState.PlayerTurn)
            return;

        var card = hand[index];

		CurrentTurn = BattleTurnState.Busy;

		if(card.damage > 0)
		{
			PlayerAttack(card.damage);
		}

		if(card.healAmount > 0)
		{
			PlayerHeal(card.healAmount);
        }

        if (_enemyUnit.IsDead)
        {
            TurnEnd();
        }
        else
        {
            StartEnemyTurn();
        }
    }

	private void SetHandData()
	{
        for (int i = 0; i < hand.Count; i++)
        {
			if (handUI[i] == null)
			{
				Debug.Log($"{i} handUI is Null.");
				return;
			}	
            handUI[i].SetCardData(hand[i], i);
			handUI[i].OnCardClicked += UseCard;
        }
    }
	//region Gizmos

}
