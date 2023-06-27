using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public MoveBase moveBase {get; set;}
	public int numberofuses { get; set; }

	public Move(MoveBase skillBase)
	{
		this.moveBase = skillBase;
		numberofuses = skillBase.Getnumberofuses(); 
	}
}
