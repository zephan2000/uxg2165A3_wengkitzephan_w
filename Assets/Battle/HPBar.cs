using System.Collections;
using System.Collections.Generic;
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
			health.transform.localScale = new Vector3(Mathf.Clamp((float)Game.mainsessionData.currenthp / Game.mainsessionData.maxhp, 0, 1), 1f);
			
		}
		public IEnumerator SetHPSmooth (float newHp)
		{
			Debug.Log($"this is current hp with {Game.mainsessionData.currenthp}, current exp scale:{Mathf.Clamp((float)Game.mainsessionData.currenthp / Game.mainsessionData.maxhp,0,1)}");
			float curHp = health.transform.localScale.x;
			float HpDifference = curHp - newHp; //negative means that the player gained
			Debug.Log($"this is HpDifference: {HpDifference}");
			if(HpDifference >= 0)
			{
				while (curHp - newHp >= 0) //mathf.epsilon is the smallest value that a float can have different from zero
				{
					curHp += HpDifference * -1 * Time.deltaTime;
					health.transform.localScale = new Vector3(Mathf.Clamp(curHp, 0, 1), 1f);
					//Debug.Log(health.transform.localScale);
					yield return null;
				}
			}
			else
			{
				while (curHp - newHp <= 0)
				{
					curHp += HpDifference * -1 * Time.deltaTime;
					health.transform.localScale = new Vector3(Mathf.Clamp(curHp, 0, 1), 1f);
					yield return null;
				}
			}
		}
	}
}

