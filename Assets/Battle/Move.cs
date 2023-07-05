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
		UsesLeft = Base.GetMaxUsesFromSkill(); 
	}

	//public List<Move> GetListOfMovesByType(string actorType)
	//{
	//var listOfMoves = new List<Move>();
	//var listOfLearnableMoves = GetListOfLearnableMovesByType(string actorType);
	//
	//}

	//public List<Move> GetListOfMovesByType(string actorType)
	//{
	//var listOfMoves = new List<Move>();
	//var listOfLearnableMoves = GetListOfLearnableMovesByType(string actorType);
	//
	//}
}
