using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	//SerializeField
	[Header("BattleUnitBase")]
	[SerializeField] private BattleUnitBase _playerUnit;
    [SerializeField] private UnitAttack _playerUnitAttack;
    [SerializeField] private BattleUnitBase _enemyUnit;
	[SerializeField] private UnitAttack _enemyUnitAttack;
	//private variables

	//private components

	//public property

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

    }

    private void Update()
    {
        
    }
	
	//region Public Methods

	//region Private Methods
	private void PlayerAttack()
	{
		if(!_playerUnit.IsDead && !_enemyUnit.IsDead)
			_playerUnitAttack.Attack(_enemyUnit);
	}

	private void EnemyAttack()
	{
        if (!_playerUnit.IsDead && !_enemyUnit.IsDead)
            _enemyUnitAttack.Attack(_playerUnit);
	}

	//region Gizmos

}
