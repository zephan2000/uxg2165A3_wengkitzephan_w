using pattayaA3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Zephan
namespace pattayaA3
{
	public class StartSceneController : MonoBehaviour
	{
		public string sceneName;
		protected GameController gameController;

		public virtual void Initialize(GameController aController)
		{
			gameController = aController;
		}
	}
}