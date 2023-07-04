using pattayaA3;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
	[SerializeField] PokemonBase _base;
	[SerializeField] int level;
	[SerializeField] bool isPlayerUnit;

	public Pokemon Pokemon { get; set; }
	public void Setup()
	{
		Pokemon = new Pokemon(_base, level);
		GetComponent<Image>().sprite = Pokemon._base.GetPokemonSprite();

		Debug.Log(Pokemon._base.GetName());
        Debug.Log(Pokemon.MaxHP);
        Debug.Log(Pokemon.Attack);
    }
}
