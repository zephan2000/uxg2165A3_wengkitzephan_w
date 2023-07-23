using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

namespace pattayaA3
{
	public class HpBar : MonoBehaviour
	{
		public GameObject health;
		//private void Start()
		//{
		//	health.transform.localScale = new Vector3(Mathf.Clamp((float)(Game.mainsessionData.currenthp / Game.maxHP),0,1), 1f);
		//}
		public void SetHPData(float hpNormalized)
		{
			health.transform.localScale = new Vector3(Mathf.Clamp((float)Game.mainsessionData.currenthp / Game.maxHP, 0, 1), 1f);
			
		}
		public IEnumerator SetHPSmooth(float newHp)
		{
			//Debug.Log($"this is current hp with {Game.mainsessionData.currenthp}, maxHp: {Game.mainsessionData.maxhp}, current hp scale:{Mathf.Clamp((float)Game.mainsessionData.currenthp / Game.maxHP, 0, 1)}");
			//health.transform.localScale = new Vector3(Mathf.Clamp((float)Game.mainsessionData.currenthp / Game.maxHP, 0, 1), 1f);
			float curHp = health.transform.localScale.x;
			float HpDifference = curHp - newHp; //negative means that the player gained
			//Debug.Log($"this is HpDifference: {HpDifference}");
			if (HpDifference >= 0)
			{
				while (curHp - newHp >= 0) //// for increase of Hp, e.g. 0.5 - 1 = -0.5
				{
					curHp += HpDifference * -1 * Time.deltaTime;
					health.transform.localScale = new Vector3(Mathf.Clamp(curHp, 0, 1), 1f);
					//Debug.Log(health.transform.localScale);
					yield return null;
				}
			}
			else
			{
				while (curHp - newHp <= 0) // for decrease of Hp, e.g. 1 - 0.5 = 0.5
				{
					curHp += HpDifference * -1 * Time.deltaTime;
					health.transform.localScale = new Vector3(Mathf.Clamp(curHp, 0, 1), 1f);
					yield return null;
				}
			}
		}
		//public IEnumerator SetHPSmooth(float newHp)
		//{
		//	float curHp = health.transform.localScale.x; // Get the current scale of the health object
		//	float maxHp = Game.mainsessionData.maxhp;

		//	// Calculate the target scale between 0 and 1
		//	float targetScale = Mathf.Clamp(newHp / maxHp, 0f, 1f);

		//	float speed = 0.1f; // Animation speed

		//	while (!Mathf.Approximately(curHp, targetScale))
		//	{
		//		// Move the current scale towards the target scale
		//		curHp = Mathf.MoveTowards(curHp, targetScale, Time.deltaTime * speed);

		//		// Set the new scale for the health object
		//		health.transform.localScale = new Vector3(curHp, 1f);

		//		// Check if the scale is moving towards the target scale
		//		if ((curHp < targetScale && newHp > Game.mainsessionData.currenthp) || (curHp > targetScale && newHp < Game.mainsessionData.currenthp))
		//		{
		//			break;
		//		}

		//		yield return null;
		//	}
		//}
	}
}

