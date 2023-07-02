using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player
{
    //Raiyan's Code
    public string currentActor;
    public string id;

    public int playerVit;
    public int playerPow;
    public int playerExp;
    public int playerXp;
    public int playerGold;
    public string playerImage;

    private bool isDirty;

    public player(string id, string currentActor)
    {
        this.id = id;
        this.currentActor = currentActor;

        isDirty = true;
    }
    public string GetId()
    {
        return id;
    }

    public string GetCurrentActor()
    {
        return currentActor;
    }

    public void SetCurrentActor(string actor)
    {
        currentActor = actor;

        isDirty = true;
    }

    public bool UpdateStats()
    {
        if (!isDirty) return false;

        actor playerActor = Game.Getactorbyid(currentActor);


        playerVit = playerActor.vitality;
        playerPow = playerActor.power;
        playerExp = playerActor.expertise;
        playerXp = playerActor.exp;
        playerGold = playerActor.gold;
        playerImage = playerActor.displaySpritePath;

        isDirty = false;
        return true;
    }

    public int getPower()
    {
        UpdateStats();
        return playerPow;
    }
    public int getVitality()
    {
        UpdateStats();
        return playerVit;
    }
    public int getExp()
    {
        UpdateStats();
        return playerExp;
    }
    public int getXp()
    {
        UpdateStats();
        return playerXp;
    }
    public int getGold()
    {
        UpdateStats();
        return playerGold;
    }
    public string getImg()
    {
        UpdateStats();
        return playerImage;
    }
}
