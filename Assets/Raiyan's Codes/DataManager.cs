using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        Debug.Log(dataString);



        DemoData demoData = JsonUtility.FromJson<DemoData>(dataString);


        //Debug.Log(demoData.items[0].displayName);
        //Debug.Log(demoData);

        Debug.Log(demoData.player[0].name);
        Debug.Log(demoData.enemydummy[0].displayName);
        Debug.Log(demoData.items[0].displayName);

        //process data
        ProcessDemoData(demoData);
    }

    private void ProcessDemoData(DemoData demoData)
    {
        List<items> itemList = new List<items>();
        List<EnemyBaseData> enemyList = new List<EnemyBaseData>();
        List<player> playerList = new List<player>();
        
        
        foreach (RefItems refItem in demoData.items)
        {
            items item = new items(refItem.itemId, refItem.displayName,
                refItem.cLass, refItem.vitalityBuff, refItem.powerBuff,
                refItem.expertiseBuff, refItem.cost);

            itemList.Add(item);
        }
        Game.SetItemList(itemList);

        foreach (RefEnemies refenemy in demoData.enemydummy)
        {
            EnemyBaseData enemy = new EnemyBaseData(refenemy.enemyId, refenemy.displayName, refenemy.healthMax, refenemy.enemyattk);

            //itemList.Add(item);
            enemyList.Add(enemy);
        }
        Game.SetEnemyList(enemyList);

        foreach (RefPlayer refplayer in demoData.player)
        {
            player pLayer = new player(refplayer.name, refplayer.maxhp, refplayer.atkdmg);

            //itemList.Add(item);
            //enemyList.Add(enemy);
            playerList.Add(pLayer);
        }
        Game.SetPlayerList(playerList);
    }
}
