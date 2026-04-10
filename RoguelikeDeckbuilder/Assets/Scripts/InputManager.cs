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
    private InputAction _firstCardInput;
    private InputAction _secondCardInput;
    private InputAction _thirdCardInput;
    //private InputAction _eAttackInput;
    //private components
    private PlayerInput _playerInput;
    //Events
    public event Action OnCardSlot1Pressed;
    public event Action OnCardSlot2Pressed;
    public event Action OnCardSlot3Pressed;
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
        _firstCardInput = _playerInput.actions["FirstCardUse"];
        _secondCardInput = _playerInput.actions["SecondCardUse"];
        _thirdCardInput = _playerInput.actions["ThirdCardUse"];
    }

    private void OnEnable()
    {
		_firstCardInput.performed += HandleFirstCard;
        _secondCardInput.performed += HandleSecondCard;
        _thirdCardInput.performed += HandleThirdCard;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

	private void OnDisable()
    {
        _firstCardInput.performed -= HandleFirstCard;
        _secondCardInput.performed -= HandleSecondCard;
        _thirdCardInput.performed -= HandleThirdCard;
    }

    //region Public Methods

    //region Private Methods
    private void HandleFirstCard(InputAction.CallbackContext context)
	{
        OnCardSlot1Pressed?.Invoke();
	}

    private void HandleSecondCard(InputAction.CallbackContext context)
    {
        OnCardSlot2Pressed?.Invoke();
    }

    private void HandleThirdCard(InputAction.CallbackContext context)
    {
        OnCardSlot3Pressed?.Invoke();
    }
    //region Gizmos

}
