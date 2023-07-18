using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor.AddressableAssets;
using System.ComponentModel;
using pattayaA3;
using System.Net.NetworkInformation;

public class DataManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadRefData();
    }

    public void LoadRefData()
    {
        string filePath = Path.Combine(Application.dataPath, "export.json");
        string dataString = File.ReadAllText(filePath);
        //Debug.Log(dataString);
        DemoData demoData = JsonUtility.FromJson<DemoData>(dataString);


        //Debug.Log(demoData.items[0].displayName);
        //Debug.Log(demoData);

        //Debug.Log(demoData.player[0].name);
        //Debug.Log(demoData.enemydummy[0].displayName);
        //Debug.Log(demoData.items[0].displayName);

        //process data
        ProcessDemoData(demoData);
        //actor best = Game.Getactorbytype("Player");
        //Debug.Log(best.power);
        //Debug.Log("Yes");
        //Debug.Log(Game.GetItemList());
        //Game.ListdownSkills();
        //Game.AssignAllSkillListToActor();
        //Game.ListdownSkills();
        //Game.ListdownSkills("enemyBat");
        Game.AssignAllSkillListToActor();
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
        List<session> sessionList = new List<session>();
        List<Dialog> dialogList = new List<Dialog>();
        List<level> levelList = new List<level>();
        
        foreach (RefItems refItem in demoData.items)
        {
            items item = new items(refItem.itemId, refItem.itemType,
                refItem.actorType, refItem.displayName, refItem.defenseBuff,
                refItem.physicaldmgBuff, refItem.magicdmgBuff, refItem.cost,
                refItem.displaySpritePath);

            itemList.Add(item);
        }
        Game.SetItemList(itemList);
        
        foreach (refSkills refskills in demoData.skills)
        {
            skills skill = new skills(refskills.skillid, refskills.actorType, refskills.category, refskills.target, refskills.skillname, refskills.dmg, refskills.hpgain, refskills.accuracy, refskills.priority, refskills.maxuses);

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
        session asession = new session(demoData.session[0].seshname, demoData.session[0].actorType, demoData.session[0].levelId, demoData.session[0].maxhp, demoData.session[0].physicaldmg, demoData.session[0].magicdmg, demoData.session[0].vitality,
                demoData.session[0].power, demoData.session[0].intelligence, demoData.session[0].attspeed, demoData.session[0].attributePoint, demoData.session[0].exp, demoData.session[0].gold, demoData.session[0].weapon, demoData.session[0].helmet, demoData.session[0].armour, demoData.session[0].inventory, demoData.session[0].displaySpritePath);
        Game.SetSession(asession);

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

        foreach (refLevel reflevel in demoData.level)
        {
            level lEvel = new level(reflevel.levelId, reflevel.actorType, reflevel.expToGain, reflevel.maxExp, reflevel.expGain, reflevel.goldGain, reflevel.maxhp,
                reflevel.physicaldmg, reflevel.magicdmg, reflevel.vitality, reflevel.power, reflevel.intelligence, reflevel.attspeed);

            levelList.Add(lEvel);
        }
        Game.SetLevelList(levelList);

        //Zephan
        foreach(refDialogue dialogue in demoData.dialogue)
        {
            Dialog diAlogue = new Dialog(dialogue.dialogueId, dialogue.nextdialogueId, dialogue.dialogueType, dialogue.currentSpeakerName,
                dialogue.displaySpritePathLeft, dialogue.displaySpritePathRight, dialogue.dialogueText, dialogue.choices);
            dialogList.Add(diAlogue);
        }
        Game.SetDialogList(dialogList);


    }
}
