using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actor
{
    //public string actorId { get; }
    public string actorType;
    public string displayName;
    public string skillslist;
    public string displaySpritePath { get; }

    public actor(string actorType,string displayName, string skillslist,
        string displaySpritePath)
    {
        //this.actorId = actorId;
        this.actorType = actorType;
        this.displayName = displayName;
        this.skillslist = skillslist;
        this.displaySpritePath = displaySpritePath;
    }
}