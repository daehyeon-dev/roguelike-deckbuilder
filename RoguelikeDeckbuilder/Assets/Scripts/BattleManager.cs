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
	[SerializeField] private TMP_Text _textMeshPro;
	//private variables
	private BattleTurnState _currentTurn;
	//private components

	//public property
	public BattleTurnState CurrentTurn
	{
		get { return _currentTurn; }
		set { _currentTurn = value;
			Debug.Log($"{_currentTurn}");
			_textMeshPro.text = _currentTurn.ToString();
		}
	}
	//Unity Lifecycle
	private void Awake()
	{

	}

    private void OnEnable()
    {
        InputManager.Instance.OnAttackPressed += PlayerAttack;
        //InputManager.Instance.OnEAttackPressed += EnemyAttack;
    }

    private void Start()
    {
		CurrentTurn = BattleTurnState.PlayerTurn;
    }

    private void Update()
    {
        
    }
	
	//region Public Methods

	//region Private Methods
	private void PlayerAttack()
	{
		if (CurrentTurn != BattleTurnState.PlayerTurn)
			return;
		if (!_playerUnit.IsDead && !_enemyUnit.IsDead)
		{
			CurrentTurn = BattleTurnState.Busy;
			_playerUnitAttack.Attack(_enemyUnit);
			if (_enemyUnit.IsDead)
			{
				TurnEnd();
            }
			else
			{
				StartEnemyTurn();
            }
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
		Debug.Log("Enemy Turn Coroutine is Started");
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

	//region Gizmos

}
