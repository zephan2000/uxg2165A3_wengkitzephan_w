using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pattayaA3
{
	
	public class BattleDialogBox : MonoBehaviour
	{
		[SerializeField] Text dialogText;
		[SerializeField] GameObject actionSelector;
		[SerializeField] GameObject moveSelector;
		[SerializeField] GameObject moveDetails;

		[SerializeField] List<Text> actionTexts;
		[SerializeField] List<Text> moveTexts;

		[SerializeField] Text cooldownText;
		[SerializeField] Text priorityText;
		[SerializeField] Text typeText;
		[SerializeField] Color highlightedColor;
		public void SetDialog(string dialog)
		{
			dialogText.text = dialog;
		}

		public IEnumerator TypeDialog(string dialog) // animating dialog to reveal letter by letter
		{
			dialogText.text = "";
			Debug.Log(dialogText.text);
			foreach (var letter in dialog.ToCharArray()) 
			{
				dialogText.text += letter;
					yield return new WaitForSeconds(1f / 30);
			}
		}

		public void EnableDialogText(bool enabled)
		{
			dialogText.enabled = enabled;
		}
		public void EnableActionSelector(bool enabled)
		{
			actionSelector.SetActive(enabled);
		}
		public void EnableMoveSelector(bool enabled)
		{
			moveSelector.SetActive(enabled);
			moveDetails.SetActive(enabled);
		}
		public void UpdateActionSelection(int selectedAction)
		{
			for(int i=0; i<actionTexts.Count; i++)
			{
				if (i == selectedAction)
				{
					actionTexts[i].color = highlightedColor;
				}
				else
					actionTexts[i].color = Color.black;

			}
		}
		public void UpdateMoveSelection(int selectedMove, Move move) //
		{
			for (int i = 0; i < moveTexts.Count; i++)	
			{
				if (i == selectedMove)
				{
					moveTexts[i].color = highlightedColor;
				}
				else
					moveTexts[i].color = Color.black;
			}
			cooldownText.text = $"Uses {move.UsesLeft}/ {move.moveBase.moveMaxUses}";
			priorityText.text = $"Priority: {move.moveBase.movePriority}";
			typeText.text = $"{move.moveBase.moveCategory}";
			if (move.UsesLeft == 0)
				cooldownText.color = Color.red;
			else
				cooldownText.color = Color.black;
		}
		public void SetMoveName(List<Move> moves)
		{
			for(int i = 0; i<moveTexts.Count; i++)
			{
				if (i < moves.Count)
					moveTexts[i].text = moves[i].moveBase.moveName;
				else
					moveTexts[i].text = "-";
			}
		}
	} 
}


