using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pattayaA3
{
	//Zephan
	public class StartMenuController : GameSceneController
	{
		public GameObject IntroDialogue;
		public GameObject StartMenu;
		public GameObject SaveMenu;
		public SaveMenuController saveMenuController;
		public GameObject scrollBar;

		public override void Initialize(GameController aController)
		{
			base.Initialize(aController);
		}

		public void StartLevel(string aScene)
		{
			//Debug.Log($"finding Id from StartMenuController: {Game.mainsessionData.levelId}, otherData: {Game.mainsessionData.actorName}, {Game.mainsessionData.actorType} ");
			gameController.LoadScene(aScene);
			//Debug.Log($"finding Id after Loading: {Game.mainsessionData.levelId}, otherData: {Game.mainsessionData.actorName}, {Game.mainsessionData.actorType} ");
			gameController.RemoveScene(sceneName);
		}

		public void StartIntroduction()
		{
			IntroDialogue.SetActive(true);
			StartCoroutine(IntroDialogue.GetComponent<IntroductionDialogManager>().ShowDialog("INTRO"));
			StartMenu.SetActive(false);
		}

		public void OnSaveMenu()
		{
			SaveMenu.SetActive(true);
			saveMenuController.OpenMenu();
			scrollBar.SetActive(true);
		}
	}
}