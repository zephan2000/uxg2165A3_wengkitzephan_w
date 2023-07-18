using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
	public string questId;
	public string questType;
	public string questName;
	public int questReq;
	public string actorTypeToSlay;
	public int expReward;
	public int goldReward;
	public string questdialogIdTrigger;
	public Quest(string questId, string questType, string questName, int questReq, string actorTypeToSlay, int expReward, int goldReward, string questdialogIdTrigger)
	{
		this.questId = questId;
		this.questType = questType;
		this.questName = questName;
		this.questReq = questReq; 
		this.actorTypeToSlay = actorTypeToSlay;
		this.expReward = expReward;
		this.goldReward = goldReward;
		this.questdialogIdTrigger = questdialogIdTrigger;
	}
}
