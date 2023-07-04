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
    public int speed { get; }
    public int exp { get; }
    public int gold { get; }
    public string displaySpritePath { get; }

    public actor(string actorType,string displayName, int maxhp, int defense, int physicaldmg, int magicdmg,
        int vitality,int power,int intelligence, int speed,int exp,int gold,
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
        this.speed = speed;
        this.exp = exp;
        this.gold = gold;
        this.displaySpritePath = displaySpritePath;
    }
}
