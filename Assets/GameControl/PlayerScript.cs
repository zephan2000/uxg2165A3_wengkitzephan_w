using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pattayaA3
{
	//Zephan
	public class PlayerScript : MonoBehaviour
	{
		private LevelController levelController;
		public GameController gameController;
		public Transform initialPosition;
		public float speed = 6f;
		public int initHealth = 5;
		public Transform healthBar;
		private int currHealth;
		public SpriteRenderer sprite;
		//public LayerMask buildingLayer; // only use if implementing physics2d.OverlapCircle
		public Collider2D playerCol;
		public LayerMask portalLayer;
		//protected ContactFilter2D buildingFilter;
		//protected ContactFilter2D portalFilter;
		private string newLevel;
		public void MovePlayer(Vector2 moveDir)
		{
			//set player direction is done in GameController
			//move player position
			if(IsPortal())
			{
				levelController.StartNewLevel("Battle View"); // will change this to start battle sequence
			}
			//if (IsWalkable((Vector3)moveDir))
				this.transform.position += (Vector3)moveDir * speed;


		}

		//private bool IsWalkable(Vector3 targetPos)
		//{

		//	if (Physics2D.OverlapCircle(targetPos, 0.2f, buildingLayer) != null)
		//	{
		//		Debug.Log("Wall");
		//		return false;
		//	}


		//	else
		//		return true;
		//}

		private bool IsPortal()
		{
			if (playerCol.IsTouchingLayers(portalLayer) == true)
			{
				Debug.Log("Portal");
				return true;
			}
			else
				return false;
		}

		public void Initialize(LevelController aController)
		{
			levelController = aController;

			//set to initial position
			this.transform.position = initialPosition.transform.position;
			//buildingFilter.useTriggers = false;
			//buildingFilter.SetLayerMask(buildingLayer); // only use if implementing physics2d.OverlapCircle
			//buildingFilter.useLayerMask = true;
			//portalFilter.useTriggers = false;
			//portalFilter.SetLayerMask(portalLayer);
			//portalFilter.useLayerMask = true;

			//	currHealth = initHealth;

			//	UpdateHealthDisplay();
			//}

			//public void OnTriggerEnter2D(Collider2D collision)
			//{
			//	if (collision.gameObject.GetComponent<DamagerScript>() != null)
			//	{
			//		TakeDamage(1);
			//		collision.gameObject.GetComponent<DamagerScript>().DoOnHit();
			//	}

			//	//detect collectible
			//	if (collision.gameObject.GetComponent<CollectibleScript>() != null && !levelController.CheckGameOver() && levelController.CheckIsStarted())
			//	{
			//		levelController.AddCollectedCount(1);
			//		collision.gameObject.GetComponent<CollectibleScript>().DoCollect();
			//	}

			//	//detect end point
			//	if (collision.gameObject.GetComponent<EndPointScript>() != null && !levelController.CheckGameOver())
			//		levelController.SetGameOver(true, true);
		}

		private void UpdateHealthDisplay()
		{
			float healthPercent = (float)currHealth / (float)initHealth;
			healthBar.localScale = new Vector3(healthPercent, healthPercent, 1f);
		}

		private void TakeDamage(int dmg)
		{
			currHealth -= dmg;
			UpdateHealthDisplay();

			//if (currHealth <= 0)
			//{
			//	levelController.SetGameOver(true, false);
			//}
		}
	}
}