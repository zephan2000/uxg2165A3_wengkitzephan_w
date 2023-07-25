using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace pattayaA3
{
	//Zephan
	public class PlayerScript : MonoBehaviour
	{
		
		//Raiyan's Code
		private player player2;
		private actor playerActor;
        public string currentActor;
        public string id;

		public int playerVit;
        public int playerPow;
        public int playerExp;
        public int playerXp;
        public int playerGold;
        public string playerImage;	
		
		public bool inventoryOpen = false;
        


		private LevelController levelController;
		public GameController gameController;
		public Transform initialPosition;
		public SpriteRenderer sprite;
		public BoxCollider2D playerCol;
		public LayerMask interactableLayer;
		public LayerMask buildingLayer;
		public LayerMask portalLayer;
		public Portal portal;
		public trainingCenterControl trainingCenter;
		public GameObject trainingCenterBackground;
		public Vector3 currentposition;
		public Collider2D triggerCol;
		private GameObject gameObj;
		Rigidbody2D rb;
		public ContactFilter2D movementFilter;
		List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
		public float collisionOffset = 0.05f;
		public bool isTouchingDoor;
		public GameObject levelUpText;
		public BattleHud playerHud;
		private bool levelUp;

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
		}
		public void HandleUpdate()
		{
			Vector2 moveDir = Vector2.zero;
			if (Input.GetKey(KeyCode.W)) { moveDir += Vector2.up; }
			if (Input.GetKey(KeyCode.S)) { moveDir += Vector2.down; }
			if (Input.GetKey(KeyCode.A)) { moveDir += Vector2.left; this.GetComponent<SpriteRenderer>().flipX = true; } // flipSprite when moving left
			if (Input.GetKey(KeyCode.D)) { moveDir += Vector2.right; this.GetComponent<SpriteRenderer>().flipX = false; }
			//move player position
			//Debug.Log(IsWalkable((Vector3)moveDir));
			if(moveDir != Vector2.zero)
			{
				MovePlayer(moveDir);
			}

			if (IsInteractable() && Input.GetKeyDown(KeyCode.Z))
			{
				Debug.Log(IsInteractable());
				Debug.Log("Press");
				if (GetGameObject() != null)
				{
					if (GetGameObject().name == "Portal")
					portal.Interact();

					else if (GetGameObject().name == "Training Center Door")
						isTouchingDoor = true;

					else if (GetGameObject().name == "Shop Door")
                        isTouchingDoor = true;
                    else
						GetGameObject().GetComponent<NPC>()?.Interact();
				}
				Debug.Log($"This is isTouchinDoor {isTouchingDoor}");
			}   

			if(Input.GetKeyDown(KeyCode.F2)) 
			{
				levelUp = true;
                if (!levelUp)
                    return;

                StartLevelUp(); //need to run this after quest is done too
            }
			if(Input.GetKeyDown(KeyCode.F1)) //for bug testing
			{
				StartCoroutine(SetHpTo50());
			}
            //if (Input.GetKeyDown(KeyCode.F3)) //for bug testing
            //{
            //    StartCoroutine(SetGoldTo500());
            //}
            if (Input.GetKeyDown(KeyCode.F4)) //for bug testing
            {
                Game.mainsessionData.gold += 200;
                Game.SaveToJSON<save>(Game.saveList);
            }
			if (Input.GetKeyDown(KeyCode.F5))
            {
                Game.mainsessionData.attributePoint += 3;
                Game.SaveToJSON<save>(Game.saveList);
            }
            //if (Input.GetKeyDown(KeyCode.F6))
            //{
            //    Game.playerLevel++;
            //}
            if (Input.GetKeyDown(KeyCode.F7))
            {
                //Game.mainsessionData.attributePoint = 0;
                Game.SaveToJSON<save>(Game.saveList);
            }
            //if (Input.GetKeyDown(KeyCode.F8))
            //{
            //    Game.mainsessionData.currenthp = 50;
            //    Game.SaveToJSON<save>(Game.saveList);
            //}

        }

		public void StartLevelUp()
		{
			Debug.Log("level up function triggered");
			while(levelUp)
			{
                levelUpText.SetActive(true);

                if (Game.playerLevel + 1 > 20)
                {
                    StartCoroutine(MaxLevelReachedSettings());
                    break;
                }
                Game.playerLevel++;
                string newlevelid = Game.mainsessionData.actorType + "_" + Game.playerLevel.ToString();
                Debug.Log($"this is level id before level up: {newlevelid}");
                Game.mainsessionData.levelId = newlevelid;
                Debug.Log($"this is level id after level up: {Game.mainsessionData.levelId}");
                Game.SetSessionDataFromLevelId(newlevelid);
                Game.mainsessionData.exp = 0;
                Game.mainsessionData.currenthp = (int)Game.mainsessionData.maxhp;
                Game.mainsessionData.attributePoint += 2; //This is for attribute points per level up
                Game.mainsessionData.exp += 10;
                StartCoroutine(playerHud.UpdateTownData());
                levelUpText.SetActive(false);
       

                if (Game.mainsessionData.exp >= Game.currentmaxEXP) // if multiple level ups
                {
                    Game.mainsessionData.exp -= Game.currentmaxEXP;
                    Debug.Log("checking for level up");
					StartCoroutine(levelUpPause());
					//StartCoroutine(playerHud.UpdateTownData());
					//playerHud.UpdateTownData();
					
                }
                levelUp = false;
                Game.SaveToJSON<save>(Game.saveList);
            }
		}
		public IEnumerator MaxLevelReachedSettings()
		{
            levelUpText.GetComponent<Text>().text = "Max Level Reached!";
            yield return new WaitForSeconds(1f);
            levelUpText.SetActive(false);

        }

		public IEnumerator levelUpPause()
		{
			yield return new WaitForSeconds(0.3f);
            levelUp = true;
        }

		//public IEnumerator LevelUp() //do one for cheats and one for normal
		//{
		//	yield return new WaitForSeconds(0.3f);
		//	levelUpText.SetActive(true);
		//	if (Game.playerLevel + 1 > 20)
		//	{
		//		levelUpText.GetComponent<Text>().text = "Max Level Reached!";
		//		yield return new WaitForSeconds(1f);
		//		levelUpText.SetActive(false);
		//		yield break;
		//	}
		//	levelUpText.GetComponent<Text>().text = "Level Up!";

		//	Game.playerLevel++;
		//	//Game.playerLevel = Game.playerLevel;
		//	string newlevelid = Game.mainsessionData.actorType + "_" + Game.playerLevel.ToString();
		//	Debug.Log($"this is level id before level up: {newlevelid}");
		//	Game.mainsessionData.levelId = newlevelid;
		//	Debug.Log($"this is level id after level up: {Game.mainsessionData.levelId}");
		//	Game.SetSessionDataFromLevelId(newlevelid);
		//	Game.mainsessionData.exp = 0;
		//	Game.mainsessionData.currenthp = (int)Game.mainsessionData.maxhp;
		//	Game.mainsessionData.attributePoint += 2; //This is for attribute points per level up
		//	Game.mainsessionData.exp += 10; // have to take this out befere submission
		//									//Game.mainsessionData.gold += 200;
		//									//Game.mainsessionData.attributePoint += 5;
		//	Debug.Log(newlevelid);
		//	//Debug.Log("Teting save for level up"); //This is where the code stops
		//	yield return playerHud.UpdateTownData();
		//	//Debug.Log("Teting save for level up");
		//	yield return new WaitForSeconds(0.5f);
		//	//Debug.Log("Teting save for level up");
		//	levelUpText.SetActive(false);
		//	//Debug.Log("Teting save for level up");
		//	if (Game.mainsessionData.exp >= Game.currentmaxEXP) // if multiple level ups
		//	{
		//		Game.mainsessionData.exp -= Game.currentmaxEXP;
		//		Debug.Log("checking for level up");
		//		yield return new WaitForSeconds(0.3f);
		//		//StartCoroutine(playerHud.UpdateTownData());
		//		//playerHud.UpdateTownData();
		//		yield return LevelUp();
		//	}
		//	//Debug.Log("Teting save for level up");
		//	Game.SaveToJSON<save>(Game.saveList);
		//	//Debug.Log("Teting save for level up");
		//	// reset current exp
		//}
		public void RestoreHealth()
		{
			Game.mainsessionData.currenthp = (int)Game.mainsessionData.maxhp;
			Debug.Log($"this is currentHp from RestoreHealth: {Game.mainsessionData.currenthp} / {Game.mainsessionData.maxhp}, currentexp: {Game.mainsessionData.exp} / {Game.currentmaxEXP}");
			StartCoroutine(playerHud.UpdateTownData());
		}
		public IEnumerator SetHpTo50() // for bug testing
		{
			Game.mainsessionData.currenthp = 50;
			//StartCoroutine(playerHud.UpdateTownData());
			//playerHud.SetTownData();
			StartCoroutine (playerHud.UpdateTownData());
			yield return new WaitForSeconds(1.2f);
			//StopAllCoroutines();
		}
        //public IEnumerator SetGoldTo500() // for bug testing
        //{
        //    Game.mainsessionData.gold = 500;
        //    //StartCoroutine(playerHud.UpdateTownData());
        //    yield return new WaitForSeconds(1.2f);
        //}
        public void MovePlayer(Vector2 moveDir) // old movement codes
		{
			int count = rb.Cast(moveDir, movementFilter, castCollisions, 5.5f * Time.fixedDeltaTime + collisionOffset);

			if (count == 0)
				rb.MovePosition(rb.position + moveDir * 5.5f * Time.fixedDeltaTime);
		}
		private bool IsInteractable()
		{
			var facingDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			var interactPos = transform.position + facingDir;
			var collision = Physics2D.OverlapCircle(interactPos, 0.25f, interactableLayer);
			if (collision != null)
			{
				//Debug.Log(collision.gameObject.name);
				StartCoroutine(InteractablePing(collision));
				//Debug.Log(IsInteractable());
				return false;
			}
			else
			{
				return true;
			}
		}

		IEnumerator InteractablePing(Collider2D collision)
		{
			collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
			yield return new WaitForSeconds(0.7f);
			collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
		}

		public GameObject GetGameObject()
		{
			GameObject collider = null;
			RaycastHit2D hit = Physics2D.BoxCast(playerCol.bounds.center, new Vector2(3f, 3f), 0, Vector2.up, 0, portalLayer | interactableLayer);
			if (hit.collider != null)
			{
				//Debug.Log("Found");
				if (hit.collider.gameObject.CompareTag("NPC") || hit.collider.gameObject.CompareTag("Portal"))
				{
					//Debug.Log(hit.collider.gameObject.name);
					collider = hit.transform.gameObject;
				}
			}
			return collider;
		}
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(playerCol.bounds.center, new Vector2(3f, 3f));
			Gizmos.DrawWireCube(playerCol.bounds.center, playerCol.bounds.size);

		}
		
		public Vector3 GetCurrentPosition()
		{
			return currentposition = this.transform.position;
		}

		public void Initialize(LevelController aController)
		{
			levelController = aController;
			this.transform.position = initialPosition.transform.position;
			UpdatePlayerStats();
		}

		
		//Raiyan's Code
		
		public void UpdatePlayerStats() 
		{
			//player.UpdateStats();
			//Debug.Log("Running");
			//Debug.Log(Game.Getactorbytype("Player").displaySpritePath);
            playerImage = Game.GetActorByActorType("playerWizard").displaySpritePath ;
			Debug.Log(playerImage);

			//Sprite tileSprite = Resources.Load(playerImage) as Sprite;

			AssetManager.LoadSprite(playerImage, (Sprite s) =>
			{
				this.GetComponent<SpriteRenderer>().sprite = s;
			});
        }
		
    }
}
