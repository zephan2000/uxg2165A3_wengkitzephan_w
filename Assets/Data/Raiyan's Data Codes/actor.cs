using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actor
{
    //public string actorId { get; }
    public string actorType { get; }
    public string displayName { get; }
    public int maxhp { get; }
    public int defense { get; }
    public int physicaldmg { get; }
    public int magicdmg { get; }
    public int vitality { get; }
    public int power { get; }
    //public int expertise { get; }
    public int intelligence { get; }
    public int attSpeed { get; }
    public string skillslist { get; set; }
    public int exp { get; }
    public int gold { get; }
    public string displaySpritePath { get; }

    public actor(string actorType,string displayName, int maxhp, int defense, int physicaldmg, int magicdmg,
        int vitality,int power,int intelligence, int attspeed, string skillslist, int exp,int gold,
        string displaySpritePath)
    {
        //this.actorId = actorId;
        this.actorType = actorType;
        this.displayName = displayName;
        this.maxhp = maxhp;
        this.defense = defense;
        this.physicaldmg = physicaldmg;
        this.magicdmg = magicdmg;
        this.vitality = vitality;
        this.power = power;
        //this.expertise = expertise;
        this.intelligence = intelligence;
        this.attSpeed = attspeed;
        this.skillslist = skillslist;
        this.exp = exp;
        this.gold = gold;
        this.displaySpritePath = displaySpritePath;
    }
}
