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

		public void SetData(Pokemon enemy)
		{
			nameText.text = enemy._base.GetName();
			levelText.text = "Lvl" + enemy.level;
			hpBar.SetHP((float)enemy.HP / enemy.MaxHP);
		}
	}
}

