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
			nameText.text = pokemon._base.GetName();
			levelText.text = "Lvl" + pokemon.level;
			hpBar.SetHP((float)pokemon.HP / pokemon.MaxHP);
			hptrack = (float)pokemon.HP;
			Debug.Log(hptrack);
		}
		public void UpdateHP()
		{
			hpBar.SetHP((float)_pokemon.HP / _pokemon.MaxHP);
			hptrack = (float)_pokemon.HP;
			Debug.Log(hptrack);
		}
	}
}

