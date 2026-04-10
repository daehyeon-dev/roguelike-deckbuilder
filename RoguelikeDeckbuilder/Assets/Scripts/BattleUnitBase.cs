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
	private bool _isDead = false;
	//private components

	//Events
	public event Action<int,int> OnHPChanged;
	//public property
	public int CurrentHealth
	{
		get
		{
			return _currentHealth;
		}
		private set
		{
			_currentHealth = value;
			OnHPChanged?.Invoke(_currentHealth, _maxHealth);
		}
	}
	public int MaxHealth => _maxHealth;
	public bool IsDead => _isDead;
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
		if (_isDead)
			return;
        CurrentHealth -= damage;
        Debug.Log($"{gameObject.name} Get {damage} damage. Current HP is {CurrentHealth}");
		if (CurrentHealth <= 0)
			Dead();
	}
	public void Heal(int healAmount)
	{
		if (_isDead)
			return;
        CurrentHealth = Math.Min(_maxHealth, healAmount+CurrentHealth);
        Debug.Log($"{gameObject.name} Get {healAmount} recovery. Current HP is {CurrentHealth}");
	}
	//region Private Methods
	private void Dead()
	{
		if (_isDead)
			return;
        Debug.Log($"{gameObject.name} is Dead");
		_isDead = true;
		gameObject.SetActive(false);
	}
	//region Gizmos

}
