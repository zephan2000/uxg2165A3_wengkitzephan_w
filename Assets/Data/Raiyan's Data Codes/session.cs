using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class session
{
    public string seshname { get; }
    public string actorType { get; }
    public string levelid { get; }
    public int maxhp { get; }
    public int defense { get; }
    public int physicaldmg { get; }
    public int magicdmg { get; }
    public int vitality { get; }
    public int power { get; }
    public int intelligence { get; }
    public int attspeed { get; }
    public int exp { get; }
    public int gold { get; }
    public string weapon { get; }
    public string helmet { get; }
    public string armour { get; }
    public string displaySpritePath { get; }

    public session(string seshname, string actorType, string levelid, int maxhp, int defense, int physicaldmg, int magicdmg, int vitality, int power, int intelligence, int attspeed, int exp, int gold, string weapon, string helmet, string armour, string displaySpritePath)
    {
        this.seshname = seshname;
        this.actorType = actorType;
        this.levelid = levelid;
        this.maxhp = maxhp;
        this.defense = defense;
        this.physicaldmg = physicaldmg;
        this.magicdmg = magicdmg;
        this.vitality = vitality;
        this.power = power;
        this.intelligence = intelligence;
        this.attspeed = attspeed;
        this.exp = exp;
        this.gold = gold;
        this.weapon = weapon;
        this.helmet = helmet;
        this.armour = armour;
        this.displaySpritePath = displaySpritePath;
    }
}
