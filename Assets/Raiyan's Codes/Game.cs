using pattayaA3;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Game
{
    private static List<items> itemlist;
    private static List<player> playerlist;
    private static List<EnemyBaseData> enemylist;
    private static List<actor> actorList;
    private static player mainPlayer;
    private static List<skills> skillslist;
    private static List<session> sessionlist;

    public static player GetPlayer()
    {
        return mainPlayer;
    }

    public static void SetPlayer(player player)
    {
        mainPlayer = player;
    }

    public static List<items> GetItemList()
    {
        return itemlist;
    }

    public static List<player> GetPlayerList()
    {
        return playerlist;
    }

    public static List<EnemyBaseData> GetEnemyList()
    {
        return enemylist;
    }

    public static List<actor> GetActorList()
    {
        return actorList;
    }


    public static actor Getactorbytype(string type)
    {
        foreach (actor aactor in actorList)
        {
            if (aactor.actorType == type) return aactor;
        }
        return null;
    }
    public static actor GetActorByName(string name)
    {
        foreach (actor aactor in actorList)
        {
            if (aactor.displayName == name) return aactor;
        }
        return null;
    }

    public static items Getitemsbyid(string id)
    {
        foreach(items item in itemlist)
        {
            if (item.itemId == id) return item;
        }
        return null;
    }
    public static items GetitemsbyName(string name)
    {
        foreach (items item in itemlist)
        {
            if (item.displayName == name) return item;
        }
        return null;
    }
    public static List<items> GetitemsbyitemType(string type)
    {
        List<items> aitems = new List<items>();
        foreach (items aitem in itemlist)
        {
            if (aitem.itemType == type)
            {
                aitems.Add(aitem);
            }
        }
        return aitems;
    }

    public static skills GetSkillById(string id)
    {
        foreach (skills askill in skillslist)
        {
            if (askill.skillid == id) return askill;
        }
        return null;
    }
    public static string GetSkillByType(string type)
    {
        return Game.Getactorbytype(type).skillslist;
    }
	public static skills GetskillbyName(string name)
	{
		foreach (skills askill in skillslist)
		{
			if (askill.skillname == name) return askill;
		}
		return null;
	}
	public static List<skills> GetListOfSkillsByType(string type)
    {
        List<skills> nskills = new List<skills>();
        foreach (skills askill in skillslist)
        {
            if (askill.actorType == type)
            {
                nskills.Add(askill);
            }
        }
        return nskills;
    }
    public static List<skills> GetskillsbyCategory(string category)
    {
        List<skills> nskills = new List<skills>();
        foreach (skills askill in skillslist)
        {
            if (askill.category == category)
            {
                nskills.Add(askill);
            }
        }
        return nskills;
    }
    public static void ListdownSkills(string type)
    {
        Debug.Log("===Start===");
        Debug.Log(Game.Getactorbytype(type).displayName);
        Debug.Log(Game.Getactorbytype(type).actorType);
        Debug.Log(Game.Getactorbytype(type).skillslist);
        Debug.Log("===End===");
    }

    public static void SetItemList(List<items> alist)
    {
        itemlist = alist;
    }
    public static void SetPlayerList(List<player> aplayer)
    {
        playerlist = aplayer;
    }
    public static void SetEnemyList(List<EnemyBaseData> aenemy)
    {
        enemylist = aenemy;
    }
    public static void SetActorList(List<actor> aactor)
    {
        actorList = aactor;
    }
    public static void SetSkillList(List<skills> askill)
    {
        skillslist = askill;
    }
    public static void SetSessionList(List<session> asession)
    {
        sessionlist = asession;
    }

    public static void AssignAllSkillListToActor()
    {
        foreach (actor aactor in actorList)
        {
            //Debug.Log("yes");
            List<skills> skillList = GetListOfSkillsByType(aactor.actorType);
            string stringSkillList = "";
            foreach (skills askill in skillList)
            {
                //Debug.Log("yes");
                stringSkillList += askill.skillid + "%1,";
            }
            //Debug.Log(stringSkillList);
            //stringSkillList.Remove(stringSkillList.Length - 1, 1);
            //aactor.skillslist = stringSkillList;
        }
    }
}
