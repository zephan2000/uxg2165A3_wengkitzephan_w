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
		public GameObject nosaveFile;

		public override void Initialize(GameController aController)
		{
			base.Initialize(aController);
		}

		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.N))
			{
				Game.StartNewGame("playerWarrior_1");
			}
		}
		public void StartLevel(string aScene)
		{
			if(Game.mainsessionData !=null)
			{
				Debug.Log($"finding Id from StartMenuController: {Game.mainsessionData.levelId}, otherData: {Game.mainsessionData.actorName}, {Game.mainsessionData.actorType} ");
				gameController.LoadScene(aScene);
				Game.ProcessSaveData();
				Debug.Log($"finding Id after Loading: {Game.mainsessionData.levelId}, otherData: {Game.mainsessionData.actorName}, {Game.mainsessionData.actorType} ");
				gameController.RemoveScene(sceneName);
			}
			else
			{
				nosaveFile.GetComponent<Text>().text = "No session data assigned";
				StartCoroutine(startMenuWarning());
			}
		}

		public void StartIntroduction()
		{
			IntroDialogue.SetActive(true);
			StartCoroutine(IntroDialogue.GetComponent<IntroductionDialogManager>().ShowDialog("INTRO"));
			StartMenu.SetActive(false);
		}

		public void OnSaveMenu()
		{
			if (Game.saveList != null)
			{
				SaveMenu.SetActive(true);
				saveMenuController.OpenMenu();
				scrollBar.SetActive(true);
			}
			else
			{
				nosaveFile.GetComponent<Text>().text = "No Save Files";
				StartCoroutine(startMenuWarning());
			}
			
		}

		public IEnumerator startMenuWarning()
		{
			yield return new WaitForSeconds(0.2f);
			yield return Input.GetMouseButtonDown(0);
			nosaveFile.SetActive(true);
			yield return new WaitForSeconds(1f);
			nosaveFile.SetActive(false);
		}
	}
}