using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class save
{
	public string saveId;
	public string seshname;
    public string saveStatus;
    public string actorName;
    public string actorType;
    public string levelId;
	public int currenthp;
	public int maxhp;
    public int physicaldmg;
    public int magicdmg;
    public int vitality;
    public int power;
    public int intelligence;
    public int attspeed;
    public int vitality_added;
    public int power_added;
    public int intelligence_added;
    public int attspeed_added;
    public int attributePoint;
	public int exp;
    public int gold;
    public string weapon;
    public string helmet;
    public string armour;
    public string inventory;
    public string displaySpritePath;
	public string startedQuest;
	public string completedQuest;
	public int totalDamageDealt;
	public int timeInBattle;
    public int timeInQuest;
    public string battles;

	public save(string saveId,
                string seshname,
                string saveStatus,
                string actorName,
                string actorType,
                string levelId,
                int currenthp,
                int maxhp,
                int physicaldmg,
                int magicdmg,
                int vitality,
                int power, 
                int intelligence,
                int attspeed,
                int vitality_added,
                int power_added,
                int intelligence_added,
                int attspeed_added,
                int attributePoint,
                int exp,
                int gold,
                string weapon,
                string helmet,
                string armour,
                string inventory,
                string displaySpritePath,
                string startedQuest,
                string completedQuest,
                int totalDamageDealt,
                int timeInBattle,
                int timeInQuest,
                string battles)
    {
        this.saveId = saveId;
        this.seshname = seshname;
        this.saveStatus = saveStatus;
        this.actorName = actorName;
        this.actorType = actorType;
        this.levelId = levelId;
        this.currenthp = currenthp;
        this.maxhp = maxhp;
        this.physicaldmg = physicaldmg;
        this.magicdmg = magicdmg;
        this.vitality = vitality;
        this.power = power;
        this.intelligence = intelligence;
        this.attspeed = attspeed;
        this.vitality_added = vitality_added;
        this.power_added = power_added;
        this.intelligence_added = intelligence_added;
        this.attspeed_added = attspeed_added;
        this.attributePoint = attributePoint;
        this.exp = exp;
        this.gold = gold;
        this.weapon = weapon;
        this.helmet = helmet;
        this.armour = armour;
        this.inventory = inventory;
        this.displaySpritePath = displaySpritePath;
        this.startedQuest = startedQuest;
        this.completedQuest = completedQuest;
        this.totalDamageDealt = totalDamageDealt;
        this.timeInBattle = timeInBattle;
        this.timeInQuest = timeInQuest;
        this.battles = battles;
    }

	public int GetSaveNumber()
	{
		// Assuming the format of the saveId is "save_0000"
		if (saveId.Length < 9 || !saveId.StartsWith("save_"))
		{
			throw new ArgumentException("Invalid saveId format");
		}

		if (!int.TryParse(saveId.Substring(5), out int saveNumber))
		{
			throw new ArgumentException("Invalid saveId format");
		}

		return saveNumber;
	}

}
