using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
	//SerializeField
	[Header("Manager")]
	[SerializeField] private BattleManager _battleManager;
	[Header("UI")]
	[SerializeField] private TMP_Text _turnText;
	//private variables

	//private components
	
	//public property
	
	//Unity Lifecycle
	private void Awake()
	{
		if (_battleManager == null )
		{
			_battleManager = gameObject.GetComponent<BattleManager>();
		}
	}

    private void OnEnable()
    {
		_battleManager.OnTurnChanged += RefreshTurnText;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
	
	//region Public Methods
	public void RefreshTurnText()
	{
		var currentTurn = _battleManager.CurrentTurn;
		if (currentTurn == BattleManager.BattleTurnState.Busy)
			return;
		
		_turnText.text = currentTurn.ToString();
		
	}
	//region Private Methods

	//region Gizmos

}
