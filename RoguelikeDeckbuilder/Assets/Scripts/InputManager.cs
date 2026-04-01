using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	//SerializeField
	[Header("Components")]
	[SerializeField] private UnitAttack playerAttack;
	[SerializeField] private UnitAttack enemyAttack;
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
	public void OnPlayerAttack(InputAction.CallbackContext context)
	{
		if (!context.performed) return;
		playerAttack.Attack();

	}
	public void OnEnemyAttack(InputAction.CallbackContext context)
	{
		if(!context.performed) return;
		enemyAttack.Attack();
	}
	//region Private Methods

	//region Gizmos

}
