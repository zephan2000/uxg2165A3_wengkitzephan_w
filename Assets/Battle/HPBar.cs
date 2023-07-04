using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pattayaA3
{
	public class HPBar : MonoBehaviour
	{
		public GameObject health;
		private void Start()
		{
			health.transform.localScale = new Vector3(0.5f, 1f);
		}
		public void SetHP(float hpNormalized)
		{
			health.transform.localScale = new Vector3(hpNormalized, 1f);
		}
		public IEnumerator SetHPSmooth (float newHp)
		{
			float curHp = health.transform.localScale.x;
			float changeAmt = curHp - newHp;
			if(changeAmt >= 0)
			{
				while (curHp - newHp > Mathf.Epsilon) //mathf.epsilon is the smallest value that a float can have different from zero
				{
					curHp -= changeAmt * Time.deltaTime;
					health.transform.localScale = new Vector3(curHp, 1f);
					yield return null;
				}
			}
			else
			{
				while (curHp - newHp < Mathf.Epsilon)
				{
					curHp += changeAmt * -1 * Time.deltaTime;
					health.transform.localScale = new Vector3(curHp, 1f);
					yield return null;
				}
			}

			
		}
	}
}

