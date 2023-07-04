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
	[SerializeField] int Hpgain;
	[SerializeField] int accuracy;
	[SerializeField] int priority;
	[SerializeField] int MaxUses;
	[SerializeField] MoveCategory category;
	[SerializeField] MoveEffects effects;
	[SerializeField] MoveTarget target;

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
	public int GetPriority()
	{
		return priority;
	}
	public int GetMaxUses()
	{
		return MaxUses;
	}
	public int GetHpGain()
	{
		return Hpgain;
	}
	public MoveCategory GetCategory()
	{
		return category;
	}
	public MoveEffects GetEffects()
	{
		return effects;
	}
	public MoveTarget GetTarget()
	{
		return target;
	}
}
[System.Serializable]
// for passive effects like stat boosting, heal etc.
public class MoveEffects
{
	[SerializeField] List<StatBoost> boosts;

	public List<StatBoost> Boosts // may need to be a function
	{ get { return boosts; } }	
}
[System.Serializable]
public class StatBoost
{
	public Stat stat;
	public int boost;
}

public enum MoveCategory
{
	Physical, Magic , Passive
}

public enum MoveTarget
{
	Foe,Self
}
