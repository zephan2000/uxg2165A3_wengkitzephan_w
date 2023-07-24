using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level
{
    public string levelId { get; }
    public string actorType { get; }
    public int expToGain { get; }
    public int maxExp { get; }
	public int expGain { get; }
	public int goldGain { get; }
	public int basehp { get; }
    public int physicaldmg { get; }
    public int magicdmg { get; }
    public int vitality { get; }
    public int power { get; }
    public int intelligence { get; }
    public int attspeed { get; }

    public level(string levelId, string actorType, int expToGain, int maxExp, int expGain, int goldGain, int basehp, int physicaldmg, int magicdmg, int vitality, int power, int intelligence, int attspeed)
    {
        this.levelId = levelId;
        this.actorType = actorType;
        this.expToGain = expToGain;
        this.maxExp = maxExp;
        this.expGain = expGain;
        this.goldGain = goldGain;
        this.basehp = basehp;
        this.physicaldmg = physicaldmg;
        this.magicdmg = magicdmg;
        this.vitality = vitality;
        this.power = power;
        this.intelligence = intelligence;
        this.attspeed = attspeed;
    }
}
