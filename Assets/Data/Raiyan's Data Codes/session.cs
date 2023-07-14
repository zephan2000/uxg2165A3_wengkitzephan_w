using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class session
{
    public string seshname { get; set; }
    public string actorType { get; set; }
    public string levelId { get; set; }
    public int maxhp { get; set; }
    public int physicaldmg { get; set; }
    public int magicdmg { get; set; }
    public int vitality { get; set; }
    public int power { get; set; }
    public int intelligence { get; set; }
    public int attspeed { get; set; }
	public int attributePoint{ get; set; }
	public int exp { get; set; }
    public int gold { get; set; }
    public string weapon { get; set; }
    public string helmet { get; set; }
    public string armour { get; set; }
    public string inventory { get; set; }
    public string displaySpritePath { get; }

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
//"seshname": "john",
//            "actorType": "playerWarrior",
//            "levelId": "playerWarrior_1",
//            "maxhp": "0",
//            "physicaldmg": "0",
//            "magicdmg": "0",
//            "vitality": "0",
//            "power": "0",
//            "intelligence": "0",
//            "attspeed": "0",
//            "attributeP": "0",
//            "exp": "0",
//            "gold": "0",
//            "weapon": "item10",
//            "helmet": "item13",
//            "armour": "item14",
//            "inventory": "",
//            "displaySpritePath": ""