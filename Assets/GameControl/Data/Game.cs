using pattayaA3;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using System.Text;
using static UnityEditor.Progress;

public static class Game
{
    private static List<items> itemlist;
    private static List<player> playerlist;
    private static List<EnemyBaseData> enemylist;
    private static List<actor> actorList;
    private static player mainPlayer;
    private static List<skills> skillslist;

    
   

	private static List<Dialog> dialogList;
    private static List<level> levellist;
    private static int enemyPokemonLevel = 1;
	private static int darkWizardLevel = 1;
    private static EachItem eachitem;
     //data added in from DemoData
	
    public static bool initialStart = true;
	//Save
	public static List<save> saveList;
	public static string saveFilePath;
	public static string filePath;
	public static string testFilePath;
	public static save mainsessionData;

	//Quest
	public static List<Quest> questList;
	public static Quest startedQuest;
    public static bool questInProgress;
	public static int battleQuestProgress;
	public static bool questComplete;

    //Analytics
    public static string [] battleResultByEnemyType;
    public static bool isBattleOver;// format: enemyType_battleStatus, where battleStatus {0 = Run, 1 = Lost, 2 = Won}
    public static int battlesWon;
	public static int battlesLost;
	public static int battlesRan;
    public static string enemyTypeForAnalytics;
    public static int gameRunTime;

    //Achievement

	public static string chosenenemyName { get; set; }
    public static string chosenenemyType { get; set; }
    public static string sessionactorName { get; set; }
    public static string sessionactorType { get; set; }
    public static bool levelUp { get; set; }
    public static int playerLevel;
	public static int playerExp { get; set; }
  
	public static float maxHP{ get; set; }
    public static string currentPokemonType { get; set; }
    public static bool playerWon { get; set; }  
    public static bool playerRan { get;set; }

    public static float currentEXP;
	public static float currentexpToGain { get; set; }
    public static int currentmaxEXP;
    public static int runonce { get; set; }
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



	public static actor GetActorByActorType(string type)
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
    public static actor GetActorByActorName(string name)
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
        return GetActorByActorType(type).skillslist;
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
        
        return Game.GetActorByActorType(type).skillslist;
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
	#region Zephan's Data

	#region Level System
	public static void SetSessionDataFromLevelId(string levelid) // triggers when you level up
    {
        level alevel = GetLevelByLevelId(levelid);
        mainsessionData.maxhp = alevel.maxhp;
		mainsessionData.physicaldmg = alevel.physicaldmg;
		mainsessionData.magicdmg = alevel.magicdmg;
		mainsessionData.vitality = alevel.vitality;
		mainsessionData.power = alevel.power;
		mainsessionData.intelligence = alevel.intelligence;
		mainsessionData.attspeed = alevel.attspeed;
        playerLevel = int.Parse(levelid.Split('_')[1]);
        currentmaxEXP = GetLevelByLevelId(mainsessionData.levelId).maxExp;
		Debug.Log($"this is: {levelid}, current level max exp:{alevel.maxExp}, Game's currentexp: {Game.mainsessionData.exp}, currentmaxEXP: {Game.currentmaxEXP}");
		currentexpToGain = GetLevelByLevelId(mainsessionData.levelId).expToGain;
        maxHP = GetLevelByLevelId(mainsessionData.levelId).maxhp;
	}
    public static void CreateNewMainSession(string levelid)
    {
        level alevel = GetLevelByLevelId(levelid);
        Debug.Log($"checking actorType{levelid.Split('_')[0]}");
        actor currentactor = GetActorByActorType(levelid.Split('_')[0]);
        string saveId = "";
        int saveIdnumber = 0;
        if (saveList != null)
        {
            foreach (save saveData in saveList)
            {
                saveIdnumber++;
            }
        }
        if (saveIdnumber < 10)
        {
            saveId = "save000" + ++saveIdnumber;

        }
        else { }
		Debug.Log($"debugging current actor : {currentactor.displayName}, {currentactor.actorType}");
		Debug.Log($"constructing mainsessionData: {saveId},{"lewis"}, {"ACTIVE"}, {currentactor.displayName}, {currentactor.actorType}");
        Debug.Log($"constructing mainsessionData:{levelid}, {alevel.maxhp}, {alevel.maxhp}, {alevel.physicaldmg}");
        Debug.Log($"constructing mainsessionData: {alevel.magicdmg}, {alevel.vitality}");
        Debug.Log($"constructing mainsessionData:{alevel.power}, {alevel.intelligence}, {alevel.attspeed},0, 0, 0");
        Debug.Log($"constructing mainsessionData:{"item10"}, {"item14"}, {"item13"},{""}");
        Debug.Log($"constructing mainsessionData: {currentactor.displaySpritePath}, {""}, {""}, {""}, 0,{""}");


		mainsessionData = new save(saveId, "lewis", "ACTIVE", currentactor.displayName, currentactor.actorType, 
                                   levelid, alevel.maxhp, alevel.maxhp, alevel.physicaldmg, alevel.magicdmg, alevel.vitality,
                                   alevel.power, alevel.intelligence, alevel.attspeed, 0, 0, 0, "item10", "item14", "item13", "", 
                                   currentactor.displaySpritePath, "", "", "", 0,"");

        SaveToJSON<save>(saveList);
	}
	public static int GetLevelFromLevelId(string levelid)
	{
		string[] levelIdArray = levelid.Split('_');
        return Int32.Parse(levelIdArray[1]);
    }

