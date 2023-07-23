using pattayaA3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Zephan
public class Portal : MonoBehaviour, Interactable
{
	public LevelController levelController;
	public void Interact()
	{
		//Game.SetChosenEnemyActorDisplayName("Dark Wizard");
		//Game.SetChosenEnemyActorType("enemyWizard");
		Game.chosenenemyName = "Dark Wizard";
		Game.chosenenemyType = "enemyWizard";
		if (Game.mainsessionData.currenthp != Game.mainsessionData.maxhp)
		{
			Game.isBossWarning = true;
			Debug.Log("showing warning");
			StartCoroutine(TownDialogManager.Instance.ShowDialog("WARNING"));
		}
		else
		{
			levelController.StartNewLevel("Battle View");
		}
	}
}

