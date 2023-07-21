//using pattayaA3;
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invent : MonoBehaviour
{
    public GameObject menu;
    public bool isOpenInventory;
    public GameObject inventory;
    public GameObject test;

    public void ActivateInvent()
    {
        Debug.Log($"this is mainsessionData before instantiate: {Game.mainsessionData.saveId}, {Game.mainsessionData.actorName}");
		test = Instantiate(menu, GameObject.FindGameObjectWithTag("Inventory").transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Inventory").transform) as GameObject;
		Debug.Log($"this is mainsessionData after instantiate: {Game.mainsessionData.saveId}, {Game.mainsessionData.actorName}");
		//test.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform, false);
        //Instantiate(canvas);
    }

    public void DeActivateInvent()
    {
        GameObject.Destroy(test);
    }

    public void ToggleInventory(bool isOpenInventory)
    {
        SetInventory(!isOpenInventory);
    }

    public void SetInventory(bool aInventory)
    {
        isOpenInventory = aInventory;
        if (isOpenInventory == true)
        {
            ActivateInvent();
            test.SetActive(true);
        }
        else
        {
            DeActivateInvent();
            test.SetActive(false);
        }
    }
}
