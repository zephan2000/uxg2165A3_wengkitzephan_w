using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor.AddressableAssets;


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
        Game.ListdownSkills();
    }

    private void ProcessDemoData(DemoData demoData)
    {
        List<items> itemList = new List<items>();
        //List<EnemyBaseData> enemyList = new List<EnemyBaseData>();
        //List<player> playerList = new List<player>();
        List<actor> actorList = new List<actor>();
        List<skills> skillList = new List<skills>();
        List<session> sessionList = new List<session>();

        foreach (refActor refactor in demoData.actor)
        {
            actor aCtor = new actor(refactor.actorType, refactor.displayName, refactor.maxhp, refactor.defense, refactor.physicaldmg, refactor.magicdmg,
                refactor.vitality, refactor.power, refactor.intelligence, refactor.speed, refactor.exp, refactor.gold, refactor.displaySpritePath);

            actorList.Add(aCtor);
        }
        Game.SetActorList(actorList);
        
        foreach (RefItems refItem in demoData.items)
        {
            items item = new items(refItem.itemId, refItem.itemType,
                refItem.actorType, refItem.displayName, refItem.defenseBuff,
                refItem.physicaldmgBuff, refItem.magicdmgBuff, refItem.cost);

            itemList.Add(item);
        }
        Game.SetItemList(itemList);
        
        foreach (refSkills refskills in demoData.skills)
        {
            skills skill = new skills(refskills.skillid, refskills.actorType, refskills.category, refskills.skillname, refskills.dmg, refskills.hpgain, refskills.accuracy, refskills.priority, refskills.maxuses);

            skillList.Add(skill);
        }
        Game.SetSkillList(skillList);

        foreach (refSession refsession in demoData.session)
        {
            session asession = new session(refsession.seshname, refsession.actorType, refsession.maxhp, refsession.defense, refsession.physicaldmg, refsession.magicdmg, refsession.vitality,
                refsession.power, refsession.intelligence, refsession.speed, refsession.exp, refsession.gold, refsession.weapon, refsession.helmet, refsession.armour, refsession.displaySpritePath);

            sessionList.Add(asession);
        }
        Game.SetSessionList(sessionList);

    }
}
