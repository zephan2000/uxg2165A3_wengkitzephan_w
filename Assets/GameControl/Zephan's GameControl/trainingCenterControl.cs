using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pattayaA3
{
    public class trainingCenterControl: MonoBehaviour
    {
        //Item text
        [SerializeField] Text enemy1;
		[SerializeField] Image enemy1_image;
		[SerializeField] Text enemy2;
		[SerializeField] Image enemy2_image;


		//Training NPC
		[SerializeField] Text trainingNPCname;
        [SerializeField] GameObject trainingNPCSprite;
        [SerializeField] Text trainingNPCDialog;


        //Skills
        List<PokemonBase> trainingEnemyList = new List<PokemonBase>();

        public void SetTrainingCenterText()
        {
            session currentsession = Game.GetSession();

            enemy1.text = Game.GetActorByName("enemyBat").displayName;
            enemy2.text = Game.GetActorByName("enemyGhost").displayName;
            
		}

        void enemy1Setup()
        {
			enemy2.text = Game.Getactorbytype("enemyBat").displayName;
			string enemy1_spritePath = Game.GetActorByName("enemyBat").displaySpritePath;
            UpdateSprite(enemy1_spritePath, enemy1_image);
		}
		void enemy2Setup()
		{
			enemy2.text = Game.Getactorbytype("enemyGhost").displayName;
			string enemy2_spritePath = Game.GetActorByName("enemyBat").displaySpritePath;
			UpdateSprite(enemy2_spritePath, enemy2_image);
		}


		public void UpdateSprite(string path, Image enemyImage)
        {
            AssetManager.LoadSprite(path, (Sprite s) =>
            {
                Debug.Log(path);
                enemyImage.sprite = s;
            });
        }

        public void LoadEnemy1(string enemyType)
        {
            Game.SetChosenEnemyName(Game.Getactorbytype(enemyType).displayName);
            Game.SetChosenEnemyType(enemyType);
        }

        public void LoadEnemy2(string enemyType)
        {
			Game.SetChosenEnemyName(Game.Getactorbytype(enemyType).displayName);
			Game.SetChosenEnemyType(enemyType);
		}
       
    }
}
