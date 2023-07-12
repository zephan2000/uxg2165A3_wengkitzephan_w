using pattayaA3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
	
	public void BattleUnitSetup(string actorName, string actorType)
	{
		Debug.Log("passing in name and type");
		this._base = new PokemonBase(actorName, actorType);
		Debug.Log("created base");
		if(actorName == "Dark Wizard")
		{
			Pokemon = new Pokemon(_base, level);
		}
		else
		{
			Debug.Log(Game.GetPokemonLevel());
			Debug.Log(level);
			Pokemon = new Pokemon(_base, Game.GetPokemonLevel());
		}
		
		//Sprite pokemonSprite = Resources.Load<Sprite>(_base.pokemonSpritePath); 
		//this.GetComponent<Image>().sprite = Resources.Load<Sprite>(_base.pokemonSpritePath);
		AssetManager.LoadSprite(_base.pokemonSpritePath, (Sprite s) =>
		{
			Debug.Log(_base.pokemonSpritePath);
			this.GetComponent<Image>().sprite = s;
		});
		Debug.Log($"{_base.pokemonName} is {_base.pokemonAttSpeed}");
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
