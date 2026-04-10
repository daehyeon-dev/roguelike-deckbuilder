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
	[SerializeField] private TMP_Text _playerHP;
	[SerializeField] private TMP_Text _enemyHP;
	[Header("Objects")]
	[SerializeField] private BattleUnitBase _player;
	[SerializeField] private BattleUnitBase _enemy;
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
        _player.OnHPChanged += RefreshPlayerHPText;
        _enemy.OnHPChanged += RefreshEnemyHPText;
    }

    private void Start()
    {
        RefreshPlayerHPText(_player.CurrentHealth, _player.MaxHealth);
        RefreshEnemyHPText(_enemy.CurrentHealth, _enemy.MaxHealth);
    }

    private void Update()
    {
        
    }

    //region Public Methods

    //region Private Methods
    private void RefreshTurnText()
    {
        var currentTurn = _battleManager.CurrentTurn;
        if (currentTurn == BattleManager.BattleTurnState.Busy)
            return;

        _turnText.text = currentTurn.ToString();

    }

    private void RefreshEnemyHPText(int currentHP, int maxHP)
	{
		RefreshHPText(_enemyHP, currentHP, maxHP);

    }
    private void RefreshPlayerHPText(int currentHP, int maxHP)
    {
		RefreshHPText(_playerHP, currentHP, maxHP);
    }

    private void RefreshHPText(TMP_Text targetText, int currentHP, int maxHP)
    {
        targetText.text = $"{currentHP} / {maxHP}";
    }
    //region Gizmos

}