    public static level GetLevelbyActorType(string type)
	{
		foreach (level level in levellist)
		{
			if (level.actorType == type)
			{
				//Debug.Log(aactor.actorType);
				//Debug.Log(aactor.displaySpritePath);
				return level;
			}
		}
		return null;
	}

	public static level GetLevelByLevelId (string levelid)
	{
		foreach (level alevel in levellist)
		{
            if (alevel.levelId == levelid)
            {
				return alevel;
			}
           
		}
		return null;
	}
	#endregion
	#region File Handling (Save System)

	public static void ProcessSaveData()
	{
        string saveString = File.ReadAllText(saveFilePath);
        DemoData saveData = JsonUtility.FromJson<DemoData>(saveString);
		List<save> saveDataList = new List<save>();

		foreach (refSave refData in saveData.save)
		{
			save savedata = new save(refData.saveId, refData.seshname, refData.saveStatus, refData.actorName, refData.actorType, refData.levelId, refData.currenthp, refData.maxhp, refData.physicaldmg, refData.magicdmg, refData.vitality, refData.power,
							refData.intelligence, refData.attspeed, refData.attributePoint, refData.exp, refData.gold, refData.weapon, refData.helmet, refData.armour, refData.inventory, refData.displaySpritePath, 
                            refData.startedQuest, refData.completedQuest, refData.completedAchievement, refData.runtime, refData.battles);
			saveDataList.Add(savedata);
		}
		saveList = saveDataList;
	}
	public static save GetSave() //for single play (testing purposes)
	{
		foreach (save savedata in saveList)
		{
			if (savedata.saveStatus == "ACTIVE")
				mainsessionData = savedata;
		}
        Debug.Log($"this is mainsessionId: {mainsessionData.levelId}");
		return mainsessionData;
	}

