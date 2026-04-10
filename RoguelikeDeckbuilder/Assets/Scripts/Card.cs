using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
	//SerializeField
	[SerializeField] private CardData data;
	//private variables
	
	//private components
	
	//public property
	
	//Unity Lifecycle

	//region Public Methods
	public void Use(BattleUnitBase target)
	{
		if(data.damage > 0)
		{
			target.TakeDamage(data.damage);
		}
	}
	//region Private Methods

	//region Gizmos

}
