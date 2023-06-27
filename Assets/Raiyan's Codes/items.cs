using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class items
{
    public string itemId { get; }
    public string displayName { get; }
    public string cLass { get; }
    public int vitalityBuff { get; }
    public int powerBuff { get; }
    public int expertiseBuff { get; }
    public int cost { get; }

    public items(string itemId, string displayName, string cLass, int vitalityBuff, int powerBuff, int expertiseBuff, int cost)
    {
        this.itemId = itemId;
        this.displayName = displayName;
        this.cLass = cLass;
        this.vitalityBuff = vitalityBuff;
        this.powerBuff = powerBuff;
        this.expertiseBuff = expertiseBuff;
        this.cost = cost;

    }
}
