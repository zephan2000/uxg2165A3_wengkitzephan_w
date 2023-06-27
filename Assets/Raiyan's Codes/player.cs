using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player
{
    public string name { get; }
    public int maxhp { get; }
    public int atkdmg { get; }

    public player(string name, int maxhp, int atkdmg)
    {
        this.name = name;
        this.maxhp = maxhp;
        this.atkdmg = atkdmg;
    }
}
