using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitAttack : MonoBehaviour
{
	//SerializeField
	[Header("Attack Target")]
	[SerializeField] private BattleUnitBase _target;
	[Header("Status")]
	[SerializeField] private int _attackPower = 10;
	[SerializeField] private PlayerInput _playerInput;
	//private variables
	
	//private components
	
	//public property
	
	//Unity Lifecycle
	private void Awake()
	{

	}

    private void OnEnable()
    {
		_playerInput.actions["PlayerAttack"].performed += OnAttack;
    }
    private void Start()
    {
        
    }

    private void Update()
    {

    }

    private void OnDisable()
    {

    }

	private void OnAttack(InputAction.CallbackContext context)
	{
		Attack();
	}

    //region Public Methods
    public void Attack()
	{
		if(isActiveAndEnabled && _target.isActiveAndEnabled)
			_target.TakeDamage(_attackPower);
	}
	//region Private Methods

	//region Gizmos
}
