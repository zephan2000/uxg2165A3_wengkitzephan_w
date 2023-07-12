using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace pattayaA3
{
	//Zephan
	public class CameraControl : MonoBehaviour
	{
		public GameController gameController;
		[Header("Player Focus")]
		private Vector3 offset = new Vector3(0f, 0f, -10f);
		private float smoothTime = 0.1f;
		private Vector3 velocity = Vector3.zero;
		public Vector3 targetPos { get; set; }

		private void Update()
		{
			//Smooth camera follow
			if (gameController.getactiveSceneName() != null)
			{
				if (gameController.getactiveSceneName() == "Town")
				{
					targetPos = gameController.getPlayer().transform.position + offset; //target is player, get ref to player from gameController
					transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
				}
				else
				{

				}
			}
			
				
		}

	}
}

