using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace pattayaA3
{
	//Zephan
	//public enum GameState { FreeRoam, Dialog}
	public class GameController : MonoBehaviour
	{
		private PlayerScript player;
		private GameSceneController currentSceneController;
		public string initialScene = "StartScreen";
		public GameObject gameOverScreen;
		public GameObject pauseScreen;
		public GameObject warningText;
		public GameObject progressHUD;
		private bool isGameOver;
		private bool isPaused;
		GameState state;
		void Start()
		{
			//set initial state
			SetPause(false);

			//Raiyan's Code
			DataManager dataManager = GetComponent<DataManager>();
			dataManager.LoadRefData();
            
            //Game.GetSave();
			//Debug.Log($"This is mainsessionData: {Game.mainsessionData.saveId}");// gets active data;

			//load initial scene
			LoadScene(initialScene);

			//DialogManager.Instance.OnShowDialog += () =>
			//{
			//	state = GameState.Dialog;
			//};

			//DialogManager.Instance.OnCloseDialog += () =>
			//{
			//	if(state == GameState.Dialog) //for cases where you want to go to battle straight after dialog
			//	state = GameState.FreeRoam;
			//};
		}

		public void LoadScene(string aScene)
		{
			AsyncOperation loadSceneOp = SceneManager.LoadSceneAsync(aScene, LoadSceneMode.Additive);
			loadSceneOp.completed += (result) =>
			{
				var getRootObj = SceneManager.GetSceneByName(aScene).GetRootGameObjects();
				for (int i = 0; i < getRootObj.Length; i++)
				{
					if (currentSceneController = getRootObj[i].GetComponentInChildren<GameSceneController>())
					{
						break;
					}
				}

				currentSceneController.Initialize(this);
			};
		}

		public void RemoveScene(string aScene)
		{
			AsyncOperation UnloadScene = SceneManager.UnloadSceneAsync(aScene);
		}
		public void StartLevel(PlayerScript playerScript)
		{
			player = playerScript;
			//SetPause(false);
        }
		public PlayerScript getPlayer()
		{
			return player;
		}
		public void GoToLevelSelect()
		{
			SetPause(false);

			if (currentSceneController != null) RemoveScene(currentSceneController.sceneName);
			LoadScene("StartScreen");
		}
		public string getactiveSceneName()
		{
			if (currentSceneController != null) return currentSceneController.sceneName;
			else
				return null;
		}

		public void TogglePause()
		{
			SetPause(!isPaused);
		}

		public void SetPause(bool aPause)
		{
			isPaused = aPause;
			if (isPaused == true)
			{
				Time.timeScale = 0;
				pauseScreen.SetActive(true);
			}
			else
			{
				Time.timeScale = 1f;
				pauseScreen.SetActive(false);
			}
		}
		public bool CheckGameOver()
		{
			//check if game over
			return isGameOver;
		}

		public void UpdateProgressHUD(int numCollected, int numTotal)
		{
			progressHUD.GetComponent<Text>().text = "Progress: " + numCollected + "/" + numTotal;

			if (numCollected >= numTotal)
			{
				progressHUD.GetComponent<Text>().text += " DONE!\nHead to Exit";
			}
		}
	}
}