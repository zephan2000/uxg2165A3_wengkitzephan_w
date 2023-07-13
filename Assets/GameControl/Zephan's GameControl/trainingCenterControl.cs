using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Zephan
namespace pattayaA3
{
    public class TrainingCenterControl: MonoBehaviour, Interactable
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

		//Skills
		List<PokemonBase> trainingEnemyList = new List<PokemonBase>();

		public void Interact()
		{
			trainingCenterBackground.SetActive(true);
			SetupTrainingCenter();
		}
		public void SetupTrainingCenter()
        {
			enemy1Setup("enemyBat");
			enemy2Setup("enemyGhost");
			trainingCenterNPCSetup("npcTraining");
		}
		void trainingCenterNPCSetup(string trainingNPC)
		{
			actor trainingNPCactor = Game.Getactorbytype(trainingNPC);
			trainingNPCname.text = trainingNPCactor.displayName;
			string trainingNPC_spritePath = trainingNPCactor.displaySpritePath;
			StartCoroutine(TypeDialog(Game.GetDialogByDialogId("TRAINING001").dialogueText));
			UpdateSprite(trainingNPC_spritePath, trainingNPCSprite);

		}
		public IEnumerator TypeDialog(string dialog) // animating dialog to reveal letter by letter
		{
			trainingNPCDialog.text = "";
			foreach (var letter in dialog.ToCharArray())
			{
				trainingNPCDialog.text += letter;
				yield return new WaitForSeconds(1f / 30);
			}
		}
		public void SetPokemonLevel(string inputText) // need to figure this out, why is no input being read
		{
			Game.SetEnemyPokemonLevel(inputText);
		}
		void enemy1Setup(string enemy1type)
        {
			actor enemy1actor= Game.Getactorbytype(enemy1type);
			enemy1.text = enemy1actor.displayName;
			string enemy1_spritePath = enemy1actor.displaySpritePath;
            UpdateSprite(enemy1_spritePath, enemy1_image);
		}
		void enemy2Setup(string enemy2type)
		{
			actor enemy2actor = Game.Getactorbytype(enemy2type);
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
                Debug.Log(path);
                actorImage.sprite = s;
            });
        }

        public void LoadEnemy1(string enemyType)
        {
            Game.chosenenemyName = Game.Getactorbytype(enemyType).displayName;
            Game.chosenenemyType = enemyType;
			levelController.StartNewLevel("Battle View");
		}

        public void LoadEnemy2(string enemyType)
        {
			Game.chosenenemyName = Game.Getactorbytype(enemyType).displayName;
			Game.chosenenemyType = enemyType;
			levelController.StartNewLevel("Battle View");
		}
       
    }
}
