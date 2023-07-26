using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
//using UnityEditor;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace pattayaA3
{
	//Zephan
	public enum GameState { FreeRoam, Dialog, Shop, Inventory, Training, SaveMenu, SaveMenuSelection , ProgressMenu, ProgressMenuSelection}
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
		private GameObject progressButton;
		private GameObject progressMenu;
		public GameObject analyticsPrefab;
		public List<GameObject> childList;
		public List<GameObject> progressMenuList;
		public GameObject trainingCenterBackground;
		private int currentChoice = 0;
		private bool isPauseMenuOpen;
		private bool allowEscape;
		private bool allowReturn;
		private bool isProgressMenuOpen;
		private float questRunTime;
		//private int PlayTimeHour;
		//private int PlayTimeMinute;
		//private int PlayTimeSecond;
		//private int MyPlayTime;


		void Start() //Zephan
		{
			Game.ProcessSaveData();

			Game.GetSave();
			player.UpdatePlayerSprite();
			TownDialogManager.Instance.OnShowDialog += () =>
			{
				state = GameState.Dialog;
			};

			TownDialogManager.Instance.OnCloseDialog += () =>
			{
				if (state == GameState.Dialog) //for cases where you want to go to battle straight after dialog
					state = GameState.FreeRoam;
			};
			TownDialogManager.Instance.OnCloseTrainingWarning += () =>
			{
				if (state == GameState.Dialog)
				{
					state = GameState.Training;
					Debug.Log($"setting Gamestate from Dialog to {state}");
				}	
			};
			TownDialogManager.Instance.OnCloseBossWarning += () =>
			{
				if (state == GameState.Dialog)
				{
					state = GameState.FreeRoam;
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
				Debug.Log("reward collected");
				RewardCollectedHud();
				CheckForLevelUp();
			};
			//Game.ProcessSaveData(Game.demoData2);
           
			//why does the game data disappear after writing?

			//Debug.Log($"finding Id {Game.mainsessionData.levelId}, otherData: {Game.mainsessionData.actorName}, {Game.mainsessionData.actorType} ");
			Game.SetSessionDataFromLevelId(Game.mainsessionData.levelId);
			//Debug.Log($"finding maxHp by Id: {Game.mainsessionData.maxhp}");
			trainingCenterBackground.SetActive(false);
			Game.enemyPokemonLevel = 1;
			if (Game.darkWizardDefeated)
			{
				player.levelUpText.SetActive(true);
				player.levelUpText.GetComponent<Text>().text = "Game Complete";
			}
				
			if (Game.mainsessionData.startedQuest != "")
			{
				questStatus = QuestStatus.Ongoing;
				Game.questInProgress = true;
				Game.SetQuestData();
				//RecordSecondsTakenForQuest();
				StartBattleQuestHud();
				CheckQuestProgress();
			}
			if(questStatus == QuestStatus.Completed)
			{
				QuestCompleteHud();
			}
			Debug.Log("this is currentbattleruntime from start:" + Game.currentBattleRunTime);

			playerHud.SetTownData();

			//StartCoroutine(playerHud.UpdateTownData());
			Debug.Log("this is level check: " + Game.mainsessionData.exp + "current max Exp" + Game.currentmaxEXP);
			CheckForLevelUp();

			//set EXP and HP data on Start()
			//Debug.Log(Game.saveList[0].saveId); //works
		}
		private void Update()
		{
			//if (player != null && !isGameOver && Input.GetKeyDown(KeyCode.Escape))
			//{
			//	TogglePause();
			//}	
			//Debug.Log(Game.mainsessionData.attributePoint);

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
			
			if (Input.GetKeyDown(KeyCode.I) && state == GameState.FreeRoam | state == GameState.Inventory)
            {
				//Raiyan the rest is Zephan
				if (!isOpenInventory)
				{
					Debug.Log($"this is mainsessionData before inventory: {Game.mainsessionData.saveId}, {Game.mainsessionData.actorName}");
					state = GameState.Inventory;
					invent.ToggleInventory(isOpenInventory);
					Debug.Log($"this is mainsessionData after toggle: {Game.mainsessionData.saveId}, {Game.mainsessionData.actorName}");
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
				TogglePauseMenu();
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
			else if (state == GameState.ProgressMenuSelection)
			{
				HandleProgressMenuSelection();
			}

			if(Game.questInProgress)
			{
				RecordSecondsTakenForQuest();
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
				player.levelUp = true;
                player.StartLevelUp();
				playerHud.SetTownData();
				//StartCoroutine(playerHud.SetTownData());
			}
		}
		public void StartNewLevel(string aScene)
		{
			player.currentposition = player.GetCurrentPosition();
			if (Game.questInProgress) { Debug.Log("adding questRunTime" + Game.mainsessionData.timeInQuest + "this is questruntime" + Game.questRunTime); Game.mainsessionData.timeInQuest += (int)questRunTime; questRunTime = 0; }// check what is the current player position
			gameController.LoadScene(aScene);
			gameController.RemoveScene(sceneName);
			Game.SaveToJSON<save>(Game.saveList);
		}

		public PlayerScript GetPlayer()
		{
			return player;
		}

		public bool CheckIsStarted()
		{
			return isStarted;
		}

		#region PauseMenu Settings (Zephan)
		public void TogglePauseMenu()
		{

			SetPauseMenu(!isPauseMenuOpen);
		}
		public void SetPauseMenu(bool aInventory)
		{
			isPauseMenuOpen = aInventory;
			if (isPauseMenuOpen == true)
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
							if(child.gameObject.name == "Progress")
							{
								progressButton = child.gameObject;
							}
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

		public void UpdateChoiceSelection(int selectedChoice)
		{
			for (int i = 0; i < childList.Count; i++)
			{
				if (i == selectedChoice)
				{
					Debug.Log($"this is {childList[i].gameObject.name}");
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
				{
					++currentChoice;
				}
					
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
			if (state == GameState.SaveMenuSelection && Input.GetKeyDown(KeyCode.Escape))
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
			switch (childList[currentChoice].gameObject.name)
			{
				case "Resume":
					state = GameState.FreeRoam;
					if (pauseMenu != null)
					{
						GameObject.Destroy(pauseMenu);
						childList.Clear();
					}
					currentChoice = 0;
					break;

				case "Progress":
					Debug.Log("progress runnning");
					ToggleProgressMenu();
					foreach (GameObject child in childList)
					{
						if(child.gameObject.name == "Progress")
						{  }
						else
							child.SetActive(false);
					}
					currentChoice = 0;
					break;
				case "Save":
					Debug.Log("Saving this is questRunTime:" + questRunTime + "mainsessionData quest Time" + Game.mainsessionData.timeInQuest);
					if (Game.questInProgress) { Game.mainsessionData.timeInQuest += (int)questRunTime; questRunTime = 0; }
					Game.SaveToJSON<save>(Game.saveList);
					currentChoice = 0;
					break;
				case "Exit":
					gameController.GoToLevelSelect();
					currentChoice = 0;
					Debug.Log("this is questRunTime: " + questRunTime);
					if (Game.questInProgress) { Game.mainsessionData.timeInQuest +=	(int)questRunTime; questRunTime = 0; }
					Game.mainsessionData.saveStatus = "INACTIVE";
					Debug.Log($"this is mainsessionData's saveId {Game.mainsessionData.saveId} with saveStatus {Game.mainsessionData.saveStatus}");
					Game.SaveToJSON<save>(Game.saveList);
					if (pauseMenu != null)
					{
						GameObject.Destroy(pauseMenu);
						childList.Clear();
					}
					break;

			}
		}

		#endregion

		#region Training Center (Zephan)
		public void SetTrainingCenter(bool aInventory)
		{
			isOpenTrainingCenter = aInventory;
			Debug.Log($"setting training center, state is: {state}");
			if (isOpenTrainingCenter == true)
			{
				state = GameState.Training;
				Debug.Log($"attempting to interact, state is: {state}");
				trainingCenterBackground.GetComponent<trainingCenterControl>().Interact();
			}
			else
			{
				//if(Game.dialogIsOpen == true)
				//{
				//	TownDialogManager.Instance.dialogBox.SetActive(false);
				//}
				state = GameState.FreeRoam;
				trainingCenterBackground.GetComponent<trainingCenterControl>().OffTrainingCenter();

				player.isTouchingDoor = false;
			}
		}
		public void ToggleTrainingCenter()
		{

			SetTrainingCenter(!isOpenTrainingCenter);
		}
		#endregion

		#region Progress Menu (Zephan)
		public void ToggleProgressMenu()
		{
			SetProgressMenu(!isProgressMenuOpen);
		}
		public void SetProgressMenu(bool aInventory)
		{
			isProgressMenuOpen = aInventory;
			if (isProgressMenuOpen == true)
			{
				state = GameState.ProgressMenu;
				progressMenu = Instantiate(analyticsPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				progressMenu.transform.SetParent(progressButton.transform, false);
				Debug.Log($"runnning: {progressMenu.gameObject.name}");
				//pauseMenu.GetComponent<GameObject>().SetActive(false);

				foreach(Transform child in progressMenu.transform)
				{
					Debug.Log($"progress Menu children gameObject: {child.gameObject.name}");
					if (child.gameObject.name == "Battle Won/Lost/Ran")
					{
						Debug.Log($"this is the enemy battle data {enemyBatString()} + {enemyGhostString()} + {enemyWizardString()}");
						child.gameObject.GetComponent<Text>().text = enemyBatString() + "," + enemyGhostString() + "," + enemyWizardString();

					}

					else if (child.gameObject.name == "Average Damage Per Battle")
					{
						Debug.Log($"this is quest ongoing {Game.mainsessionData.startedQuest}");
						child.gameObject.GetComponent<Text>().text = "Average Damage Per Battle: " + (Game.mainsessionData.totalDamageDealt / Game.mainsessionData.battles.Split('@').Length);
					}
					else if (child.gameObject.name == "Quest Info")
					{
						child.gameObject.GetComponent<Text>().text = "Quest Ongoing: " + Game.mainsessionData.startedQuest.Split('_')[0] + " Quest Completed: " + GetCompletedQuestData();
					}
					else if (child.gameObject.name == "Average Time Spent Per Completed Quest")
					{
						child.gameObject.GetComponent<Text>().text = "Average Time Spent Per Completed Quest:" + (Game.mainsessionData.timeInQuest / Game.mainsessionData.completedQuest.Split('@').Length);
					}
					else if (child.gameObject.name == "Return")
					{
						progressMenuList.Add(child.gameObject);
						Debug.Log($"finding return gameObject {child.gameObject.name}");
					}
					else if (child.gameObject.name == "Minutes Per Battle")
					{
						child.gameObject.GetComponent<Text>().text = "Average Minutes Per Battle: " + ((Game.mainsessionData.timeInBattle/60)/ Game.mainsessionData.battles.Split('@').Length);
						Debug.Log("assign run time");
					}
					else
						return;
					
				}
				allowReturn = false;
				state = GameState.ProgressMenuSelection;
			}
			else 
			{
				state = GameState.SaveMenuSelection;
				foreach (Transform child in progressButton.transform.GetChild(0))
				{
					Destroy(child.gameObject);
				}
				Destroy(progressMenu);
				progressMenuList.Clear();
				foreach (GameObject child in childList)
				{
					child.SetActive(true);
				}
			}
		}
		void ProgressMenuSelection() //for vertical choice selector
		{
			if (state == GameState.ProgressMenuSelection && Input.GetKeyDown(KeyCode.Return))
			{
				currentChoice = 0;
				StartCoroutine(CheckForReturn());
			}
		}

		private IEnumerator CheckForReturn()
		{
			yield return new WaitForSeconds(0.2f);
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
			allowReturn = true;
		}

		void HandleProgressMenuSelection()
		{
			if (progressMenuList[currentChoice].gameObject.name == "Return")
			{
				Debug.Log($"this is currentchoice: {currentChoice}");
				progressMenuList[currentChoice].GetComponent<Text>().color = Color.blue;
				if (Input.GetKeyDown(KeyCode.Return))
					ToggleProgressMenu();
			}

		}

		#endregion
		#region Battle And Quest Analytics (Zephan)
		public void RecordSecondsTakenForQuest()
		{
			questRunTime += Time.deltaTime;
		}
		public string GetCompletedQuestData()
		{
			string[] questData = Game.mainsessionData.completedQuest.Split('@');
			string questString = " ";
			string quest1Complete = "Incomplete";
			string quest2Complete = "Incomplete";
			string quest3Complete = "Incomplete";
			string quest4Complete = "Incomplete";
			string quest5Complete = "Incomplete";
			string quest6Complete = "Incomplete";
			string quest7Complete = "Incomplete";
			string quest8Complete = "Incomplete";
			string quest9Complete = "Incomplete";
			string quest10Complete = "Incomplete";



			foreach (string quest in questData) 
			{
				if (quest == "QUEST1")
					quest1Complete = "Complete";
				else if (quest == "QUEST2")
					quest2Complete = "Complete";
				else if (quest == "QUEST3")
					quest3Complete = "Complete";
				else if (quest == "QUEST4")
					quest4Complete = "Complete";
				else if (quest == "QUEST5")
					quest5Complete = "Complete";
				else if (quest == "QUEST6")
					quest6Complete = "Complete";
				else if (quest == "QUEST7")
					quest7Complete = "Complete";
				else if (quest == "QUEST8")
					quest8Complete = "Complete";
				else if (quest == "QUEST9")
					quest9Complete = "Complete";
				else if (quest == "QUEST10")
					quest10Complete = "Complete";
			}
			questString = $"Quest 1 : {quest1Complete}, Quest 2 : {quest2Complete}, Quest 3: {quest3Complete} \n Quest 4 : {quest4Complete}, " +
				$"Quest 5 : {quest5Complete}, Quest 6: {quest6Complete}, Quest 7 : {quest7Complete}, Quest 8 : {quest8Complete}, Quest 9: {quest9Complete}, Quest 10: {quest10Complete}";

			return questString;

		}
		public string enemyBatString()
		{
			string[] enemyBatBattles = Game.BattleResultByEnemyType("enemyBat");
			string enemyBatString = " ";
			if (enemyBatBattles.Length >= 0)
			{
				int enemyBatWon = 0;
				int enemyBatLost = 0;
				int enemyBatRan = 0;
				for (int i = 0; i < enemyBatBattles.Length; i++)
				{

					string[] enemyBatResult = enemyBatBattles[i].Split('_');
					if (enemyBatResult[1] == "0")
						enemyBatRan++;
					else if (enemyBatResult[1] == "1")
						enemyBatLost++;
					else if (enemyBatResult[1] == "2")
						enemyBatWon++;
					else
						return null;
				}

				if (enemyBatString == " ")
					enemyBatString= $"Bat(Won/Lost/Ran): {enemyBatWon}/{enemyBatLost}/{enemyBatRan}";
			}
			else
			{
				enemyBatString = "Bat(Won/Lost/Ran): 0/0/0";
			}

			Debug.Log($"this is the enemyBatString {enemyBatString}");
			return enemyBatString;
		}

		public string enemyGhostString()
		{
			string[] enemyGhostBattles = Game.BattleResultByEnemyType("enemyGhost");
			string enemyGhostString = " ";
			if (enemyGhostBattles.Length >= 0)
			{
				int enemyGhostWon = 0;
				int enemyGhostLost = 0;
				int enemyGhostRan = 0;
				for (int i = 0; i < enemyGhostBattles.Length; i++)
				{

					string[] enemyGhostResult = enemyGhostBattles[i].Split('_');
					if (enemyGhostResult[1] == "0")
						enemyGhostRan++;
					else if (enemyGhostResult[1] == "1")
						enemyGhostLost++;
					else if (enemyGhostResult[1] == "2")
						enemyGhostWon++;
				}

				if (enemyGhostString == " ")
					enemyGhostString = $"Ghost(Won/Lost/Ran): {enemyGhostWon}/{enemyGhostLost}/{enemyGhostRan}";
			}
			else
			{
				enemyGhostString = "Ghost(Won/Lost/Ran): 0/0/0";
			}

			return enemyGhostString;
		}

		public string enemyWizardString()
		{
			string[] enemyWizardBattles = Game.BattleResultByEnemyType("enemyWizard");
			string enemyWizardString = " ";
			if (enemyWizardBattles.Length >= 0)
			{
				int enemyWizardWon = 0;
				int enemyWizardLost = 0;
				int enemyWizardRan = 0;
				for (int i = 0; i < enemyWizardBattles.Length; i++)
				{

					string[] enemyWizardResult = enemyWizardBattles[i].Split('_');
					if (enemyWizardResult[1] == "0")
						enemyWizardRan++;
					else if (enemyWizardResult[1] == "1")
						enemyWizardLost++;
					else if (enemyWizardResult[1] == "2")
						enemyWizardWon++;
				}

				if (enemyWizardString == " ")
					enemyWizardString = $"Dark Wizard (Won/Lost/Ran): {enemyWizardWon}/{enemyWizardLost}/{enemyWizardRan}";
			}
			else
			{
				enemyWizardString = "Dark Wizard (Won/Lost/Ran): 0/0/0";
			}

			return enemyWizardString;
		}
		#endregion

		#region Quest (Zephan)
		public void StartBattleQuestHud()
		{
			questHud.SetActive(true);
			questHud.transform.GetChild(0).GetComponent<Text>().text = $"Quest Requirement: {Game.startedQuest.questName}";
			if(Game.startedQuest.questType.Contains("BATTLE"))
			{
				questHud.transform.GetChild(1).GetComponent<Text>().text = $"Progress: {Game.battleQuestProgress}/{Game.startedQuest.questReq}";
			}
			else
			{
				questHud.transform.GetChild(1).GetComponent<Text>().text = $"Progress: {Game.questComplete}";
			}
			CheckQuestProgress();
			Game.questInProgress = true; // tracking for runtime purposes
			//RecordSecondsTakenForQuest();
			Game.SaveToJSON<save>(Game.saveList);
		}
		public void QuestCompleteHud()
		{
			questHud.SetActive(true);
			questHud.transform.GetChild(0).GetComponent<Text>().text = $"Talk to NPC for reward! {Game.startedQuest.questName} completed!";
			if (Game.startedQuest.questType.Contains("BATTLE"))
			{
				questHud.transform.GetChild(1).GetComponent<Text>().text = $"Progress: {Game.battleQuestProgress}/{Game.startedQuest.questReq}";
			}
			else
			{
				questHud.transform.GetChild(1).GetComponent<Text>().text = $"Progress: {Game.questComplete}";
			}
			//Game.mainsessionData.startedQuest = Game.startedQuest.questId + "_" + 1; // removing this because quest trigger sets this// timeInQuest tracked from battleRunTime and questRunTime in case the reference it's not passed when in battle
			Game.questRunTime = 0;
			Game.SaveToJSON<save>(Game.saveList);
		}

		public void RewardCollectedHud()
		{
			questHud.SetActive(false);
			Game.rewardCollected = true;
			Game.mainsessionData.timeInQuest += (int)questRunTime; 
			questRunTime = 0; // only used to check if player has collected reward
			Game.questInProgress = false;
			Game.damagePerBattle = 0;
			questStatus = QuestStatus.Inactive;
		}
		public void CheckQuestProgress()
		{
			Debug.Log("this is currentBattleRunTime from quest progress: " + Game.currentBattleRunTime);
			if (Game.startedQuest.questType.Contains("BATTLE"))
			{
				if (Game.battleQuestProgress >= Game.startedQuest.questReq)
				{
					Game.questComplete = true;
					questStatus = QuestStatus.Completed;
					QuestCompleteHud();
				}
			}
			else if (Game.startedQuest.questType.Contains("TIME"))
			{
				if (Game.currentBattleRunTime <= Game.startedQuest.questReq && Game.currentBattleRunTime >= 0 && Game.chosenenemyType == Game.startedQuest.actorTypeToSlay)
				{
					Debug.Log("this is currentBattleRunTime from time quest: " + Game.currentBattleRunTime );
					Game.questComplete = true;
					Game.mainsessionData.startedQuest = Game.startedQuest.questId + "_" + 1;
					questStatus = QuestStatus.Completed;
					QuestCompleteHud();
				}
				else if (Game.mainsessionData.startedQuest.Split('_')[1] == "1")
				{
					Debug.Log("this is currentBattleRunTime from time quest: " + Game.currentBattleRunTime);
					Game.questComplete = true;
					Game.mainsessionData.startedQuest = Game.startedQuest.questId + "_" + 1;
					questStatus = QuestStatus.Completed;
					QuestCompleteHud();
				}
			}
			else if (Game.startedQuest.questType.Contains("DAMAGE"))
			{
				Debug.Log("this is damage dealt:" + Game.damagePerBattle + "questReq: " + Game.startedQuest.questReq);
				if (Game.damagePerBattle >= Game.startedQuest.questReq)
				{
					Game.questComplete = true;
					questStatus = QuestStatus.Completed;
					Debug.Log("appending startedQuest");
					Game.mainsessionData.startedQuest = Game.startedQuest.questId + "_" + 1;
					QuestCompleteHud();
				}
				else if (Game.mainsessionData.startedQuest.Split('_')[1] == "1")
				{
					Game.questComplete = true;
					questStatus = QuestStatus.Completed;
					Debug.Log("appending startedQuest");
					Game.mainsessionData.startedQuest = Game.startedQuest.questId + "_" + 1;
					QuestCompleteHud();
				}
			}


			else Game.questComplete = false;
		}
		#endregion


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