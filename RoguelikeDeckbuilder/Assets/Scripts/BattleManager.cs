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
        InputManager.Instance.OnEAttackPressed += EnemyAttack;
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
			CurrentTurn = BattleTurnState.EnemyTurn;
		}			
	}

	private void EnemyAttack()
	{
		if (CurrentTurn != BattleTurnState.EnemyTurn)
			return;
        if (!_playerUnit.IsDead && !_enemyUnit.IsDead)
		{
			CurrentTurn = BattleTurnState.Busy;
            _enemyUnitAttack.Attack(_playerUnit);
			CurrentTurn = BattleTurnState.PlayerTurn;
        }
	}

	//region Gizmos

}
