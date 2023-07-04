using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]
public class PokemonBase : ScriptableObject
{
    //this script is akin to pokemonbase meant to store data
    // if no scaling than this will be the only class used
    // using scriptableObjects first for testing, SerializeFields will be removed
    [SerializeField] string nameText;
	[TextArea] [SerializeField] string description;
	[SerializeField] Sprite PokemonSprite;

	[SerializeField] int maxHp;
	[SerializeField] int attack;
	[SerializeField] int defense;
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
	public int Getattack()
	{
        return attack;
	}
	public int Getdefense()
	{
		return defense;
	}
	public List<LearnableSkill> GetLearnableSkills()
	{
		return skill;
	}
}
[System.Serializable] //allows data to be save locally and reloaded later
public class LearnableSkill
{
	[SerializeField] MoveBase skillBase;
	[SerializeField] int level;

	public MoveBase GetSkillBase()
	{
		return skillBase;
	}
	public int GetLevel() 
	{ 
		return level;
	}
}
