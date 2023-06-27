using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    private static List<items> itemlist;
    private static List<player> playerlist;
    private static List<EnemyBaseData> enemylist;

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

    public static items Getitemsbyid(string id)
    {
        foreach(items item in itemlist)
        {
            if (item.itemId == id) return item;
        }
        return null;
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
}
