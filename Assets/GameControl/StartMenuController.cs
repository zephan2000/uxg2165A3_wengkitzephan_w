using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pattayaA3
{
	//Zephan
	public class StartMenuController : GameSceneController
	{
		public override void Initialize(GameController aController)
		{
			base.Initialize(aController);
		}

		public void StartLevel(string aScene)
		{
			gameController.LoadScene(aScene);
			gameController.RemoveScene(sceneName);
		}
	}
}