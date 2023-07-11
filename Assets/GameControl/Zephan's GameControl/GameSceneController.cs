using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace pattayaA3
{
	//Zephan
	public class GameSceneController : MonoBehaviour
	{
		public string sceneName;
		protected GameController gameController;

		public virtual void Initialize(GameController aController)
		{
			gameController = aController;
		}
	}
}