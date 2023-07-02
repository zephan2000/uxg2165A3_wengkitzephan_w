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
        actor best = Game.Getactorbyid("actor03");
        //Debug.Log(best.displayName);
        //Debug.Log("Yes");
        //Debug.Log(Game.GetActorList().Count);
    }

    private void ProcessDemoData(DemoData demoData)
    {
        //List<items> itemList = new List<items>();
        //List<EnemyBaseData> enemyList = new List<EnemyBaseData>();
        //List<player> playerList = new List<player>();
        List<actor> actorList = new List<actor>();

        foreach (refActor refactor in demoData.actor)
        {
            actor aCtor = new actor(refactor.actorId, refactor.actorType, refactor.displayName,
                refactor.vitality, refactor.power, refactor.expertise, refactor.exp,
                refactor.gold, refactor.displaySpritePath);

            actorList.Add(aCtor);
        }
        Game.SetActorList(actorList);
        
        /*
        foreach (RefItems refItem in demoData.items)
        {
            items item = new items(refItem.itemId, refItem.displayName,
                refItem.cLass, refItem.vitalityBuff, refItem.powerBuff,
                refItem.expertiseBuff, refItem.cost);

            itemList.Add(item);
        }
        Game.SetItemList(itemList);
        */

        /*
        foreach (RefEnemies refenemy in demoData.enemydummy)
        {
            EnemyBaseData enemy = new EnemyBaseData(refenemy.enemyId, refenemy.displayName, refenemy.healthMax, refenemy.enemyattk);

            //itemList.Add(item);
            enemyList.Add(enemy);
        }
        Game.SetEnemyList(enemyList);
        */

        /*
        foreach (RefPlayer refplayer in demoData.player)
        {
            player pLayer = new player(refplayer.name, refplayer.maxhp, refplayer.atkdmg);

            //itemList.Add(item);
            //enemyList.Add(enemy);
            playerList.Add(pLayer);
        }
        Game.SetPlayerList(playerList);
        */
    }
}
