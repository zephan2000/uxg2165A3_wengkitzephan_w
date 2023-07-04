using pattayaA3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    private static List<items> itemlist;
    private static List<player> playerlist;
    private static List<EnemyBaseData> enemylist;
    private static List<actor> actorList;
    private static player mainPlayer;

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

    public static void SetActorList(List<actor> aactor)
    {
        actorList = aactor;
    }
}
