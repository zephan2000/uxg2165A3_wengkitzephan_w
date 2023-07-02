using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actor
{
    public string actorId { get; }
    public string actorType { get; }
    public string displayName { get; }
    public int vitality { get; }
    public int power { get; }
    public int expertise { get; }
    public int exp { get; }
    public int gold { get; }
    public string displaySpritePath { get; }

    public actor(string actorId,string actorType,string displayName,
        int vitality,int power,int expertise,int exp,int gold,
        string displaySpritePath)
    {
        this.actorId = actorId;
        this.actorType = actorType;
        this.displayName = displayName;
        this.vitality = vitality;
        this.power = power;
        this.expertise = expertise;
        this.exp = exp;
        this.gold = gold;
        this.displaySpritePath = displaySpritePath;
    }
}
