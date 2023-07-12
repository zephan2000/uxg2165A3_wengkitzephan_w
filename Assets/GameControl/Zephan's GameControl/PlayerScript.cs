using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
		public TrainingCenterControl trainingCenter;
		public GameObject trainingCenterBackground;
		public Vector3 currentposition;
		public Collider2D triggerCol;
		private GameObject gameObj;
		Rigidbody2D rb;
		public ContactFilter2D movementFilter;
		List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
		public float collisionOffset = 0.05f;
		public bool isTouchingDoor;
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
			if(moveDir != Vector2.zero && !inventoryOpen)
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

					else
						GetGameObject().GetComponent<NPC>()?.Interact();
				}
			}
            else if (Input.GetKeyDown(KeyCode.K))
            {
                if (inventoryOpen)
                {
                    inventoryOpen = !inventoryOpen;
                }
				else
				{
                    inventoryOpen = !inventoryOpen;
                }
            }

        }
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
			Debug.Log("Running");
			//Debug.Log(Game.Getactorbytype("Player").displaySpritePath);
            playerImage = Game.Getactorbytype("playerWizard").displaySpritePath ;
			Debug.Log(playerImage);

			Sprite tileSprite = Resources.Load(playerImage) as Sprite;

			AssetManager.LoadSprite(playerImage, (Sprite s) =>
			{
				this.GetComponent<SpriteRenderer>().sprite = s;
			});
        }
		
    }
}
