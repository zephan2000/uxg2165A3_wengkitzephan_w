using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pattayaA3
{
	public class ExpBar : MonoBehaviour
	{
		public GameObject exp;
		//private void Start()
		//{
		//	Debug.Log($"this is current exp numbers: {Game.mainsessionData.exp} / {Game.currentmaxEXP}");
		//	Debug.Log($"this is current exp scale: {Mathf.Clamp((float)Game.mainsessionData.exp / Game.currentmaxEXP, 0, 1)}");
		//	exp.transform.localScale = new Vector3(Mathf.Clamp((float)Game.mainsessionData.exp / Game.currentmaxEXP,0,1), 1f);
		//}
		public void SetEXPData(float expNormalized)
		{
			exp.transform.localScale = new Vector3(Mathf.Clamp((float)Game.mainsessionData.exp / Game.currentmaxEXP, 0, 1), 1f);
		}
		public IEnumerator SetEXPSmooth(float newExp)
		{
			Debug.Log($"this is from setEXPSmooth : {Mathf.Clamp((float)Game.mainsessionData.exp / Game.currentmaxEXP, 0, 1)}");
			//exp.transform.localScale = new Vector3(Mathf.Clamp((float)Game.mainsessionData.exp / Game.currentmaxEXP, 0, 1),1f);
			float curExp = exp.transform.localScale.x;
			float expDifference = curExp - newExp;
			if (expDifference >= 0)
			{
				while (curExp - newExp >= 0) //mathf.epsilon is the smallest value that a float can have different from zero
				{
					curExp += expDifference * 1 * Time.deltaTime;
					//curHp -= HpDifference * Time.deltaTime;
					exp.transform.localScale = new Vector3(Mathf.Clamp(curExp, 0, 1), 1f);
					//Debug.Log(health.transform.localScale);
					yield return null;
				}
			}
			else
			{
				while (curExp - newExp <= 0)
				{
					curExp += expDifference * 1 * Time.deltaTime;
					exp.transform.localScale = new Vector3(Mathf.Clamp(curExp,0,1), 1f);
					yield return null;
				}
			}
			//while (curExp - newExp <= 0)
			//{
			//	if (expDifference >= 1f)
			//		curExp = 1f;
			//	else
			//		curExp += expDifference * -1 * Time.deltaTime;

			//	exp.transform.localScale = new Vector3(curExp, 1f);
			//	yield return null;
			//}
		}
	}
}

