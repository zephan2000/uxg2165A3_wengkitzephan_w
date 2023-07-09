using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pattayaA3
{
	//Zephan
	public class StartMenuController : GameSceneController
	{
		public GameObject IntroDialogue;
		public GameObject StartMenu;
		public override void Initialize(GameController aController)
		{
			base.Initialize(aController);
		}

		public void StartLevel(string aScene)
		{
			gameController.LoadScene(aScene);
			gameController.RemoveScene(sceneName);
		}

		public void StartIntroduction()
		{
			IntroDialogue.SetActive(true);
			StartCoroutine(IntroDialogue.GetComponent<IntroductionDialogManager>().ShowDialog("INTRO"));
			StartMenu.SetActive(false);
		}
	}
}