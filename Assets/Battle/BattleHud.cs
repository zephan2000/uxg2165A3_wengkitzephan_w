using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pattayaA3
{
	public class BattleHud : MonoBehaviour
	{
		[SerializeField] Text nameText;
		[SerializeField] Text levelText;
		[SerializeField] HPBar hpBar;
		float hptrack;
		Pokemon _pokemon;
		public void SetData(Pokemon pokemon)
		{
			_pokemon = pokemon;
			nameText.text = pokemon.Base.pokemonName;
			levelText.text = "Lvl" + pokemon.Level;
			hpBar.SetHP((float)pokemon.HP / pokemon.MaxHP);
			//hptrack = (float)pokemon.HP;
			//Debug.Log(hptrack);
		}
		public IEnumerator UpdateHP()
		{
			yield return hpBar.SetHPSmooth((float)_pokemon.HP / _pokemon.MaxHP);
			//hptrack = (float)_pokemon.HP;
			//Debug.Log(hptrack);
		}
	}
}

