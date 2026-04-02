using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	//SerializeField
	//private variables
    private static InputManager _instance;
    private InputAction _attackInput;
    private InputAction _eAttackInput;
    //private components
    private PlayerInput _playerInput;
    //Events
    public event Action OnAttackPressed;
    public event Action OnEAttackPressed;
    //public property
    public static InputManager Instance { get; private set; }
	//Unity Lifecycle
	private void Awake()
	{
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if(_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
        _attackInput = _playerInput.actions["Attack"];
        _eAttackInput = _playerInput.actions["EAttack"];
    }

    private void OnEnable()
    {
		_attackInput.performed += HandleAttack;
        _eAttackInput.performed += HandleEAttack;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

	private void OnDisable()
    {
        _attackInput.performed -= HandleAttack;
        _eAttackInput.performed -= HandleEAttack;
    }

    //region Public Methods
    public void HandleAttack(InputAction.CallbackContext context)
	{
        OnAttackPressed?.Invoke();
	}

    public void HandleEAttack(InputAction.CallbackContext context)
    {
        OnEAttackPressed?.Invoke();
    }

	//region Gizmos

}
