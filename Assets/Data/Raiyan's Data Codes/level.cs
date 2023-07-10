using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level
{
    public string levelId { get; }
    public string actorType { get; }
    public int expToGain { get; }
    public int maxExp { get; }
    public int maxhp { get; }
    public int defense { get; }
    public int physicaldmg { get; }
    public int magicdmg { get; }
    public int vitality { get; }
    public int power { get; }
    public int intelligence { get; }
    public int attspeed { get; }

    public level(string levelId, string actorType, int expToGain, int maxExp, int maxhp, int defense, int physicaldmg, int magicdmg, int vitality, int power, int intelligence, int attspeed)
    {
        this.levelId = levelId;
        this.actorType = actorType;
        this.expToGain = expToGain;
        this.maxExp = maxExp;
        this.maxhp = maxhp;
        this.defense = defense;
        this.physicaldmg = physicaldmg;
        this.magicdmg = magicdmg;
        this.vitality = vitality;
        this.power = power;
        this.intelligence = intelligence;
        this.attspeed = attspeed;
    }
}
