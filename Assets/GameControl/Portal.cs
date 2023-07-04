using pattayaA3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, Interactable
{
	public LevelController levelController;
	public void Interact()
	{
		levelController.StartNewLevel("Battle View");
	}
}

