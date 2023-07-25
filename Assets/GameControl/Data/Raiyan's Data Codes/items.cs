using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class items
{
    public string itemId { get; }
    public string itemType { get; }
    public string actorType { get; }
    public string displayName { get; }
    public int vitalityBuff { get; }
    public int physicaldmgBuff { get; }
    public int magicdmgBuff { get; }
    public int cost { get; }
    public string displaySpritePath { get; }

    public items(string itemId, string itemType, string actorType,
        string displayName, int vitalityBuff, int physicaldmgBuff,
        int magicdmgBuff, int cost, string displaySpritePath)
    {
        this.itemId = itemId;
        this.itemType = itemType;
        this.actorType = actorType;
        this.displayName = displayName;
        this.vitalityBuff = vitalityBuff;
        this.physicaldmgBuff = physicaldmgBuff;
        this.magicdmgBuff = magicdmgBuff;
        this.cost = cost;
        this.displaySpritePath = displaySpritePath;
    }
}
