using pattayaA3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Zephan
namespace pattayaA3
{
	public enum BattleState { Start,ActionSelection,MoveSelection,ExecuteTurn,Busy, BattleOver} //busy is for when either enemy or player are making moves
	public enum BattleAction {Move, UseItem, Run} //UseItem is able to add potions if needed, can use this state for upgrade skills or view stats during battle
	public class BattleSystem : GameSceneController
	{
		private Camera mainCamera;
		[SerializeField] BattleUnit enemyUnit;
		[SerializeField] BattleUnit playerUnit;
		[SerializeField] BattleDialogBox dialogBox;
		[SerializeField] Image battleBackground;
		private bool isStarted;
		private bool playerWon;

		BattleState state;
		int currentAction;
		int currentMove;
		private void Start()
		{
			StartCoroutine(SetupBattle());
		}

		public override void Initialize(GameController aController)
		{
			isStarted = false;
			base.Initialize(aController);
			isStarted = true;
		}
		public IEnumerator SetupBattle()
		{
			Debug.Log("Setting up");
			//Set a new background for boss battle
			//Debug.Log($"{Game.sessionactorName},{Game.sessionactorType}");
			Debug.Log($"{Game.chosenenemyName},{Game.chosenenemyType}");
			//playerUnit.BattleUnitSetup(Game.sessionactorName,Game.sessionactorType);
			playerUnit.BattleUnitSetup("Warrior", "playerWarrior");
			enemyUnit.BattleUnitSetup(Game.chosenenemyName, Game.chosenenemyType);

			dialogBox.SetMoveName(playerUnit.Pokemon.Moves);
			yield return dialogBox.TypeDialog($"A wild {enemyUnit.Pokemon.Base.pokemonName} has appeared.");
			yield return new WaitForSeconds(1f);

			ActionSelection();
		}

		void ActionSelection()
		{
			state = BattleState.ActionSelection;
			StartCoroutine(dialogBox.TypeDialog("Choose an action"));
			dialogBox.EnableActionSelector(true);
		}

		void MoveSelection()
		{
			state = BattleState.MoveSelection;
			dialogBox.EnableActionSelector(false);
			dialogBox.EnableDialogText(false);
			dialogBox.EnableMoveSelector(true);
		}	
		// comment here for battle architecture, to be able to keep track of state machine
		IEnumerator RunTurns(BattleAction playerAction)
		{
			state = BattleState.ExecuteTurn;

			if(playerAction == BattleAction.Move)
			{
				playerUnit.Pokemon.CurrentMove = playerUnit.Pokemon.Moves[currentMove];
				enemyUnit.Pokemon.CurrentMove = enemyUnit.Pokemon.GetRandomMove();

				int playerMovePriority = playerUnit.Pokemon.CurrentMove.moveBase.movePriority;
				int enemyMovePriority = enemyUnit.Pokemon.CurrentMove.moveBase.movePriority;


				// check who goes first, Replaced ChooseFirstTurn()
				bool playerGoesFirst = true;
				if(enemyMovePriority > playerMovePriority) //checking based on priority
				{
					playerGoesFirst = false;
				}
				else if (enemyMovePriority ==  playerMovePriority)
				{
					playerGoesFirst = playerUnit.Pokemon.Speed >= enemyUnit.Pokemon.Speed; // returns true if playerUnit is faster
				}
				Debug.Log(playerGoesFirst);
				var fasterUnit = (playerGoesFirst) ? playerUnit : enemyUnit;
				//slowerUnit is to dictate who goes second to pass into RunMove() not to determine who is faster
				var slowerUnit = (playerGoesFirst) ? enemyUnit : playerUnit; 
				
				//First Turn
				yield return RunMove(fasterUnit, slowerUnit, fasterUnit.Pokemon.CurrentMove);
				if (state == BattleState.BattleOver) yield break;
				//Second Turn
				yield return RunMove(slowerUnit,fasterUnit, slowerUnit.Pokemon.CurrentMove);
				if (state == BattleState.BattleOver) yield break;

				ActionSelection();
			}
		}
		// sourceUnit is the attacker, targetUnit is the victim of the move
		IEnumerator RunMove(BattleUnit sourceUnit, BattleUnit targetUnit, Move move)
		{ 
			yield return dialogBox.TypeDialog($"{sourceUnit.Pokemon.Base.pokemonName} used {move.moveBase.moveName}");
			move.UsesLeft--;
			yield return new WaitForSeconds(1f);
			if(move.moveBase.moveCategory == MoveCategory.Passive) // status = passive, may remove if no effect since heal function is already done
			{
				yield return StartCoroutine(RunMoveEffects(sourceUnit.Pokemon, targetUnit.Pokemon, move)); ;
			}
			else
			{
				yield return StartCoroutine(CheckForHeal(sourceUnit, targetUnit, move)); 
			}
			
			if (targetUnit.Pokemon.HP <= 0)
			{
				yield return dialogBox.TypeDialog($"{targetUnit.Pokemon.Base.pokemonName} Fainted");
				playerWon = true;
				yield return new WaitForSeconds(2f);
				CheckForBattleOver(targetUnit);
			}
		}

