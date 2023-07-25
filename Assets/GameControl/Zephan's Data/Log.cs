using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zephan
[System.Serializable]
public class Log //not needed
{

	public string logId;
	public int vitality_added;
	public int power_added;
	public int intelligence_added;
	public int avgDamageDealtPerBattle;
	public int avgTimeInBattle;
	public int totalTimeInBattle;
	public int avgTimeInQuest;
	public int totalTimeInQuest;
	public int totalBattles;
	public string battleInfo;


	public Log(string logId, int vitality_added,int power_added, int intelligence_added, int avgDamageDealtPerBattle, int avgTimeInBattle, int totalTimeInBattle, int avgTimeInQuest, int totalTimeInQuest, int totalBattles, string BattleInfo)
	{
		this.logId = logId;
		this.vitality_added = vitality_added;
		this.power_added = power_added;
		this.intelligence_added = intelligence_added;
		this.avgDamageDealtPerBattle = avgDamageDealtPerBattle;	
		this.avgTimeInBattle = avgTimeInBattle;
		this.totalTimeInBattle = totalTimeInBattle;
		this.avgTimeInQuest = avgTimeInQuest;
		this.totalTimeInQuest = totalTimeInQuest;
		this.totalBattles = totalBattles;
		this.battleInfo = BattleInfo;
		
	}
}