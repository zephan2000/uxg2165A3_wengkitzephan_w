using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Zephan
//[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]
public class PokemonBase
{
	//this script is akin to pokemonbase meant to store data
	// if no scaling than this will be the only class used
	// using scriptableObjects first for testing, SerializeFields will be removed

	public string pokemonName { get { return nameText; } }
	public int MaxHp { get { return maxHp; } }
	public string pokemonSpritePath { get { return actorSpritePath; } } //need to change the current PokemonSprite to actorSprite so that it will reference the correct var
	public int pokemonPhysicalDmg { get { return physicaldamage; } }
	public int pokemonMagicDmg { get { return magicdamage; } }
	public int pokemonDefense { get { return defense; } }
	public int pokemonVitality { get { return vitality; } }
	public int pokemonPower { get { return power; } }
	public int pokemonIntelligence { get { return intelligence; } }
	public int pokemonAttSpeed { get { return attspeed; } }
	public int pokemonExp { get { return exp; } }
	public int pokemonGold { get { return gold; } }
	public List<LearnableSkill> pokemonListOfLearnableSkill { get { return listofskills; } }



	[SerializeField] string nameText;
	[TextArea][SerializeField] string description;
	[SerializeField] string actorSpritePath;
	//attack = vitality or strength, spAttack = intelligence, defense is shield/ additional health, speed is implemented to facilitate the priority system
	// may or may not add accuracy, accuracy affects the probability of the attack landing
	[SerializeField] int maxHp;
	[SerializeField] int physicaldamage;
	[SerializeField] int magicdamage; // affects magic skills
	[SerializeField] int defense; //extra hp
	[SerializeField] int vitality;
	[SerializeField] int power; // affects physical damage
	[SerializeField] int intelligence; // affects magicdamage
	[SerializeField] int attspeed; // affects priority
	[SerializeField] int exp;
	[SerializeField] int gold;

	[SerializeField] List<LearnableSkill> listofskills;

	public PokemonBase(string actorName, string actorType) // for optimisation, create general functions so that it does not return twice
	{
		GetPokemonNameFromActorName(actorName);
		GetPokemonSpritePathFromActorName(actorName);
		GetPokemonMaxHpFromActorName(actorName);
		GetPokemonDefenseFromActorName(actorName);
		GetPokemonPhysicalDamageFromActorName(actorName);
		GetPokemonMagicDamageFromActorName(actorName);
		GetPokemonVitalityFromActorName(actorName);
		GetPokemonPowerFromActorName(actorName);
		GetPokemonIntelligenceFromActorName(actorName);
		GetPokemonSpeedFromActorType(actorName);
		GetPokemonListOfLSFromActorType(actorType);
	}

	public void GetPokemonNameFromActorName(string actorName)
	{
		nameText = Game.GetActorByName(actorName).displayName;
	}
	public void GetPokemonSpritePathFromActorName(string actorName)
	{
		actorSpritePath = Game.GetActorByName(actorName).displaySpritePath;
	}
	public void GetPokemonMaxHpFromActorName(string actorName)
	{
		maxHp = Game.GetActorByName(actorName).maxhp;
	}
	public void GetPokemonDefenseFromActorName(string actorName)
	{
		defense = Game.GetActorByName(actorName).defense;
	}
	public void GetPokemonPhysicalDamageFromActorName(string actorName)
	{
		physicaldamage = Game.GetActorByName(actorName).physicaldmg;
	}
	public void GetPokemonMagicDamageFromActorName(string actorName)
	{
		magicdamage = Game.GetActorByName(actorName).magicdmg;
	}
	public void GetPokemonVitalityFromActorName(string actorName)
	{
		vitality = Game.GetActorByName(actorName).vitality;
	}
	public void GetPokemonPowerFromActorName(string actorName)
	{
		power = Game.GetActorByName(actorName).power;
	}
	public void GetPokemonIntelligenceFromActorName(string actorName)
	{
		intelligence = Game.GetActorByName(actorName).intelligence;
	}
	public void GetPokemonListOfLSFromActorType(string actorType) //revist this
	{
		listofskills = GetListOfLearnableSkillByType(actorType);
	}
	public void GetPokemonSpeedFromActorType(string actorName)
	{
		attspeed = Game.GetActorByName(actorName).attSpeed;
	}
	public void GetPokemonExpFromActorType(string actorName)
	{
		exp = Game.GetActorByName(actorName).exp;
	}
	public void GetPokemonGoldFromActorType(string actorName)
	{
		gold = Game.GetActorByName(actorName).gold;
	}
	public List<LearnableSkill> GetListOfLearnableSkillByType(string actorType)
	{
		Debug.Log("Running List of LS function");
		string BLSstring = Game.GetSkillListByType(actorType);
		Debug.Log(BLSstring);
		string[] BLSarray = BLSstring.Split(','); // this will turn BLSstring into a list, now we turn this into a list of LearnableSkill
		List<LearnableSkill> ListOfLS = new List<LearnableSkill>();
		foreach (var LS in BLSarray)
		{
			//Split into MoveBase and Level
			LearnableSkill newLS = new LearnableSkill(LS); // returning ID of move here and level has to be casted as int later
			ListOfLS.Add(newLS);
		}
		return ListOfLS;
	}


	//public string GetName() // will be loading data from sheets
	//{
	//	//return Game.Getactorbytype
	//	return pokemonName;
	//}
	//public string GetDescription()
	//{
	//	return description;
	//}
	
	//public Sprite GetPokemonSprite()
	//{
	//	return pokemonSprite;
	//}
	//public int GetMaxHp()
	//{
	//	return pokemonPhysicalDmg;
	//}
	//public int GetDefense()
	//{
	//	return pokemonDefense;
	//}
	//public int GetPhysicalDamage() // affects physical skills
	//{
	//	return physicaldamage;
	//}
	//public int GetMagicDamage() // affects magic skills
	//{
	//	return pokemonMagicDmg;
	//}
	//public int GetVitality()
	//{
	//	return pokemonVitality;
	//}
	//public int GetPower()  // affects physical damage
	//{
	//	return pokemonPower;
	//}

	//public int GetIntelligence()  // affects magic damage
	//{
	//	return pokemonIntelligence;
	//}
	
	//public List<LearnableSkill> GetLearnableSkillList()
	//{
	//	//return skill.GetListOfLearnableSkillByType(actorType);
	//	return pokemonListOfLearnableSkill;
	//}
	//public int GetSpeed()
	//{
	//	return pokemonSpeed;
	//}
	//public int GetExp()
	//{
	//	return pokemonExp;
	//}
	//public int GetGold()
	//{
	//	return pokemonGold;
	//}
}

public enum Stat
{
	PhysicalDamage,
	Defense,
	MagicDamage,
	Vitality,
	Power,
	Intelligence,
	Speed,
	Exp,
	Gold,
	MaxHP
}



[System.Serializable] //allows data to be save locally and reloaded later
public class LearnableSkill
{
	[SerializeField] MoveBase moveBase;
	[SerializeField] int level;

	public LearnableSkill(string lsID) //continue from here, need to create moveBase constructor
	{
		moveBase = new MoveBase(lsID);
		string[] LSarray = lsID.Split('_');
		level = int.Parse(LSarray[1]);
	}

	public MoveBase GetMoveBase()
	{
		//Game.GetLSFromSkill(skillname).movebase;
		return moveBase;
	}
	public int GetLevel() 
	{
		//Game.GetLSFromSkill(skillname).level;
		return level;
	}
}


