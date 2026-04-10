using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Data")]
public class CardData : ScriptableObject
{
	public string cardName;
	public int cost;
	public int damage;
}
