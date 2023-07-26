using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBackup : MonoBehaviour
{
	//void Update() // old movement
	//{
	//	if (!isMoving) // allows tile by tile movement	
	//	{
	//		input.x = Input.GetAxisRaw("Horizontal");
	//		input.y = Input.GetAxisRaw("Vertical");
	//		if (input.x != 0) input.y = 0;

	//		if (input != Vector2.zero)
	//		{
	//			var targetPos = transform.position;
	//			targetPos.x += input.x;
	//			targetPos.y += input.y;
	//			if (IsWalkable(targetPos))
	//				StartCoroutine(Move(targetPos));
	//		}
	//	}
	//	if (IsInteractable() && Input.GetKeyDown(KeyCode.Z))
	//	{
	//		Debug.Log("Press");
	//		if (playerCol.IsTouchingLayers(interactableLayer))
	//		{
	//			portal.Interact();
	//		}
	//		else
	//		{
	//			//Debug.Log(GetGameObject().name);
	//			GetGameObject().GetComponent<NPC>()?.Interact();
	//		}

	//	}
	//}
	//IEnumerator Move(Vector3 targetPos)
	//{
	//	isMoving = true;
	//	while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
	//	{
	//		transform.position = Vector3.MoveTowards(transform.position, targetPos, 5.5f * Time.deltaTime);
	//		yield return null;
	//	}
	//	transform.position = targetPos;
	//	isMoving = false;
	//}
	//private bool IsWalkable(Vector3 targetPos)
	//{

	//	if (Physics2D.OverlapCircle(targetPos, 0.2f, buildingLayer | interactableLayer) != null)
	//	{
	//		//Debug.Log("Wall");
	//		return false;
	//	}
	//	else
	//		return true;
	//}

	//void FixedUpdate() 
	//{
	//	Vector2 moveDir = Vector2.zero;
	//	if (Input.GetKey(KeyCode.W)) { moveDir += Vector2.up; }
	//	if (Input.GetKey(KeyCode.S)) { moveDir += Vector2.down; }
	//	if (Input.GetKey(KeyCode.A)) { moveDir += Vector2.left; this.GetComponent<SpriteRenderer>().flipX = true; } // flipSprite when moving left
	//	if (Input.GetKey(KeyCode.D)) { moveDir += Vector2.right; this.GetComponent<SpriteRenderer>().flipX = false; }
	//	//move player position
	//	//Debug.Log(IsWalkable((Vector3)moveDir));
	//	if (IsWalkable((Vector3)moveDir))
	//	{

	//		MovePlayer(moveDir.normalized * Time.fixedDeltaTime);
	//	}


	//	if (IsInteractable() && Input.GetKeyDown(KeyCode.Z))
	//	{
	//		Debug.Log("Press");
	//		if (GetGameObject().name == "Portal")
	//		{
	//			portal.Interact();
	//		}
	//		else
	//		{
	//			//Debug.Log(GetGameObject().name);
	//			GetGameObject().GetComponent<NPC>()?.Interact();
	//		}

	//	}
	//}
	//private bool IsWalkable(Vector3 targetPos)
	//{
	//	Vector2 endPos = (Vector2)targetPos;
	//	RaycastHit2D hit = Physics2D.Linecast(playerCol.bounds.center, endPos, buildingLayer | interactableLayer);
	//	Debug.DrawLine(playerCol.bounds.center, endPos, Color.blue);
	//	if (hit.collider != null)
	//	{
	//		Debug.Log("Hit");
	//		return false;
	//	}
	//	return true;
	//}
	//private bool IsWalkable(Vector3 targetPos)
	//{
	//	Vector2 endPos = (Vector2)targetPos;
	//	RaycastHit2D hit = Physics2D.BoxCast(playerCol.bounds.center, playerCol.bounds.size, 0, Vector2.zero, 0, buildingLayer | interactableLayer);
	//	if (hit.collider != null)
	//	{
	//		Debug.Log("Hit");
	//		return false;
	//	}
	//	return true;
	//}

	//public void Interact()
	//{
	//	Debug.Log("Interacting");
	//	var facingDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	//	var interactPos = transform.position + facingDir;
	//	//Debug.DrawLine(transform.position, interactPos, Color.green,0.5f);

	//	var collision = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);
	//	if (collision != null)
	//	{
	//		collision.gameObject.GetComponent<Interactable>()?.Interact();
	//	}
	//}
	//public void MovePlayer(Vector2 moveDir) // old movement codes
	//{
	//	//set player direction is done in GameController
	//	//move player position
	//	this.transform.position += (Vector3)moveDir * speed;


	//}
}
