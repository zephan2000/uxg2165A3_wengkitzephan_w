using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Zephan
public class Pokemon 
{
	// This is akin to the Pokemon class
	// scales enemy stats based on level may not need this
	[SerializeField] PokemonBase _base;
    [SerializeField] int level;
	public List<Move> Moves;
	
	public PokemonBase Base { get { return _base; } }
	public int Level { get { return level; } }	
	public Move CurrentMove { get; set; }
	public int HP { get; set; } // creates a private variable behind the scenes, 10:54 of part 6
    public Dictionary<Stat,int> Stats { get; private set; }	
	public Dictionary<Stat, int> StatBoosts { get; private set; }
	public Pokemon(PokemonBase pbase, int plevel) //pass in actorName, actorType and, Pokemon level
	{
		_base = pbase; //pass in actorName and actorType
		level = plevel; //pass in Pokemon Level, have not implemented a way to get the level of the pokemon yet
		//Move = GetListOfMovesByActorType(actorType) // return a list of skills,
		Moves = new List<Move>();
		foreach (LearnableSkill LS in _base.pokemonListOfLearnableSkill) //GetLearnableSkills(should change to GetListOfLearnableSkills
		{
			if(LS.GetLevel() <= level) // testing out conditions for learning skills may remove
			{
				Moves.Add(new Move(LS.GetMoveBase()));
			}
			if(Moves.Count >= 4)
			{
				break;
			}
		}
		
		CalculateStats();
		HP = MaxHP;
		StatBoosts = new Dictionary<Stat, int>() //important to instantiate
		{
			{Stat.Defense, 0},
			{Stat.PhysicalDamage, 0},
			{Stat.MagicDamage, 0},
			{Stat.Vitality,0},
			{Stat.Power,0},
			{Stat.Intelligence, 0},
			{Stat.Speed,0},

		};
	}
	// CalculateStats() is for optimisation purposes, Calculates once and stores value, SpAttack = Intelligence
	void CalculateStats()
	{
		Stats = new Dictionary<Stat,int>();
		Stats.Add(Stat.Defense, Mathf.FloorToInt((_base.pokemonDefense +(_base.pokemonDefense * level / 100f) + 5)));
		Stats.Add(Stat.PhysicalDamage, Mathf.FloorToInt((_base.pokemonPhysicalDmg + (_base.pokemonPower * level / 100f) + 5)));
		Stats.Add(Stat.MagicDamage, Mathf.FloorToInt((_base.pokemonMagicDmg + (_base.pokemonIntelligence * level / 100f) + 5)));
		Stats.Add(Stat.Vitality, Mathf.FloorToInt((_base.pokemonVitality * level / 100f) + 5));
		Stats.Add(Stat.Power, Mathf.FloorToInt((_base.pokemonPower * level / 100f) + 5));
		Stats.Add(Stat.Intelligence, Mathf.FloorToInt((_base.pokemonIntelligence * level / 100f) + 5));
		Stats.Add(Stat.Speed, Mathf.FloorToInt((_base.pokemonAttSpeed * level / 100f) + 5));
		Stats.Add(Stat.Exp,_base.pokemonExp);
		Stats.Add(Stat.Gold,_base.pokemonGold);

		MaxHP = Mathf.FloorToInt((_base.MaxHp * level) + 10 + level + (_base.pokemonVitality * level / 100f) + 5);
	}
	int GetStat(Stat stat)
	{
		int statVal = Stats[stat];
		//Apply stat boost, pokemon stat boosting is for level only hmm
		int boost = StatBoosts[stat]; // implementing for heal and other passive skills
		var boostValues = new float[] { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f }; //6 levels each level has a different stat boost multiplier

		if(boost >= 0)
		{
			statVal = Mathf.FloorToInt(statVal * boostValues[boost]);
		}
		else
			statVal = Mathf.FloorToInt(statVal / boostValues[-boost]);
		return statVal;
	}

	public void ApplyBoosts(List<StatBoost> statBoosts)
	{
		foreach(var statBoost in statBoosts)
		{
			var stat = statBoost.stat;
			var boost = statBoost.boost;

			StatBoosts[stat] = Mathf.Clamp(StatBoosts[stat] + boost, -6, 6);
			Debug.Log($"{stat} has been boosted to {StatBoosts[stat]}");
		}
	}
	//properties can be used here since the pokemonBase deals with the base data already
	public int Defense
	{
		get { return GetStat(Stat.Defense); }
	}
	public int PhysicalDamage 
	{
		get { return GetStat(Stat.PhysicalDamage); }
	}
	public int MagicDamage
	{
		get { return GetStat(Stat.MagicDamage); }
	}
	public int Vitality
	{
		get { return GetStat(Stat.Vitality); }
	}
	public int Power
	{
		get { return GetStat(Stat.Power); }
	}
	public int Intelligence 
	{
		get { return GetStat(Stat.Intelligence); }
	}
	
	public int Speed
	{
		get { return GetStat(Stat.Speed); }
	}
	public int MaxHP // MaxHP is a property that changes based on level, if damage looks weird check here
	{ get; private set ; }
	

	public bool InitMove(Move move, Pokemon attacker)
	{
		float attack = (move.moveBase.moveCategory == MoveCategory.Magic)? attacker.MagicDamage : attacker.PhysicalDamage;
		int damage = move.moveBase.moveDamage + (int)attack;
		Debug.Log($"This is {this.Base.pokemonName} before MoveHP: {HP}");
		HP -= damage;
		//Debug.Log($"This is before Heal: {HP}");
		HP = Mathf.Clamp(HP + move.moveBase.moveHpGain, 0, this.MaxHP);
		Debug.Log($"This is {this.Base.pokemonName} current HP: {HP}");

		if (HP <= 0) // catering for when the pokemon faints
		{
			HP = 0; //so that UI does not show negative damage
			return true;
		}
		return false;
	}

	public Move GetRandomMove() // for enemy battle strategy
	{
		int r = Random.Range(0, Moves.Count);
		return Moves[r];
	}
}