    public static void LoadSave(string saveId)
    {
		Debug.Log($"this is incoming saveId: {saveId}");
		foreach (save savedata in saveList)
        {
            //if (saveId.Equals(savedata.saveId))
            if(savedata.saveId == saveId)
            {
				Debug.Log($"this is the saveId found: {savedata.saveId}");
				mainsessionData = savedata;
				mainsessionData.saveStatus = "ACTIVE";
                SaveToJSON<save>(saveList);
                Debug.Log($"This is soemthing done /////////////////////////////////////////////");
			}
                Debug.Log($"================================================\n{savedata.saveId}");
        }
    }
    public static void SaveToJSON<T>(List<T> toSave) where T:save// change this to Save, manually write the JSON formula
    {
        if(toSave != null)
        {
			var jsonString = new StringBuilder();
			save finalObj = toSave.Last();
            List<save> newSaveList = new List<save>();
            bool sessionDataSaved = false;
			jsonString.Append("{\"save\":[");

            foreach (save savedata in saveList)
            {
                Debug.Log($"this is savedataId: {savedata.saveId}");
                if (savedata == finalObj && mainsessionData.saveId != savedata.saveId)
                    newSaveList.Add(mainsessionData);

                    newSaveList.Add(savedata);
            }
            newSaveList.Reverse();
            saveList = newSaveList;
            finalObj = newSaveList.Last();
			foreach (save savedata in saveList)
			{
				if (savedata != finalObj)
				{
					if (savedata.saveId == mainsessionData.saveId && sessionDataSaved != true)
					{
                        if(finalObj.saveId == mainsessionData.saveId)
                        {
                            jsonString.Append($"{JsonUtility.ToJson(mainsessionData)},");
							Debug.Log($"Match found: this is final object, saveId {mainsessionData.saveId} and the saveStatus {mainsessionData.saveStatus}");
                        }
                        else
                        {
							sessionDataSaved = false;
							jsonString.Append($"{JsonUtility.ToJson(mainsessionData)},");
							Debug.Log($"Match found: this is, saveId {mainsessionData.saveId} and the saveStatus {mainsessionData.saveStatus}");
						}
                        
					}

					else
						jsonString.Append($"{JsonUtility.ToJson(savedata)},");

				}
				else
					jsonString.Append(JsonUtility.ToJson(savedata));
			}
			jsonString.Append("]}");
			//string content = JsonHelper.ToJson<T>(toSave.ToArray());
			//Debug.Log($"this is the content in ToSave: {toSave.seshname}");
			Debug.Log($"now saving: {jsonString.ToString()}");
			WriteFile(Game.saveFilePath, jsonString.ToString());
		}
        else
        {
			var jsonString = new StringBuilder();
			jsonString.Append("{\"save\":[");
			jsonString.Append($"{JsonUtility.ToJson(mainsessionData)}");
			jsonString.Append("]}");
			Debug.Log($"now saving: {jsonString.ToString()}");
			WriteFile(Game.saveFilePath, jsonString.ToString());
		}
        
    }

    public static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    public static void StartNewGame(string chosenClassText)
    {
        if(chosenClassText != "")
        {
			switch (chosenClassText)
			{
				case "playerWarrior_1":
					Debug.Log($"this is mainsessiondata's levelid {chosenClassText}");
                    CreateNewMainSession(chosenClassText);
					SetSessionDataFromLevelId(mainsessionData.levelId);
					break;
				case "playerWizard_1":
					CreateNewMainSession(chosenClassText);
					SetSessionDataFromLevelId(mainsessionData.levelId);
					break;
				case "playerArcher_1":
					CreateNewMainSession(chosenClassText);
					SetSessionDataFromLevelId(mainsessionData.levelId);
					break;
			}
		}
        
    }

	#endregion
	#region Quest

    public static void SetQuestData()
    {
        string[] questdata = mainsessionData.startedQuest.Split('_');
        Game.startedQuest = GetQuestByQuestId(questdata[0]);
        Game.battleQuestProgress = Int32.Parse(questdata[1]);
    }

    public static void UpdateBattleQuestProgress()
    {
        battleQuestProgress++;
        mainsessionData.startedQuest = startedQuest.questId + "_" + battleQuestProgress;
        SaveToJSON<save>(saveList);
    }
    public static void UpdateCompletedQuest()
    {
		if (mainsessionData.completedQuest == "")
			mainsessionData.completedQuest = mainsessionData.startedQuest.Split('_')[0];
		else
			mainsessionData.completedQuest = mainsessionData.startedQuest.Split('_')[0] + "@" + mainsessionData.completedQuest;

        mainsessionData.startedQuest = "";
		SaveToJSON<save>(saveList);
	}
	public static Quest GetQuestByQuestId(string selectedquestId)
	{
		foreach (Quest aquest in questList)
		{
			if (aquest.questId == selectedquestId)
				return aquest;
		}
		return null;
	}

