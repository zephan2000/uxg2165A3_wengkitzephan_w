using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
	// Start is called before the first frame update
	public void Interact()
	{
		//StartCoroutine(DialogManager.Instance.ShowDialog(dialog)); //instead of taking in dialog, run Game.FindDialogByDialogType(dialogtype)
	}
}
