using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skills
{
    public string skillid { get; }
    public string actorType { get; }
    public string category { get; }
    public string target { get; }
    public string skillname { get; }
    public int dmg { get; }
    public int hpgain { get; }
    public int accuracy { get; }
    public string priority { get; }
    public int maxuses { get; }

    public skills(string skillid, string actorType, string category, string target, string skillname, int dmg, int hpgain, int accuracy, string priority, int maxuses)
    {
        this.skillid = skillid;
        this.actorType = actorType;
        this.category = category;
        this.target = target;
        this.skillname = skillname;
        this.dmg = dmg;
        this.hpgain = hpgain;
        this.accuracy = accuracy;
        this.priority = priority;
        this.maxuses = maxuses;
    }
}
