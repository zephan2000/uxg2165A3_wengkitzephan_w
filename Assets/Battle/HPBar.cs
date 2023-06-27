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
	}
}

