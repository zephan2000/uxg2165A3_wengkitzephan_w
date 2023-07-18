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
	public string completedAchievement;

	public save(string saveId, string seshname, string saveStatus, string actorName,string actorType, string levelId, int currenthp, int maxhp, int physicaldmg, int magicdmg, int vitality, int power, 
        int intelligence, int attspeed, int attributePoint, int exp, int gold, string weapon, string helmet, string armour, string inventory, string displaySpritePath, string startedQuest, string completedQuest, string completedAchievement)
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
        this.completedAchievement = completedAchievement;
    }

}
