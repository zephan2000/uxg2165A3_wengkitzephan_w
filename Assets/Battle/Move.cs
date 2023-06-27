using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
	public MoveBase moveBase {get; set;}
	public int numberofuses { get; set; }

	public Move(MoveBase Base)
	{
		this.moveBase = Base;
		numberofuses = Base.Getnumberofuses(); 
	}
}
