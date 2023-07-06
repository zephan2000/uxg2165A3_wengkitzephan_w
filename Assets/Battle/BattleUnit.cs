using pattayaA3;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

//Zephan
public class BattleUnit : MonoBehaviour
{
	[SerializeField] PokemonBase _base;
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
	public void Setup()
	{
		Pokemon = new Pokemon(_base, level);
		GetComponent<Image>().sprite = Pokemon.Base.GetPokemonSprite();

		hud.SetData(Pokemon);
		// pokemon enter animation will be done here
	}
}
