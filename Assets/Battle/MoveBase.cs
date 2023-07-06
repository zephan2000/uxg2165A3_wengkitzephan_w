using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Zephan
[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Move")]
public class MoveBase : ScriptableObject
{
	//this script is akin to movebase meant to store data
	// using scriptableObjects first for testing, SerializeFields will be removed

	public string moveName { get { return nameText; } }
	public int moveDamage { get { return damage; } }
	public int moveAccuracy { get { return accuracy; } }
	public int movePriority { get { return priority; } }
	public int moveMaxUses { get { return MaxUses; } }
	public int moveHpGain { get { return Hpgain; } }
	public MoveCategory moveCategory { get { return category; } }
	public MoveTarget moveTarget { get { return movetarget; } }

	[SerializeField] string nameText; // change to private when ready
	[TextArea]
	[SerializeField] string description;
	[SerializeField] int damage;
	[SerializeField] int Hpgain;
	[SerializeField] int accuracy;
	[SerializeField] int priority;
	[SerializeField] int MaxUses;
	[SerializeField] MoveCategory category;
	[SerializeField] MoveEffects effects;
	[SerializeField] MoveTarget movetarget;

	public MoveBase(string mbID) // for optimisation, create general functions so that it does not return twice
	{
		skills skillid = Game.GetSkillById(mbID);
		GetMBNameFromSkill(mbID);
		GetMBDamageFromSkill(mbID);
		GetMBAccuracyFromSkill(mbID);
		GetMBPriorityFromSkill(mbID);
		GetMBMaxUsesFromSkill(mbID);
		GetMBHpGainFromSkill(mbID);
		GetMBCategoryFromSkill(mbID);
		GetMBTargetFromSkill(mbID);

	}

	public void GetMBNameFromSkill(string mbID) // will be loading data from sheets
	{
		nameText = Game.GetSkillById(mbID).skillname;
	}
	public void GetMBDamageFromSkill(string mbID) // need to change to void
	{
		damage = Game.GetSkillById(mbID).dmg;
	}
	public void GetMBAccuracyFromSkill(string mbID) // need to change to void
	{
		accuracy = Game.GetSkillById(mbID).accuracy;
	}
	public void GetMBPriorityFromSkill(string mbID) // need to change to void
	{
		priority = Game.GetSkillById(mbID).priority;
	}
	public void GetMBMaxUsesFromSkill(string mbID) // need to change to void
	{
		MaxUses = Game.GetSkillById(mbID).maxuses;
	}
	public void GetMBHpGainFromSkill(string mbID) // need to change to void
	{
		Hpgain = Game.GetSkillById(mbID).hpgain;
	}
	public void GetMBCategoryFromSkill(string mbID) // need to change to void
	{
		var categoryfromdata = Game.GetSkillById(mbID).category;
		Debug.Log(categoryfromdata);
		category = MoveCategory.Parse<MoveCategory>(" ");
	}
	public void GetMBTargetFromSkill(string mbID) // need to change to void
	{
		var targetfromdata = Game.GetSkillById(mbID).target;
		Debug.Log(targetfromdata);
		movetarget = MoveTarget.Parse<MoveTarget>(" ");
	}
	//public string GetMBNameFromSkill() // will be loading data from sheets
	//{
	//	//nameText = Game.GetskillbyName(skillid).skillname;
	//	return pokemonName;
	//}
	//public string GetDescription() // need to change to void
	//{
	//	return description;
	//}
	//public int GetDamageFromSkill() // need to change to void
	//{
	//	//damage = Game.GetskillbyName(skillid).dmg;
	//	return pokemonDamage;
	//}
	//public int GetAccuracyFromSkill() // need to change to void
	//{
	//	//accuracy = Game.GetskillbyName(skillid).accuracy;
	//	return pokemonAccuracy;
	//}
	//public int GetPriorityFromSkill() // need to change to void
	//{
	//	//priority = Game.GetskillbyName(skillid).priority;
	//	return pokemonPriority;
	//}
	//public int GetMaxUsesFromSkill() // need to change to void
	//{
	//	//MaxUses= Game.GetskillbyName(skillid).maxuses;
	//	return pokemonMaxUses;
	//}
	//public int GetHpGainFromSkill() // need to change to void
	//{
	//	//Hpgain = Game.GetskillbyName(skillid).hpgain;
	//	return pokemonHpGain;
	//}
	//public MoveCategory GetCategoryFromSkill()
	//{
	//	// var categoryfromdata = Game.GetskillbyName(skillid).category;
	//	//MoveCategory category = Enum.Parse<MoveCategory>(categoryfromdata);
	//	return pokemonCategory;
	//}
	//public MoveEffects GetEffects()
	//{
	//	return effects;
	//}
	//public MoveTarget GetMoveTargetFromSkill()
	//{
	//	// var targetfromdata = Game.GetskillbyName(skillid).target;
	//	//MoveTarget moveTarget = Enum.Parse<MoveTarget>(targetfromdata);
	//	return pokemonMoveTarget;
	//}
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
//public MoveBase(string MoveBaseName) // assigning twice but keeping as backup
//{
//	skills skillid = GetskillbyName(MoveBaseName);
//	this.nameText = GetNameFromSkill(skillid);
//	this.damage = GetDamage(skillid);
//	this.accuracy = GetAccuracy(MoveBaseName);
//	this.priority = GetPriority(MoveBaseName);
//	this.MaxUses = GetMaxUses(MoveBaseName);
//	this.Hpgain = GetHpGain(MoveBaseName);
//	this.category = GetCategory(MoveBaseName);
//	this.movetarget = GetMoveTarget(MoveBaseName);

//}