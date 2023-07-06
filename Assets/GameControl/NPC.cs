using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Zephan
public class NPC : MonoBehaviour, Interactable
{
	[SerializeField] Dialog dialog;
	public void Interact()
	{
		StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
	}
}
