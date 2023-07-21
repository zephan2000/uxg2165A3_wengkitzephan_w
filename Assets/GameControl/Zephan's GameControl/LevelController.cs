using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace pattayaA3
{
	//Zephan
	public enum GameState { FreeRoam, Dialog, Shop, Inventory, Training, SaveMenu, SaveMenuSelection}
	public enum QuestStatus { Inactive, Ongoing, Completed} //implement this
	public class LevelController : GameSceneController
	{
		private Camera mainCamera;
		private PlayerScript player;
		private bool isStarted;
		private bool isGameOver;
		private bool isPaused;
		public float outOfBoundsDuration = 3f;
		private float outOfBoundsTimer;
		public GameObject playerobj;
		public GameState state;
		public QuestStatus questStatus;

        public GameObject inventory;
		public GameObject questHud;
		public GameObject achievementStatus;
		public inventorybox inventorybox;
		public Invent invent;
		public bool isOpenInventory;

		public ShopExist shopexist;
		public bool isOpenShop;

        private bool isOpenTrainingCenter;
		public BattleHud playerHud;
		public GameObject pauseMenu;
		public GameObject pauseMenuPrefab;
		public List<GameObject> childList;
		public GameObject trainingCenterBackground;
		private int currentChoice = 0;
		private bool isSaveMenuOpen;
		private bool allowEscape;

        void Start()
		{
			TownDialogManager.Instance.OnShowDialog += () =>
			{
				state = GameState.Dialog;
			};

			TownDialogManager.Instance.OnCloseDialog += () =>
			{
				if (state == GameState.Dialog) //for cases where you want to go to battle straight after dialog
					state = GameState.FreeRoam;
			};
			TownDialogManager.Instance.OnCloseWarningDialog += () =>
			{
				if (state == GameState.Dialog)
				{
					
					state = GameState.Training;
					Debug.Log($"setting Gamestate from Dialog to {state}");
				}	
			};
			TownDialogManager.Instance.StartBattleQuest += () =>
			{
				StartBattleQuestHud();
			};
			TownDialogManager.Instance.OnHeal += () =>
			{
				player.RestoreHealth();
				Debug.Log("heal invoked");
			};
			TownDialogManager.Instance.RewardCollected += () =>
			{
				RewardCollectedHud();
			};
			Game.ProcessSaveData();
			Game.GetSave(); //why does the game data disappear after writing?

			//Debug.Log($"finding Id {Game.mainsessionData.levelId}, otherData: {Game.mainsessionData.actorName}, {Game.mainsessionData.actorType} ");
			Game.SetSessionDataFromLevelId(Game.mainsessionData.levelId);
			//Debug.Log($"finding maxHp by Id: {Game.mainsessionData.maxhp}");
			trainingCenterBackground.SetActive(false);
			if (Game.mainsessionData.startedQuest != "")
			{
				questStatus = QuestStatus.Ongoing;
				Game.questInProgress = true;
				Game.SetQuestData();
				StartBattleQuestHud();
				CheckQuestProgress();
			}
			if(questStatus == QuestStatus.Completed)
			{
				QuestCompleteHud();
			}
			
			playerHud.SetTownData(); // set for start up
			CheckForLevelUp();

			//set EXP and HP data on Start()
			Debug.Log(Game.saveList[0].saveId); //works
		}
		private void Update()
		{
			//if (player != null && !isGameOver && Input.GetKeyDown(KeyCode.Escape))
			//{
			//	TogglePause();
			//}	
			if (state == GameState.FreeRoam && player != null)
			{
				player.HandleUpdate();
			}
			else if (state == GameState.Dialog)
			{
				if(Input.GetKey(KeyCode.M)) 
				{ 
					Debug.Log($"Handling Dialog Update, state is {state}"); 
				}
				
				TownDialogManager.Instance.HandleUpdate();
			}
			if (player.isTouchingDoor == true && Input.GetKeyDown(KeyCode.Z) && state == GameState.FreeRoam | state == GameState.Training | state == GameState.Shop)
			{
				Debug.Log($"pressing Z {state}");
				if (player.GetGameObject().name == "Training Center Door")
				{
                    ToggleTrainingCenter();
                }
				else if (player.GetGameObject().name == "Shop Door")
				{
					shopexist.ToggleShop(isOpenShop);
					if (!isOpenShop)
					{
                        state = GameState.Shop;
                    }
					else
					{
                        state = GameState.FreeRoam;
                    }
					isOpenShop = !isOpenShop;
				}
				
			}
			//Raiyan
			if (Input.GetKeyDown(KeyCode.I) && state == GameState.FreeRoam | state == GameState.Inventory)
            {
				if (!isOpenInventory)
				{
					state = GameState.Inventory;
					invent.ToggleInventory(isOpenInventory);
					isOpenInventory = !isOpenInventory;
				}
				else
				{
					invent.ToggleInventory(isOpenInventory);
					isOpenInventory = !isOpenInventory;
					state = GameState.FreeRoam;
				}
            }

			if (Input.GetKeyDown(KeyCode.Escape) && state == GameState.FreeRoam | state == GameState.SaveMenu)
			{
				ToggleSaveMenu();
			}
			if(state == GameState.SaveMenuSelection)
			{
				HandleSelection();
				if(allowEscape)
				{
					state = GameState.FreeRoam;
					GameObject.Destroy(pauseMenu);
					childList.Clear();
				}
			}
            
            //inventorybox.CheckMenu();			
        }
        public override void Initialize(GameController aController)
		{
			isStarted = false;

			base.Initialize(aController);

			mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

			//initialize player script
			if (player == null) player = FindObjectOfType<PlayerScript>();
			if (player != null) player.Initialize(this);
			if(player.currentposition != Vector3.zero) // need more work on this
			{
				Debug.Log("ran");
				player.currentposition = playerobj.transform.position;
			}
			

			gameController.StartLevel(player); // check what is the current player position


			isStarted = true;
		}

		public void CheckForLevelUp()
		{
			if (Game.mainsessionData.exp >= Game.currentmaxEXP)
			{
				Debug.Log("checking for level up");
				StartCoroutine(player.LevelUp());
				StartCoroutine(playerHud.UpdateTownData());
			}
		}
		public bool CheckGameOver()
		{
			//check if game over
			return gameController.CheckGameOver();
		}
		public void StartNewLevel(string aScene)
		{
			player.currentposition = player.GetCurrentPosition(); // check what is the current player position
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
		public void UpdateChoiceSelection(int selectedChoice)
		{
			for (int i = 0; i < childList.Count; i++)
			{
				if (i == selectedChoice)
				{
					childList[i].GetComponent<Text>().color = Color.blue;
				}
				else
					childList[i].GetComponent<Text>().color = Color.black;
			}
			//dialog Text is handled in TypeDialog already
		}
		void HandleSelection() //for vertical choice selector
		{
			//Debug.Log(childList.Count);
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				Debug.Log($"before down arrow, currentchoice = {currentChoice}");
				if (currentChoice < childList.Count)
					++currentChoice;
				Debug.Log($"After down arrow, currentchoice = {currentChoice}");
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				if (currentChoice > 0)
					--currentChoice;
			}
			UpdateChoiceSelection(currentChoice);

			if (Input.GetKeyDown(KeyCode.Return))
			{
				HandleSaveMenuSelection(); 

			}
			if(state == GameState.SaveMenuSelection && Input.GetKeyDown(KeyCode.Escape))
			{
				StartCoroutine(CheckForEscape());
			}
		}
		public IEnumerator CheckForEscape()
		{
			yield return new WaitForSeconds(0.2f);
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape));
			allowEscape = true;
		}

		void HandleSaveMenuSelection()
		{
			switch(childList[currentChoice].GetComponent<Text>().text)
			{
				case "Resume":
					state = GameState.FreeRoam;
					if(pauseMenu != null)
					{
						GameObject.Destroy(pauseMenu);
						childList.Clear();
					}
					currentChoice = 0;
					break;
				case "Save":
					Debug.Log("Saving");
					Game.SaveToJSON<save>(Game.saveList);
					currentChoice = 0;
					break;
				case "Exit":
					gameController.GoToLevelSelect();
					currentChoice = 0;
					//Game.mainsessionData.saveStatus = "INACTIVE";
					//Game.SaveToJSON<save>(Game.saveList);
					if (pauseMenu != null)
					{
						GameObject.Destroy(pauseMenu);
						childList.Clear();
					}
					break;
			}
		}
		public void SetTrainingCenter(bool aInventory)
		{
			isOpenTrainingCenter = aInventory;
			Debug.Log($"setting training center, state is: {state}");
			if (isOpenTrainingCenter == true )
			{
				state = GameState.Training;
				Debug.Log($"attempting to interact, state is: {state}");
				trainingCenterBackground.GetComponent<TrainingCenterControl>().Interact();
			}
			else
			{
				//if(Game.dialogIsOpen == true)
				//{
				//	TownDialogManager.Instance.dialogBox.SetActive(false);
				//}
				state = GameState.FreeRoam;
				trainingCenterBackground.GetComponent<TrainingCenterControl>().OffTrainingCenter();

				player.isTouchingDoor = false;
			}
		}
		public void ToggleTrainingCenter()
		{

			SetTrainingCenter(!isOpenTrainingCenter);
		}

		public void SetSaveMenu(bool aInventory)
		{
			isSaveMenuOpen = aInventory;
			if (isSaveMenuOpen == true)
			{
				state = GameState.SaveMenu;
				pauseMenu = Instantiate(pauseMenuPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				pauseMenu.transform.SetParent(GameObject.FindGameObjectWithTag("Pause Menu").transform, false);
				
				//pauseMenu.GetComponent<GameObject>().SetActive(false);
				if (childList.Count <= 0)
				{
					foreach (Transform child in pauseMenu.transform.GetChild(0))
					{
						if (child.gameObject.CompareTag("Choice") == true)
						{
							childList.Add(child.gameObject);
							Debug.Log(child.gameObject.name);
						}
					}
				}
				allowEscape = false;
				state = GameState.SaveMenuSelection;
			}
			else
			{
				state = GameState.FreeRoam;
				GameObject.Destroy(pauseMenu);
				childList.Clear();
			}
		}
		public void ToggleSaveMenu()
		{

			SetSaveMenu(!isSaveMenuOpen);
		}

		public void StartBattleQuestHud()
		{
			questHud.SetActive(true);
			questHud.transform.GetChild(0).GetComponent<Text>().text = $"Quest Requirement: {Game.startedQuest.questName}";
			questHud.transform.GetChild(1).GetComponent<Text>().text = $"Progress: {Game.battleQuestProgress}/{Game.startedQuest.questReq}";
			CheckQuestProgress();
			Game.SaveToJSON<save>(Game.saveList);
		}
		public void QuestCompleteHud()
		{
			questHud.SetActive(true);
			questHud.transform.GetChild(0).GetComponent<Text>().text = $"Talk to NPC for reward! {Game.startedQuest.questName} completed!";
			questHud.transform.GetChild(1).GetComponent<Text>().text = $"Progress: {Game.battleQuestProgress}/{Game.startedQuest.questReq}";
			Game.mainsessionData.startedQuest = Game.startedQuest.questId;
			
			Game.SaveToJSON<save>(Game.saveList);
		}

		public void RewardCollectedHud()
		{
			questHud.SetActive(false);
			questStatus = QuestStatus.Completed;
		}
		public void CheckQuestProgress()
		{
			if (Game.battleQuestProgress == Game.startedQuest.questReq)
			{
				Game.questComplete = true;
				QuestCompleteHud();
			}
				
			else Game.questComplete = false;
		}

		//public void ActivateInvent()
		//{
		//	test = Instantiate(menu, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		//	test.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform, false);
		//	//Instantiate(canvas);
		//}

			//public void DeActivateInvent()
			//{
			//	GameObject.Destroy(test);
			//}

			//public void ToggleInventory(bool isOpenInventory)
			//{
			//	SetInventory(!isOpenInventory);
			//}

			//public void SetInventory(bool aInventory)
			//{
			//	isOpenInventory = aInventory;
			//	if (isOpenInventory == true)
			//	{
			//		ActivateInvent();
			//		test.SetActive(true);
			//	}
			//	else
			//	{
			//		DeActivateInvent();
			//		test.SetActive(false);
			//	}
			//}
	}
}