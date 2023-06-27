using pattayaA3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pattayaA3
{
	public class BattleSystem : GameSceneController
	{
		[SerializeField] BattleUnit enemyUnit;
		[SerializeField] BattleUnit playerUnit;
		[SerializeField] BattleHud playerHud;
		[SerializeField] BattleHud enemyHud;
		[SerializeField] BattleDialogBox dialogueBox;

		private void Start()
		{
			SetupBattle();
		}

		public void SetupBattle()
		{
			playerUnit.Setup();
			enemyUnit.Setup();
			playerHud.SetData(playerUnit.Pokemon);
			enemyHud.SetData(enemyUnit.Pokemon);
			dialogueBox.SetDialog($"A wild {enemyUnit.Pokemon._base.GetName()} has appeared");
		}	
	}
}

