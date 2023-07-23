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
	public int pokemonExpGain { get { return expGain; } }
	public int pokemonGoldGain { get { return goldGain; } }
	public List<LearnableSkill> pokemonListOfLearnableSkill { get { return listofskills; } }
	public string pokemonActorType;



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
	[SerializeField] int expGain;
	[SerializeField] int goldGain;
	//[SerializeField] string reflevelId;

	[SerializeField] List<LearnableSkill> listofskills;

	public PokemonBase(string actorName, string actorType, string levelId) // for optimisation, create general functions so that it does not return twice
	{
	
		GetPokemonNameFromActorName(actorName);
		GetPokemonSpritePathFromActorName(actorName);
		GetPokemonMaxHpFromLevelId(levelId);
		GetPokemonPhysicalDamageFromLevelId(levelId);
		GetPokemonMagicDamageFromLevelId(levelId);
		GetPokemonVitalityFromLevelId(levelId);
		GetPokemonPowerFromLevelId(levelId);
		GetPokemonPhysicalDamageFromLevelId(levelId);
		GetPokemonPhysicalDamageFromLevelId(levelId);
		GetPokemonSpeedFromLevelId(levelId);
		GetPokemonListOfLSFromActorType(actorType);
		pokemonActorType = actorType;
		Debug.Log($"this is LevelId: {levelId}, actorName {actorName}, attspeed: {attspeed}");//getting skill list from actor sheet, don't change
	}

	//public void SetRefLevelId(string actorType, int level) 
	//{
	//	reflevelId = actorType + "_" + level.ToString();	
	//}

	public void GetPokemonNameFromActorName(string actorName)
	{
		nameText = Game.GetActorByActorName(actorName).displayName;
	}
	public void GetPokemonSpritePathFromActorName(string actorName)
	{
		actorSpritePath = Game.GetActorByActorName(actorName).displaySpritePath;
	}
	public void GetPokemonMaxHpFromLevelId(string levelId)
	{
		maxHp = Game.GetLevelByLevelId(levelId).basehp;
	}
	public void GetPokemonPhysicalDamageFromLevelId(string levelId)
	{
		physicaldamage = Game.GetLevelByLevelId(levelId).physicaldmg;
	}
	public void GetPokemonMagicDamageFromLevelId(string levelId)
	{
		magicdamage = Game.GetLevelByLevelId(levelId).magicdmg;
	}
	public void GetPokemonVitalityFromLevelId(string levelId)
	{
		vitality = Game.GetLevelByLevelId(levelId).vitality;
	}
	public void GetPokemonPowerFromLevelId(string levelId)
	{
		power = Game.GetLevelByLevelId(levelId).power;
	}
	public void GetPokemonIntelligenceFromLevelId(string levelId)
	{
		intelligence = Game.GetLevelByLevelId(levelId).intelligence;
	}
	public void GetPokemonSpeedFromLevelId(string levelId)
	{
		Debug.Log(Game.GetLevelByLevelId(levelId).attspeed);
		attspeed = Game.GetLevelByLevelId(levelId).attspeed;
	}
	public void GetPokemonExpGainFromlevelId(string levelId)
	{
		expGain = Game.GetLevelByLevelId(levelId).expGain;
	}
	public void GetPokemonGoldGainFromActorType(string levelId)
	{
		goldGain = Game.GetLevelByLevelId(levelId).goldGain;
	}
	public void GetPokemonListOfLSFromActorType(string actorType) //revist this
	{
		listofskills = GetListOfLearnableSkillByType(actorType);
	}
	
	public List<LearnableSkill> GetListOfLearnableSkillByType(string actorType)
	{
		//Debug.Log("Running List of LS function");
		string BLSstring = Game.GetSkillListByType(actorType);
		//Debug.Log(BLSstring);
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
}

public enum Stat
{
	PhysicalDamage,
	MagicDamage,
	Vitality,
	Power,
	Intelligence,
	Speed,
	ExpGain,
	GoldGain,
	MaxHP
}



[System.Serializable] //allows data to be save locally and reloaded later
public class LearnableSkill
{
	[SerializeField] MoveBase moveBase;
	[SerializeField] int level;

	public LearnableSkill(string lsID)
	{
		moveBase = new MoveBase(lsID);
		string[] LSarray = lsID.Split('_');
		level = int.Parse(LSarray[1]);
	}

	public MoveBase GetMoveBase()
	{
		return moveBase;
	}
	public int GetLevel() 
	{
		return level;
	}
}


