using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pattayaA3
{
	public class HpBar : MonoBehaviour
	{
		public GameObject health;
		private void Start()
		{
			health.transform.localScale = new Vector3(0.5f, 1f);
		}
		public void SetHPData(float hpNormalized)
		{
			health.transform.localScale = new Vector3(hpNormalized, 1f);
			
		}
		public IEnumerator SetHPSmooth (float newHp)
		{
			float curHp = health.transform.localScale.x;
			float HpDecrease = curHp - newHp;
			if(HpDecrease >= 0)
			{
				while (curHp - newHp >= 0) //mathf.epsilon is the smallest value that a float can have different from zero
				{
					curHp -= HpDecrease * Time.deltaTime;
					health.transform.localScale = new Vector3(curHp, 1f);
					Debug.Log(health.transform.localScale);
					yield return null;
				}
			}
			else
			{
				while (curHp - newHp <= 0)
				{
					curHp += HpDecrease * -1 * Time.deltaTime;
					health.transform.localScale = new Vector3(curHp, 1f);
					yield return null;
				}
			}
		}
	}
}

