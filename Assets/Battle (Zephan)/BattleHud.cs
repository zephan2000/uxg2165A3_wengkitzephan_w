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
		public HPBar hpBar;
		public ExpBar expBar;
		public GameObject HpNumbers;
		public GameObject ExpNumbers;
		float hptrack;
		Pokemon _pokemon;
		public void SetData(Pokemon pokemon)
		{
			_pokemon = pokemon;
			nameText.text = pokemon.Base.pokemonName;
			levelText.text = "Lvl" + pokemon.Level;
			hpBar.SetHPSmooth((float)(pokemon.HP / pokemon.MaxHP));
			HpNumbers.GetComponent<Text>().text = $"{_pokemon.HP} / {_pokemon.MaxHP}";
			Debug.Log($"{pokemon.Base.pokemonName} did not run successfully: ");
			expBar.SetEXPSmooth((float)(Game.mainsessionData.exp / Game.currentmaxEXP));
			ExpNumbers.GetComponent<Text>().text = $"{Game.mainsessionData.exp} / {Game.currentmaxEXP}";
			Debug.Log($"{pokemon.Base.pokemonName} ran successfully: ");
            //hptrack = (float)pokemon.HP;
            //Debug.Log(hptrack);
            //Debug.Log("This is player name : " + _pokemon.Base.pokemonName);
            //Debug.Log("This is player maxhp: " + _pokemon.Base.MaxHp);
            //Debug.Log("This is player phys: " + _pokemon.Base.pokemonPhysicalDmg);
            //Debug.Log("This is player mag: " + _pokemon.Base.pokemonMagicDmg);
        }
		public void SetEnemyData(Pokemon pokemon)
		{
			_pokemon = pokemon;
			nameText.text = pokemon.Base.pokemonName;
			levelText.text = "Lvl" + pokemon.Level;
			hpBar.SetHPData((float)(pokemon.HP / pokemon.MaxHP));
            //hptrack = (float)pokemon.HP;
            //Debug.Log(hptrack);

            //Debug.Log("This is monster name : " + _pokemon.Base.pokemonName);
            //Debug.Log("This is monster maxhp: " + _pokemon.Base.MaxHp);
            //Debug.Log("This is monster phys: " + _pokemon.Base.pokemonPhysicalDmg);
            //Debug.Log("This is monster mag: " + _pokemon.Base.pokemonMagicDmg);
        }
		public IEnumerator UpdateBattleData()
		{
			yield return hpBar.SetHPSmooth((float)_pokemon.HP / _pokemon.MaxHP);
			if (!_pokemon.Base.pokemonActorType.Contains("enemy"))
			{
				HpNumbers.GetComponent<Text>().text = $"{_pokemon.HP} / {_pokemon.MaxHP}";
				ExpNumbers.GetComponent<Text>().text = $"{Game.mainsessionData.exp} / {Game.currentmaxEXP}";
			}
			
			if (Game.currentPokemonType.Contains("enemy"))
				yield break;
			else
				yield return expBar.SetEXPSmooth((float)Game.mainsessionData.exp / Game.currentmaxEXP);
			//hptrack = (float)_pokemon.HP;
			//Debug.Log(hptrack);
		}
		public IEnumerator UpdateTownData()
		{
			nameText.text = Game.mainsessionData.actorType;
			levelText.text = "Lvl" + Game.playerLevel.ToString();
			Debug.Log($"this is currentHp from UpdateTownData: {Game.mainsessionData.currenthp} / {Game.mainsessionData.maxhp}, currentexp: {Game.mainsessionData.exp} / {Game.currentmaxEXP}");
			HpNumbers.GetComponent<Text>().text = $"{Game.mainsessionData.currenthp} / {Game.mainsessionData.maxhp}";
			ExpNumbers.GetComponent<Text>().text = $"{Game.mainsessionData.exp} / {Game.currentmaxEXP}";
			yield return hpBar.SetHPSmooth((float)Game.mainsessionData.currenthp / Game.maxHP);
			yield return expBar.SetEXPSmooth((float)Game.mainsessionData.exp / Game.currentmaxEXP);
			//hptrack = (float)_pokemon.HP;
			//Debug.Log(hptrack);
		}
		public void SetTownData()
		{
			nameText.text = Game.mainsessionData.actorType;
			levelText.text = "Lvl" + Game.playerLevel.ToString();
			Debug.Log($"this is currentHp from UpdateTownData: {Game.mainsessionData.currenthp} / {Game.mainsessionData.maxhp}, currentexp: {Game.mainsessionData.exp} / {Game.currentmaxEXP}");
			HpNumbers.GetComponent<Text>().text = $"{Game.mainsessionData.currenthp} / {Game.mainsessionData.maxhp}";
			ExpNumbers.GetComponent<Text>().text = $"{Game.mainsessionData.exp} / {Game.currentmaxEXP}";
			hpBar.SetHPData((float)Game.mainsessionData.currenthp / Game.maxHP);
			expBar.SetEXPData((float)Game.mainsessionData.exp / Game.currentmaxEXP);
			//hptrack = (float)_pokemon.HP;
			//Debug.Log(hptrack);
		}
	}
}

