using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
	public MoveBase moveBase {get; set;}
	public int UsesLeft { get; set; }

	public Move(MoveBase Base)
	{
		this.moveBase = Base;
		UsesLeft = Base.GetMaxUses(); 
	}
}
