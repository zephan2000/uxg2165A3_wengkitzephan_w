using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class session
{
    public string seshname;
    public string actorType;
    public string levelId;
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

    public session(string seshname, string actorType, string levelId, int maxhp, int physicaldmg, int magicdmg, int vitality, int power, 
        int intelligence, int attspeed, int attributePoint, int exp, int gold, string weapon, string helmet, string armour, string inventory, string displaySpritePath)
    {
        this.seshname = seshname;
        this.actorType = actorType;
        this.levelId = levelId;
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
    }
}