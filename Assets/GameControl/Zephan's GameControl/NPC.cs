using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Zephan
public class NPC : MonoBehaviour, Interactable
{
	//[SerializeField] Dialog dialog;
	public void Interact()
	{
		if(Game.questComplete && !Game.rewardCollected)
		{
			StartCoroutine(TownDialogManager.Instance.ShowDialog("QC"));
		}
		else
		{
			StartCoroutine(TownDialogManager.Instance.ShowDialog("QUEST"));
		}
		
	}
}
