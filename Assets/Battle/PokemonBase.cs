using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Zephan
[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]
public class PokemonBase : ScriptableObject
{
    //this script is akin to pokemonbase meant to store data
    // if no scaling than this will be the only class used
    // using scriptableObjects first for testing, SerializeFields will be removed
    [SerializeField] string nameText;
	[TextArea] [SerializeField] string description;
	[SerializeField] Sprite PokemonSprite;
	//attack = vitality or strength, spAttack = intelligence, defense is shield/ additional health, speed is implemented to facilitate the priority system
	// may or may not add accuracy, accuracy affects the probability of the attack landing
	[SerializeField] int maxHp;
	[SerializeField] int defense; //extra hp
	[SerializeField] int physicaldamage; // affects physical skills
	[SerializeField] int magicdamage; // affects magic skills
	[SerializeField] int vitality;
	[SerializeField] int power; // affects physical damage
	[SerializeField] int intelligence; // affects magicdamage
	[SerializeField] int speed; // affects priority
	[SerializeField] int exp;
	[SerializeField] int gold;

	[SerializeField] List<LearnableSkill> skill;

	public string GetName() // will be loading data from sheets
	{
		//return Game.Getactorbytype
		return nameText;
	}
	public string GetDescription()
	{
		return description;
	}
	
	public Sprite GetPokemonSprite()
	{
		return PokemonSprite;
	}
	public int GetMaxHp()
	{
		return maxHp;
	}
	public int GetDefense()
	{
		return defense;
	}
	public int GetPhysicalDamage() // affects physical skills
	{
		return physicaldamage;
	}
	public int GetMagicDamage() // affects magic skills
	{
		return magicdamage;
	}
	public int GetVitality()
	{
		return vitality;
	}
	public int GetPower()  // affects physical damage
	{
		return power;
	}

	public int GetIntelligence()  // affects magic damage
	{
		return intelligence;
	}
	
	public List<LearnableSkill> GetLearnableSkillList()
	{
		//return skill.GetListOfLearnableSkillByType(actorType);
		return skill;
	}
	public int GetSpeed()
	{
		return speed;
	}
	public int GetExp()
	{
		return exp;
	}
	public int GetGold()
	{
		return gold;
	}
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

	//public LearnableSkill(string mbID, int reflevel) //continue from here, need to create moveBase constructor
	//{
		//skills newskill = GetSkillById(mbID);
		//moveBase = new MoveBase(mbID); 
		//level = reflevel
	//}
	
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

	//public List<LearnableSkill> GetListOfLearnableSkillByType(string actorType)
	//{
		//string BLSstring = Game.GetSkillByType(actorType);
		//string [] BLSarray = BLSstring.Split(','); // this will turn BLSstring into a list, now we turn this into a list of LearnableSkill
		//List<LearnableSkill> ListOfLS = new List<LearnableSkill>();
		//foreach(var LS in BLSarray)
		//{
			//Split into MoveBase and Level
			//string [] LSarray = BLSarray.Split('%');
			//LearnableSkill newLS = new LearnableSkill(LSarray[0], LSarray[1]); // returning ID of move here and level that it can be learnt at
			//ListOfLS.Add(newLS);
		//}
		//return ListOfLS;
		

	//}
}


