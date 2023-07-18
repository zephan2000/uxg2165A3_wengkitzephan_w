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
		public HpBar hpBar;
		public ExpBar expBar;
		float hptrack;
		Pokemon _pokemon;
		public void SetData(Pokemon pokemon)
		{
			_pokemon = pokemon;
			nameText.text = pokemon.Base.pokemonName;
			levelText.text = "Lvl" + pokemon.Level;
			hpBar.SetHPSmooth((float)(pokemon.HP / pokemon.MaxHP));
			Debug.Log($"{pokemon.Base.pokemonName} did not run successfully: ");
			expBar.SetEXPSmooth((float)(Game.mainsessionData.exp / Game.currentmaxEXP));
			Debug.Log($"{pokemon.Base.pokemonName} ran successfully: ");
			//hptrack = (float)pokemon.HP;
			//Debug.Log(hptrack);
		}
		public void SetEnemyData(Pokemon pokemon)
		{
			_pokemon = pokemon;
			nameText.text = pokemon.Base.pokemonName;
			levelText.text = "Lvl" + pokemon.Level;
			hpBar.SetHPData((float)(pokemon.HP / pokemon.MaxHP));
			//hptrack = (float)pokemon.HP;
			//Debug.Log(hptrack);
		}
		public void SetTownData() // for start up only
		{
			nameText.text = Game.mainsessionData.actorType;
			levelText.text = "Lvl" + Game.playerLevel.ToString();
			Debug.Log($"this is currentHp from SetTownData: {Game.mainsessionData.currenthp} / {Game.maxHP}, currentexp: {Game.mainsessionData.exp} / {Game.currentmaxEXP}");
			hpBar.SetHPData((float)(Game.mainsessionData.currenthp / Game.maxHP));
			expBar.SetEXPData((float)(Game.mainsessionData.exp / Game.currentmaxEXP));
		}
		public IEnumerator UpdateBattleData()
		{
			yield return hpBar.SetHPSmooth((float)_pokemon.HP / _pokemon.MaxHP);
			if (Game.currentPokemonType.Contains("enemy"))
				yield break;
			else
				yield return expBar.SetEXPSmooth((float)Game.mainsessionData.exp / Game.currentmaxEXP);
			//hptrack = (float)_pokemon.HP;
			//Debug.Log(hptrack);
		}
		public IEnumerator UpdateTownData()
		{
			levelText.text = "Lvl" + Game.playerLevel.ToString();
			yield return hpBar.SetHPSmooth((float)Game.mainsessionData.currenthp / Game.maxHP);
			yield return expBar.SetEXPSmooth((float)Game.mainsessionData.exp / Game.currentmaxEXP);
			//hptrack = (float)_pokemon.HP;
			//Debug.Log(hptrack);
		}
	}
}

