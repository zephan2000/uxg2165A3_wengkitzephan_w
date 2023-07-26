using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class DataManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadRefData();

    }

    public void LoadRefData()
    {
        Game.filePath = "Assets/Paths/export.json";
        #region Save System (Zephan)
        Game.saveFilePath = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log($"this is the persistentDatapath {Game.saveFilePath}");
		FileInfo saveFileInfo = new FileInfo(Game.saveFilePath);

        if (saveFileInfo.Exists)
        {
			string saveString = File.ReadAllText(Game.saveFilePath);
			if (saveString.Length != 0)
            {
                Debug.Log("this is saveString " + saveString);
                if (!string.IsNullOrWhiteSpace(saveString))
                {
                    Debug.Log($"running process save data function");
                    Game.ProcessSaveData();
                }
            }
        }
        else
        {
            StreamWriter fileWriter = File.CreateText(Game.saveFilePath);
            fileWriter.WriteLine();
            fileWriter.Close();
        }
		#endregion 

		//string saveString = File.ReadAllText(Game.saveFilePath);
		//if (saveString != "")
		//    Game.ProcessSaveData();

		AssetManager.LoadFile("export.json", Game.filePath, (TextAsset t) =>
        {
            //t.text = 
            DemoData demoData = JsonUtility.FromJson<DemoData>(t.text);
            ProcessDemoData(demoData);
            //Game.ProcessSaveData();
            Game.AssignAllSkillListToActor();
        });

        //if (saveString != "")
        //{
        //    //AssetManager.LoadFile("save.json", Game.saveFilePath, (TextAsset t) =>
        //    //{
        //    //    Game.demoData2 = JsonUtility.FromJson<DemoData>(t.text);
        //    //    Game.ProcessSaveData(Game.demoData2);
        //    //    Game.GetSave();
        //    //});
        //}


        //Game.filePath = Path.Combine(Application.persistentDataPath, "export.json");
        ////Game.testFilePath = Path.Combine(Application.persistentDataPath, "export_backup.json");
        //Game.saveFilePath = Path.Combine(Application.persistentDataPath, "save.json");

        //string dataString = File.ReadAllText(Game.filePath);
        //DemoData demoData = JsonUtility.FromJson<DemoData>(dataString);


        //Debug.Log(demoData.items[0].displayName);
        //Debug.Log(demoData);

        //Debug.Log(demoData.player[0].name);
        //Debug.Log(demoData.enemydummy[0].displayName);
        //Debug.Log(demoData.items[0].displayName);

        //process data
        //ProcessDemoData(demoData);
        //      Game.ProcessSaveData();

        //actor best = Game.Getactorbytype("Player");
        //Debug.Log(best.power);
        //Debug.Log("Yes");
        //Debug.Log(Game.GetItemList());
        //Game.ListdownSkills();
        //Game.AssignAllSkillListToActor();
        //Game.ListdownSkills();
        //Game.ListdownSkills("enemyBat");

        //Game.AssignAllSkillListToActor();

        //Game.ListdownSkills("enemyBat");
        //Debug.Log(Game.GetActorByName("Warrior").skillslist);
        //Debug.Log(Game.GetEquipment(Game.mainsessionData.actorType));
        /*        List<string> testString = Game.GetListOfSkillsPartOne(Game.mainsessionData.actorType);
                List<string> testString2 = new List<string>();
                foreach (var test in testString)
                {
                    //Debug.Log(test);
                    string[] LSarray = test.Split('_');
                    testString2.Add(LSarray[0]);
                }
                foreach (var test in testString2)
                {
                    Debug.Log(test);
                }*/
        //if (Game.Getactorbytype("enemyBat").skillslist == null)
        //{
        //    Debug.Log(Game.Getactorbytype("enemyBat").actorType);
        //}
        //else
        //{
        //    Debug.Log(Game.Getactorbytype("enemyBat").skillslist);
        //}
        //foreach (var a in Game.GetItemsInInventory())
        //{
        //    Debug.Log(a.itemId);
        //}
        //Debug.Log(Game.mainsessionData.inventory);

    }



	private void ProcessDemoData(DemoData demoData)
    {
        List<items> itemList = new List<items>();
        //List<EnemyBaseData> enemyList = new List<EnemyBaseData>();
        //List<player> playerList = new List<player>();
        List<actor> actorList = new List<actor>();
        List<skills> skillList = new List<skills>();
        //List<session> sessionList = new List<session>();
        List<Dialog> dialogList = new List<Dialog>();
        List<level> levelList = new List<level>();
        List<Quest> questList = new List<Quest>();
        
        foreach (RefItems refItem in demoData.items)
        {
            items item = new items(refItem.itemId, refItem.itemType,
                refItem.actorType, refItem.displayName, refItem.vitalityBuff,
                refItem.physicaldmgBuff, refItem.magicdmgBuff, refItem.cost,
                refItem.displaySpritePath);

            itemList.Add(item);
        }
        Game.SetItemList(itemList);
        
        foreach (refSkills refskills in demoData.skills)
        {
            skills skill = new skills(refskills.skillid, refskills.actorType, refskills.category, refskills.target, refskills.skillname, refskills.dmg, refskills.hpgain, refskills.priority, refskills.maxuses);

            skillList.Add(skill);
        }
        Game.SetSkillList(skillList);

        /*
        foreach (refSession refsession in demoData.session)
        {
            session asession = new session(refsession.seshname, refsession.actorType, refsession.maxhp, refsession.defense, refsession.physicaldmg, refsession.magicdmg, refsession.vitality,
                refsession.power, refsession.intelligence, refsession.attspeed, refsession.exp, refsession.gold, refsession.weapon, refsession.helmet, refsession.armour, refsession.displaySpritePath);

            sessionList.Add(asession);
        }
        Game.SetSessionList(sessionList);*/
        //save load system will use a foreach loop
        //session asession = new session(demoData.session[0].seshname, demoData.session[0].actorType, demoData.session[0].levelId, demoData.session[0].maxhp, demoData.session[0].physicaldmg, demoData.session[0].magicdmg, demoData.session[0].vitality,
        //        demoData.session[0].power, demoData.session[0].intelligence, demoData.session[0].attspeed, demoData.session[0].attributePoint, demoData.session[0].exp, demoData.session[0].gold, demoData.session[0].weapon, demoData.session[0].helmet, demoData.session[0].armour, demoData.session[0].inventory, demoData.session[0].displaySpritePath);
        //Game.SetSession(asession);

        foreach (refActor refactor in demoData.actor)
        {
            List<skills> skillListtemp = Game.GetListOfSkillsByType(refactor.actorType);
            //string stringSkillList = "";
            //foreach (skills askill in skillListtemp)
            //{
            //    stringSkillList += askill.skillid;
            //}
            //refactor.skillslist = stringSkillList;
            //Debug.Log($"This is {refactor.actorType}'s speed {refactor.attspeed}");
            actor aCtor = new actor(refactor.actorType, refactor.displayName, refactor.skillslist, refactor.displaySpritePath);

            actorList.Add(aCtor);
        }
        Game.SetActorList(actorList);



		#region Zephan's Data
		foreach (refLevel reflevel in demoData.level)
		{
			level lEvel = new level(reflevel.levelId, reflevel.actorType, reflevel.expToGain, reflevel.maxExp, reflevel.expGain, reflevel.goldGain, reflevel.basehp,
				reflevel.physicaldmg, reflevel.magicdmg, reflevel.vitality, reflevel.power, reflevel.intelligence, reflevel.attspeed);

			levelList.Add(lEvel);
		}
		Game.SetLevelList(levelList);
		foreach (refDialogue dialogue in demoData.dialogue)
        {
            Dialog diAlogue = new Dialog(dialogue.dialogueId, dialogue.nextdialogueId, dialogue.dialogueType, dialogue.currentSpeakerName,
                dialogue.displaySpritePathLeft, dialogue.displaySpritePathRight, dialogue.dialogueText, dialogue.choices);
            dialogList.Add(diAlogue);
        }
        Game.SetDialogList(dialogList);
		foreach (refQuest quest in demoData.quest)
		{
			//Debug.Log($"this is quest: {quest.questId}, {quest.questType}, {quest.levelRequirement}, {quest.numberOf}, {quest.actorTypeToSlay}, {quest.expReward}, {quest.goldReward}, {quest.questdialogIdTrigger}");
			Quest quEst = new Quest (quest.questId, quest.questType, quest.questName, quest.questReq, quest.actorTypeToSlay, quest.expReward, quest.goldReward, quest.questdialogIdTrigger);
			questList.Add(quEst);
            //Debug.Log($"this is quEst : {quEst.questId}");
		}
        Game.questList = questList;
		#endregion


	}
}
