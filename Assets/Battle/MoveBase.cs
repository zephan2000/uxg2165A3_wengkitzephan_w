using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Zephan
[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Move")]
public class MoveBase : ScriptableObject
{
	//this script is akin to movebase meant to store data
	// using scriptableObjects first for testing, SerializeFields will be removed

	//public string pokemonName { get {return nameText;} }
	//public int pokemonDamage { get {return damage;} }
	//public int pokemonAccuracy { get {return accuracy;} }
	//public int pokemonPriority { get {return priority;} }
	//public int pokemonMaxUses { get {return MaxUses;} }
	//public int pokemonHpGain { get {return Hpgain;} }
	//public int pokemonCategory { get {return category;} }
	//public int pokemonMoveTarget { get {return movetarget;} }

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

	//public MoveBase(string mbID) // for optimisation, create general functions so that it does not return twice
	//{
	//skills skillid = GetSkillById(mbID);
	//GetMBNameFromSkill(mbID);
	//GetMBDamageFromSkill(mbID);
	//GetMBAccuracyFromSkill(mbID);
	//GetMBPriorityFromSkill(mbID);
	//GetMBMaxUsesFromSkill(mdID);
	//GetMBHpGainFromSkill(mbID);
	//GetMBCategoryFromSkill(mbID);
	//GetMBMoveTargetFromSkill(mbID);

	//}
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

	//public void GetMBNameFromSkill(mbID) // will be loading data from sheets
	//{
	//	//nameText = Game.GetSkillById(skillid).skillname;
	//}
	//public void GetMBDamageFromSkill(mbID) // need to change to void
	//{
	//	//damage = Game.GetSkillById(mbID).dmg;
	//}
	//public void GetMBDamageFromSkill(mbID) // need to change to void
	//{
	//	//accuracy = Game.GetSkillById(skillid).accuracy;
	//}
	public string GetMBNameFromSkill() // will be loading data from sheets
	{
		//nameText = Game.GetskillbyName(skillid).skillname;
		return nameText;
	}
	public string GetDescription() // need to change to void
	{
		return description;
	}
	public int GetDamageFromSkill() // need to change to void
	{
		//damage = Game.GetskillbyName(skillid).dmg;
		return damage;
	}
	public int GetAccuracyFromSkill() // need to change to void
	{
		//accuracy = Game.GetskillbyName(skillid).accuracy;
		return accuracy;
	}
	public int GetPriorityFromSkill() // need to change to void
	{
		//priority = Game.GetskillbyName(skillid).priority;
		return priority;
	}
	public int GetMaxUsesFromSkill() // need to change to void
	{
		//MaxUses= Game.GetskillbyName(skillid).maxuses;
		return MaxUses;
	}
	public int GetHpGainFromSkill() // need to change to void
	{
		//Hpgain = Game.GetskillbyName(skillid).hpgain;
		return Hpgain;
	}
	public MoveCategory GetCategoryFromSkill()
	{
		// var categoryfromdata = Game.GetskillbyName(skillid).category;
		//MoveCategory category = Enum.Parse<MoveCategory>(categoryfromdata);
		return category;
	}
	public MoveEffects GetEffects()
	{
		return effects;
	}
	public MoveTarget GetMoveTargetFromSkill()
	{
		// var targetfromdata = Game.GetskillbyName(skillid).target;
		//MoveTarget moveTarget = Enum.Parse<MoveTarget>(targetfromdata);
		return movetarget;
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
