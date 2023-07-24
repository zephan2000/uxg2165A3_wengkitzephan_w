                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Zephan
namespace pattayaA3
{
    public class trainingCenterControl: MonoBehaviour, Interactable
    {
        //Item text
        [SerializeField] Text enemy1;
		[SerializeField] Image enemy1_image;
		[SerializeField] Text enemy2;
		[SerializeField] Image enemy2_image;


		//Training NPC
		[SerializeField] Text trainingNPCname;
        [SerializeField] Image trainingNPCSprite;
        [SerializeField] Text trainingNPCDialog;

		[SerializeField] LevelController levelController;
		[SerializeField] GameObject trainingCenterBackground;
		[SerializeField] InputField levelInput;
		private Coroutine currentCoroutine;
		bool allowNext = true;

		//Skills
		List<PokemonBase> trainingEnemyList = new List<PokemonBase>();

		public void Interact()
		{
			trainingCenterBackground.SetActive(true);
			//if(!Game.dialogIsOpen)
			SetupTrainingCenter();
			Game.runonce = 0;
			//levelInput = GameObject.Find("InputField").GetComponent<InputField>();
			//levelInput.onValueChanged.AddListener( delegate { SetPokemonLevel(); } );

		}
		void Update()
		{
			if (levelInput.text != "")
			{
				SetPokemonLevel(levelInput.text);
				Debug.Log("deactivating input ");
			}
		}
		public void SetupTrainingCenter()
        {
			enemy1Setup("enemyBat");
			enemy2Setup("enemyGhost");
			trainingCenterNPCSetup("npcTraining");
		}
		void trainingCenterNPCSetup(string trainingNPC)
		{
			//Debug.Log("setting up training center");
			actor trainingNPCactor = Game.GetActorByActorType(trainingNPC);
			trainingNPCname.text = trainingNPCactor.displayName;
			string trainingNPC_spritePath = trainingNPCactor.displaySpritePath;
			StartCoroutine(TypeDialog(Game.GetDialogByDialogId("TRAINING0001").dialogueText));
			UpdateSprite(trainingNPC_spritePath, trainingNPCSprite);

		}
		public IEnumerator TypeDialog(string dialog) // animating dialog to reveal letter by letter
		{
			trainingNPCDialog.text = "";
			foreach (var letter in dialog.ToCharArray())
			{
				trainingNPCDialog.text += letter;
				yield return new WaitForSeconds(1f / 60);
			}
		}
		public void SetPokemonLevel(string inputText) // input being read now, but how do i get out of level input
		{
			Game.SetEnemyPokemonLevel(inputText);
		}
		void enemy1Setup(string enemy1type)
        {
			actor enemy1actor= Game.GetActorByActorType(enemy1type);
			enemy1.text = enemy1actor.displayName;
			string enemy1_spritePath = enemy1actor.displaySpritePath;
            UpdateSprite(enemy1_spritePath, enemy1_image);
		}
		void enemy2Setup(string enemy2type)
		{
			actor enemy2actor = Game.GetActorByActorType(enemy2type);
			enemy2.text = enemy2actor.displayName;
			string enemy2_spritePath = enemy2actor.displaySpritePath;
			UpdateSprite(enemy2_spritePath, enemy2_image);
		}
		public void OffTrainingCenter()
		{
			trainingCenterBackground.SetActive(false);
		}

		public void UpdateSprite(string path, Image actorImage)
        {
            AssetManager.LoadSprite(path, (Sprite s) =>
            {
                //Debug.Log(path);
                actorImage.sprite = s;
            });
        }

        public void LoadEnemy1(string enemyType)
        {
            Game.chosenenemyName = Game.GetActorByActorType(enemyType).displayName;
            Game.chosenenemyType = enemyType;
			Debug.Log("this is loading enemy current hp "+ Game.mainsessionData.currenthp);
			LoadBattle();
		}
		
        public void LoadEnemy2(string enemyType)
        {
			Game.chosenenemyName = Game.GetActorByActorType(enemyType).displayName;
			Game.chosenenemyType = enemyType;
			LoadBattle();
		}
       
		public void LoadBattle()
		{
			//Debug.Log("running Load Battle");
			if (Game.mainsessionData.currenthp != Game.mainsessionData.maxhp)
			{
				if (!allowNext)
				{
					StopAllCoroutines();
					return;
				}
				if (allowNext) allowNext = false;
				levelController.state = GameState.Dialog;
				Debug.Log($"showing warning Dialog, state is: {levelController.state}");
				allowNext = true;
				Game.isTrainingWarning = true;
				currentCoroutine = StartCoroutine(TownDialogManager.Instance.ShowDialog("WARNING"));
			}
			else
			{
				levelController.StartNewLevel("Battle View");
			}
		}
    }
}
