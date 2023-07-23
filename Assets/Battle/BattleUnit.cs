using pattayaA3;
using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

//Zephan
public class BattleUnit : MonoBehaviour
{
	PokemonBase _base;
	[SerializeField] int level;
	[SerializeField] bool isPlayerUnit;
	[SerializeField] BattleHud hud;

	public bool IsPlayerUnit
	{
		get { return isPlayerUnit; }
	}
	public BattleHud Hud
	{
		get { return hud; }
	}

	public Pokemon Pokemon { get; set; }
	
	public void BattleUnitSetup(string actorName, string actorType, int level)
	{
		//Debug.Log("passing in name and type");
		string levelId = actorType + "_" + level;	
		this._base = new PokemonBase(actorName, actorType, levelId);
		Game.currentPokemonType = actorType;
		//Debug.Log("created base");
		if(actorName == "Dark Wizard")
		{
			Pokemon = new Pokemon(_base, Game.GetDarkWizardLevel());

            Debug.Log("This is monster name : " + Pokemon.Base.pokemonName);
            Debug.Log("This is monster maxhp: " + Pokemon.MaxHP);
            Debug.Log("This is monster phys: " + Pokemon.Stats[0]);
            Debug.Log("This is monster mag: " + Pokemon.Stats[0]);
        }
		else if (actorType.Contains("player"))
		{
			Pokemon = new Pokemon(_base, level);

            Debug.Log("This is player name : " + Pokemon.Base.pokemonName);
            Debug.Log("This is player maxhp: " + Pokemon.MaxHP);
            Debug.Log("This is player phys: " + Pokemon.Stats[0]);
            Debug.Log("This is player mag: " + Pokemon.Stats[0]);
        }
		else
		{
			Debug.Log(Game.GetEnemyPokemonLevel());
			Debug.Log(level);
			Pokemon = new Pokemon(_base, Game.GetEnemyPokemonLevel());
		}
		AssetManager.LoadSprite(_base.pokemonSpritePath, (Sprite s) =>
		{
			Debug.Log(_base.pokemonSpritePath);
			this.GetComponent<Image>().sprite = s;
		});
		Debug.Log($"{_base.pokemonName} is {_base.pokemonAttSpeed}");
		if(actorType.Contains("enemy"))
			hud.SetEnemyData(Pokemon);
		else
			 hud.SetData(Pokemon);
        // pokemon enter animation will be done here

    }

	//public void Setup(string actorName, string actorType)
	//{
	//	PokemonBase _base = new PokemonBase(actorName, actorType);
	//	Pokemon = new Pokemon(_base, level); //
	//	AssetManager.LoadSprite(_base.pokemonSpritePath, (Sprite s) =>
	//	{
	//		this.GetComponent<SpriteRenderer>().sprite = s;
	//	});
	//	//GetComponent<Image>().sprite = Pokemon.Base.GetPokemonSprite();

	//	hud.SetData(Pokemon);
	//	// pokemon enter animation will be done here
	//}
}