	#endregion

	#region Dialog
	public static List<Dialog> GetDialogByType(string type)
	{
		List<Dialog> ndialog = new List<Dialog>();
        if(dialogList == null) 
		    Debug.Log($"this is all dialog in dialogList: ");
		foreach (Dialog adialog in dialogList)
		{
            
			if (adialog.dialogueType == type)
			{
				ndialog.Add(adialog);
			}
		}
		return ndialog;
	}

	public static Dialog GetDialogByDialogId(string dialogId)
	{
		foreach (Dialog adialog in dialogList)
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
		for (int i = 0; i < textdialogIdArray.Length; i++)
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
	#endregion Dialog

	#region Analytics

	

	public static void AssignBattleResult()
    {
        if(playerRan)
        {
            if (mainsessionData.battles == "")
                mainsessionData.battles = chosenenemyType + '_' + "0";
            else
                mainsessionData.battles = mainsessionData.battles + "@" + chosenenemyType + '_' + "0";
		}
		else if (!playerRan && !playerWon)
		{
			if (mainsessionData.battles == "")
				mainsessionData.battles = chosenenemyType + '@' + "1";
			else
				mainsessionData.battles = mainsessionData.battles + "@" + chosenenemyType + '_' + "1";
		}
		else if (!playerRan && playerWon)
		{
			if (mainsessionData.battles == "")
				mainsessionData.battles = chosenenemyType + '@' + "2";
			else
				mainsessionData.battles = mainsessionData.battles + "@" + chosenenemyType + '_' + "2";
		}
	}

    public static string [] BattleResultByEnemyType(string enemyType)
    {
        string[] eachBattle = mainsessionData.battles.Split('@');
		List<string> battleResultList = new List<string>();
		foreach (string battle in eachBattle)
        {

            if (battle.Contains(enemyType))
                battleResultList.Add(battle);
		}

        foreach (string battle in battleResultList)
        {
            Debug.Log($"this is a string in battleResultList {battle}, with enemyType name {enemyType}");
        }
        battleResultByEnemyType = battleResultList.ToArray();
        return battleResultByEnemyType;
    }

	#endregion

	#region Achievement

    public static int GetNumberOfBattles()
    {
        int noofBattles = 0;

        string[] battleString = mainsessionData.battles.Split('@');
        for(int i = 0; i < battleString.Length; i++)
        {
            noofBattles++;
        }

        return noofBattles;
    }

	#endregion
	public static int SetEnemyPokemonLevel(string pokemonLevel)
    {
        return enemyPokemonLevel = Int32.Parse(pokemonLevel);
    }
    public static int SetDarkWizardLevel(int darkWizardLevel)
    {
        return darkWizardLevel;
    }

	public static int GetEnemyPokemonLevel()
	{
        return enemyPokemonLevel;
	}
	public static int GetDarkWizardLevel()
	{
		return darkWizardLevel;
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
    //public static void SetSessionList(List<session> asession)
    //{
    //    sessionlist = asession;
    //}
    //public static void SetSession(session asession2)
    //{
    //    mainsession = asession2;
    //}
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
        mainsessionData.weapon = Game.Getitemsbyid(change).displayName;
    }

    public static void AddItemToInventory(string itemid)
    {
        if (mainsessionData.inventory == "")
        {
            mainsessionData.inventory += itemid;
            //eachitem.realcount++;
        }
        else
        {
            mainsessionData.inventory += "," + itemid;
            //eachitem.realcount++;
        }
    }

    public static EachItem GetEachItem()
    {
        return eachitem;
    }
}
