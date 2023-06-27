using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Move")]
public class MoveBase : ScriptableObject
{
	//this script is akin to movebase meant to store data
	// using scriptableObjects first for testing, SerializeFields will be removed

	[SerializeField] string nameText;
	[TextArea]
	[SerializeField] string description;
	[SerializeField] int damage;
	[SerializeField] int accuracy;
	[SerializeField] int numberofuses;

	public string GetName() // will be loading data from sheets
	{
		return nameText;
	}
	public string GetDescription()
	{
		return description;
	}
	public int Getdamage()
	{
		return damage;
	}
	public int Getaccuracy()
	{
		return accuracy;
	}
	public int Getnumberofuses()
	{
		return numberofuses;
	}
}
