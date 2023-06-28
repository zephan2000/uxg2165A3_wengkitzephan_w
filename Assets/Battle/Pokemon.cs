using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
	// This is akin to the Pokemon class
	// scales enemy stats based on level may not need this
	public PokemonBase _base;
    public int level;
	public List<Move> Moves;
	public int HP { get; set; } // creates a private variable behind the scenes, 10:54 of part 6
    public Pokemon(PokemonBase pbase, int plevel)
	{
		_base = pbase;
		level = plevel;
		HP = MaxHP; 
		Moves = new List<Move>();
		foreach (var currentskill in _base.GetLearnableSkills())
		{
			if(currentskill.GetLevel() <= level) // testing out conditions for learning skills may remove
			{
				Moves.Add(new Move(currentskill.GetSkillBase()));
			}
			if(Moves.Count >= 4)
			{

			}
		}
	}

	public int Attack // for scaling may not use
	{
		get { return Mathf.FloorToInt((_base.Getattack() * level / 100f) + 5); }
	}
	public int MaxHP // MaxHP is a property that changes based on level
	{
		get { return Mathf.FloorToInt((_base.GetMaxHp() * level / 10f) + 10); }
	}
	public bool TakeDamage(Move move, Pokemon attacker)
	{
		int damage = move.moveBase.Getdamage();

		HP -= damage;
		if(HP <= 0) // catering for when the pokemon faints
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
