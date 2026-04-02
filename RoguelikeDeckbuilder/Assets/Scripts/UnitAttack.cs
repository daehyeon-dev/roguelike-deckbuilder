using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitAttack : MonoBehaviour
{
	//SerializeField
	[Header("Status")]
	[SerializeField] private int _attackPower = 10;
	//private variables
	
	//private components
	
	//public property
	
	//Unity Lifecycle
	private void Awake()
	{

	}

    private void OnEnable()
    {

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

    //region Public Methods
    public void Attack(BattleUnitBase attackTarget)
	{
		attackTarget.TakeDamage(_attackPower);
	}
	//region Private Methods

	//region Gizmos
}
