using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace pattayaA3
{
	//Zephan
	public enum GameState { FreeRoam, Dialog, Shop, Inventory, Training}
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

        public GameObject inventory;
		public inventorybox inventorybox;
		public Invent invent;
		public bool isOpenInventory;
		private bool isOpenTrainingCenter;
		public BattleHud playerHud;

		public GameObject trainingCenterBackground;

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
					Game.runonce = 0;
				}	
			};
			Debug.Log($"finding Id {Game.mainsessionData.levelId}");
			Game.SetSessionDataFromLevelId((Game.mainsessionData.levelId));
			Debug.Log($"finding maxHp by Id: {Game.mainsessionData.maxhp}");
			trainingCenterBackground.SetActive(false);
			playerHud.SetTownData();
			if (Game.currentEXP >= Game.currentmaxEXP)
			{
				Debug.Log("checking for level up");
				StartCoroutine(player.LevelUp());
				StartCoroutine(playerHud.UpdateTownData());
			}
				
			//set EXP and HP data on Start()
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
			if (player.isTouchingDoor == true && Input.GetKeyDown(KeyCode.Z) && state != GameState.Dialog)
			{
				Debug.Log($"pressing Z {state}");

				ToggleTrainingCenter();
			}
			//Raiyan
			if (Input.GetKeyDown(KeyCode.I))
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

        //public void SetInventory(bool aInventory)
        //{
        //    isOpenInventory = aInventory;
        //    if (isOpenInventory == true)
        //    {
        //        inventory.SetActive(true);
        //    }
        //    else
        //    {
        //        inventory.SetActive(false);
        //    }
        //}
        //public void ToggleInventory()
        //{
        //    SetInventory(!isOpenInventory);
        //}
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

	}
}