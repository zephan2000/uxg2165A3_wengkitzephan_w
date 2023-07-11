using pattayaA3;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
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
    private static session mainsession;

    private static List<Dialog> dialogList;
    private static List<level> levellist;

    private static string chosenenemyName;
    private static string chosenenemyType;
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

    public static string GetChosenEnemyName()
    {
        return chosenenemyName;
    }
    public static string GetChosenEnemyType()
    {
        return chosenenemyType;
    }

    public static void SetChosenEnemyName(string enemyName)
    {
        chosenenemyName = enemyName;
    }
	public static void SetChosenEnemyType(string enemyType)
	{
		chosenenemyType= enemyType;
	}



	public static actor Getactorbytype(string type)
    {
        foreach (actor aactor in actorList)
        {
            if (aactor.actorType == type)
            {
                //Debug.Log(aactor.actorType);
				//Debug.Log(aactor.displaySpritePath);
				return aactor;
			}
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
    public static string GetSkillListByType(string type)
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

    public static string GetEquipment(string type)
    {
        
        return Game.Getactorbytype(type).skillslist;
    }

    public static session GetSession()
    {
        return mainsession;
    }

	public static List<skills> GetListOfSkillsByType(string type)
    {
        List<skills> listofskillsbyType = new List<skills>();
        int i = 0;
        foreach (skills askill in skillslist)
        {
            if (askill.actorType == type)
            {
                listofskillsbyType.Add(askill);
            }
        }
        return listofskillsbyType;
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
	//public static void ListdownSkills(string type)
	//{
	//    Debug.Log("===Start===");
	//    Debug.Log(Game.Getactorbytype(type).displayName);
	//    Debug.Log(Game.Getactorbytype(type).actorType);
	//    Debug.Log(Game.Getactorbytype(type).skillslist);
	//    Debug.Log("===End===");
	//}
	#region Zephan's Data
	public static List<Dialog> GetDialogByType(string type)
    {
        List<Dialog> ndialog = new List<Dialog>();
        foreach(Dialog adialog in dialogList)
        {
            if(adialog.dialogueType == type)
            {
                ndialog.Add(adialog);
            }
        }
        return ndialog;
    }

    public static Dialog GetDialogByDialogId(string dialogId)
    {
        foreach(Dialog adialog in dialogList)
        {
            if (adialog.dialogueId == dialogId)
                return adialog;
        }
        return null;
    }
	public static Dialog GetDialogByDialogList(string dialogId, List<Dialog> dialogList) //for optimisation, in the event where there is a ton of dialog to run through
	{
		foreach (Dialog adialog in dialogList)
		{
			if (adialog.dialogueId == dialogId)
				return adialog;
		}
		return null;
	}
    public static List<Dialog> GetListOfChoicesByDialog(Dialog currentDialog)
    {
        string[] textdialogIdArray = currentDialog.choices.Split('@');
        List<Dialog> choices = new List<Dialog>();
        for(int i =0; i < textdialogIdArray.Length; i++)
        {
            string[] choicedialogArray = textdialogIdArray[i].Split('#');
            Debug.Log(GetDialogByDialogId(choicedialogArray[1]).dialogueId);
            //Debug.Log(choicedialogArray[0]);
            choices.Add(GetDialogByDialogId(choicedialogArray[1]));
        }
        return choices;
    }
	public static void SetDialogList(List<Dialog> adialog)
	{
		dialogList = adialog;
	}
	#endregion
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
    public static void SetSession(session asession2)
    {
        mainsession = asession2;
    }
    public static void SetLevelList(List<level> alevel)
    {
        levellist = alevel;
    }

	public static void AssignAllSkillListToActor()
    {
        for (int i = 0; i < actorList.ToArray().Length-3; i++)
        {
            //Debug.Log("yes");
            List<skills> skillList = GetListOfSkillsByType(actorList.ToArray()[i].actorType);
            string stringSkillList = "";
            foreach (skills askill in skillList)
            {
                //Debug.Log("yes");
                stringSkillList += askill.skillid + ",";
            }
            string stringskillList2 = stringSkillList.Remove(stringSkillList.Length - 1);
            //Debug.Log(stringskillList2);
			//stringSkillList.Remove(stringSkillList.Length - 1, 1);
			actorList.ToArray()[i].skillslist = stringskillList2;
        }
    }

    public static void SetSessionWeaponVariable(string change)
    {
        mainsession.weapon = Game.Getitemsbyid(change).displayName;
    }
}
