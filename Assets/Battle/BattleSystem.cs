using pattayaA3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pattayaA3
{
	public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy } //busy is for when either enemy or player are making moves
	public class BattleSystem : GameSceneController
	{
		[SerializeField] BattleUnit enemyUnit;
		[SerializeField] BattleUnit playerUnit;
		[SerializeField] BattleHud playerHud;
		[SerializeField] BattleHud enemyHud;
		[SerializeField] BattleDialogBox dialogBox;

		BattleState state;
		int currentAction;
		int currentMove;
		private void Start()
		{
			StartCoroutine(SetupBattle());
		}

		public IEnumerator SetupBattle()
		{
			playerUnit.Setup();
			enemyUnit.Setup();
			playerHud.SetData(playerUnit.Pokemon);
			enemyHud.SetData(enemyUnit.Pokemon);

			dialogBox.SetMoveName(playerUnit.Pokemon.Moves);
			yield return dialogBox.TypeDialog($"A wild {enemyUnit.Pokemon._base.GetName()} has appeared.");
			yield return new WaitForSeconds (1f);

			PlayerAction();
		}	

		void PlayerAction()
		{
			state = BattleState.PlayerAction;
			StartCoroutine(dialogBox.TypeDialog("Choose an action"));
			dialogBox.EnableActionSelector(true);
		}

		void PlayerMove()
		{
			state = BattleState.PlayerMove;
			dialogBox.EnableActionSelector(false);
			dialogBox.EnableDialogText(false);
			dialogBox.EnableMoveSelector(true);
		}	
		private void Update()
		{
			if (state == BattleState.PlayerAction)
			{
				HandleActionSelection();
			}
			else if (state == BattleState.PlayerMove)
			{
				HandleMoveSelection();
			}
		}

		void HandleActionSelection()
		{
			if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				if (currentAction < 1)
					++currentAction;
			} 
			else if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				if (currentAction > 0)
					--currentAction;
			}
			dialogBox.UpdateActionSelection(currentAction);

			if(Input.GetKeyDown(KeyCode.Return))
			{
				if(currentAction == 0)
				{
					PlayerMove();
				}
				else if (currentAction == 1)
				{

				}
			}
		}
		// video #8 done but there something might be wrong with the playerunit or scripatble object do a check
		void HandleMoveSelection() 
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				if (currentMove < playerUnit.Pokemon.Moves.Count - 1)
				{
					++currentMove;
					Debug.Log("right");
				}
					
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if (currentMove > 0)
					--currentMove;
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				if (currentMove < playerUnit.Pokemon.Moves.Count - 2)
					currentMove += 2;
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				if (currentMove > 1)
					currentMove -= 2;
			}
			dialogBox.UpdateMoveSelection(currentAction, playerUnit.Pokemon.Moves[currentMove]); //Moves is the list of moves
		}	
	}
}

