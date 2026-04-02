using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	//SerializeField
	[Header("Components")]
	[SerializeField] private UnitAttack playerAttack;
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
	public event Action OnPlayerAttack;

	public void OnPlayerAttackInput(InputAction.CallbackContext context)
	{
		if (!context.performed) return;

		OnPlayerAttack?.Invoke();
	}

	//region Gizmos

}
