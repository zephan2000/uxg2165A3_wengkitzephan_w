using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pattayaA3
{
	public class ExpBar : MonoBehaviour
	{
		public GameObject exp;
		private void Start()
		{
			exp.transform.localScale = new Vector3(0.5f, 1f);
		}
		public void SetEXPData(float expNormalized)
		{
			exp.transform.localScale = new Vector3(expNormalized, 1f);
		}
		public IEnumerator SetEXPSmooth(float newExp)
		{
			float curExp = exp.transform.localScale.x;
			float HpDecrease = curExp - newExp;
			while (curExp - newExp <= 0)
			{
				curExp += HpDecrease * -1 * Time.deltaTime;
				exp.transform.localScale = new Vector3(curExp, 1f);
				yield return null;
			}
		}
	}
}