		IEnumerator CheckForHeal(BattleUnit sourceUnit, BattleUnit targetUnit, Move move) // Initialises the move and checks for heal
		{
			if (move.moveBase.moveTarget == MoveTarget.Self) // for heal
			{
				sourceUnit.Pokemon.InitMove(move, sourceUnit.Pokemon);
				yield return sourceUnit.Hud.UpdateHP();
			}
			else
			{
				targetUnit.Pokemon.InitMove(move, sourceUnit.Pokemon);
				yield return targetUnit.Hud.UpdateHP();
			}
		}
		IEnumerator RunMoveEffects( Pokemon sourceUnit, Pokemon targetUnit, Move move) // encapsulation so that status changes can be added
		{
			//var effects = move.moveBase.GetEffects();
			//if (effects.Boosts != null)
			//{
			//	if (move.moveBase.GetMoveTargetFromSkill() == MoveTarget.Self)
			//		sourceUnit.ApplyBoosts(effects.Boosts);
			//	else
			//		targetUnit.ApplyBoosts(effects.Boosts);
			//}
			yield return null;
		}

		void CheckForBattleOver(BattleUnit faintedUnit)
		{
			if(faintedUnit.IsPlayerUnit)
			{
				BattleOver(false);
			}
			else
			{
				BattleOver(true);
			}

		}
		void BattleOver(bool battleStatus)
		{
			playerWon = battleStatus;
			state = BattleState.BattleOver;
			ExitLevel("Town");
		}

		public void Update()
		{
			if (state == BattleState.ActionSelection)
			{
				HandleActionSelection();
			}
			else if (state == BattleState.MoveSelection)
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
					MoveSelection();
				}
				else if (currentAction == 1)
				{
					playerWon = false;
					ExitLevel("Town");
				}
			}
		}
		// video #8 done but there something might be wrong with the playerunit or scripatble object do a check, so far so good
		void HandleMoveSelection() 
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				if (currentMove < playerUnit.Pokemon.Moves.Count - 1)
				{
					++currentMove;
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
			dialogBox.UpdateMoveSelection(currentMove, playerUnit.Pokemon.Moves[currentMove]); //Moves is the list of moves

			if (Input.GetKeyDown(KeyCode.Return))
			{
				var move = playerUnit.Pokemon.Moves[currentMove];
				if (move.UsesLeft == 0) return;

				dialogBox.EnableMoveSelector(false);
				dialogBox.EnableDialogText(true);
				StartCoroutine(RunTurns(BattleAction.Move));
			}
		}
		public void ExitLevel(string aScene)
		{
			gameController.LoadScene(aScene);
			gameController.RemoveScene(sceneName);
		}
	}
}

