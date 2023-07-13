using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EachItem : MonoBehaviour
{
    public GameObject itemUi;
    public GameObject itemUi_addition;

    //public GameObject initiate;
    public GameObject itemUiToBeCreated;
    public GameObject itemUi_additionToBeCreated;

    public EachItem test = Game.GetEachItem();

    public session currentSession = Game.GetSession();

    public int currentcount = 0;
    public int realcount { get; set; }
    
    public bool samecount;
    public List<string> listOfInventory = new List<string> ();

    public bool CheckforInventory()
    {
        if (currentcount == test.realcount)
        {
            return true;
        }
        return false;
    }
    public void ActivateUI()
    {
        itemUi_additionToBeCreated = Instantiate(itemUi_addition, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        itemUi_additionToBeCreated.transform.SetParent(GameObject.FindGameObjectWithTag("ItemList").transform, false);

        itemUiToBeCreated = Instantiate(itemUi, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        itemUiToBeCreated.transform.SetParent(GameObject.FindGameObjectWithTag("ItemList").transform, false);
    }


}
