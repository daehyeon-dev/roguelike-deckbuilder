using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitBase : MonoBehaviour
{
	//SerializeField
	[Header("Stats")]
	[SerializeField] private int _maxHealth = 100;
	//private variables
	private int _currentHealth;
	private bool isDead = false;
	//private components
	
	//public property
	public int CurrentHealth => _currentHealth;
	//Unity Lifecycle
	protected virtual void Awake()
	{
		_currentHealth = _maxHealth;
	}
	
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }
	
	//region Public Methods
	public void TakeDamage(int damage)
	{
		if (isDead)
			return;
		Debug.Log($"Get {damage} damage. Current HP is {_currentHealth}");
		_currentHealth -= damage;
		if (_currentHealth <= 0)
			Dead();
	}
	public void Heal(int healAmount)
	{
		if (isDead)
			return;
		Debug.Log($"Get {healAmount} recovery. Current HP is {_currentHealth}");
		_currentHealth += healAmount;
	}
	public void Attack()
	{

	}
	//region Private Methods
	private void Dead()
	{
		if (isDead)
			return;
        Debug.Log($"{gameObject.name} is Dead");
		gameObject.SetActive(false);
	}
	//region Gizmos

}
