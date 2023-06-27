using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pattayaA3
{
	public class BattleDialogBox : MonoBehaviour
	{
		[SerializeField] Text dialogText;

		public void SetDialog(string dialog)
		{
			dialogText.text = dialog;
		}

		public IEnumerator TypeDialog(string dialog) // animating dialog to reveal letter by letter
		{
			dialogText.text = "";
			foreach (var letter in dialog.ToCharArray()) 
			{
				dialogText.text += letter;
				yield return new WaitForSeconds(1f / 30);
			}
		}
	} 
}


