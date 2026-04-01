using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
	//SerializeField
	[Header("Attack Target")]
	[SerializeField] private BattleUnitBase _target;
	[Header("Status")]
	[SerializeField] private int _attackPower = 10;
	//private variables
	
	//private components
	
	//public property
	
	//Unity Lifecycle
	private void Awake()
	{
		
	}
	
    private void Start()
    {
        
    }

    private void Update()
    {
        
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
