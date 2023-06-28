using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace pattayaA3
{
	//Zephan
	public class LevelController : GameSceneController
	{
		private Camera mainCamera;
		private PlayerScript player;
		//private List<EnemyScript> enemyList;
		//private List<CollectibleScript> collectibleList;
		//private EndPointScript endPoint;
		private int collectedCount;
		private bool isStarted;
		private bool isGameOver;
		private bool isPaused;
		public float outOfBoundsDuration = 3f;
		private float outOfBoundsTimer;

		public override void Initialize(GameController aController)
		{
			isStarted = false;

			base.Initialize(aController);

			mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

			//clear auto destroy objects
			//foreach (DamagerScript autoDestroy in FindObjectsOfType<DamagerScript>())
			//{
			//	autoDestroy.DestroyDamager();
			//}

			//initialize player script
			if (player == null) player = FindObjectOfType<PlayerScript>();
			if (player != null) player.Initialize(this);

			
			gameController.StartLevel(player);


			isStarted = true;
		}

		public void SetGameOver(bool aGameOver, bool isWin)
		{
			//gameController.SetGameOver(aGameOver, isWin, collectedCount, collectibleList.Count);
		}

		public bool CheckGameOver()
		{
			//check if game over
			return gameController.CheckGameOver();
		}
		public void StartNewLevel(string aScene)
		{
			gameController.LoadScene(aScene);
			gameController.RemoveScene(sceneName);
		}

		public PlayerScript GetPlayer()
		{
			return player;
		}

		public bool CheckIsStarted()
		{
			return isStarted;
		}
	}
}