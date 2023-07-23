using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;
namespace pattayaA3
{
    public class EachItem : MonoBehaviour
    {
        public Transform itemListing;
        public Transform itemListingObject;

        public GameObject itemUi;
        public GameObject itemUi_addition;
        public GameObject itemGroup;

        //First Part, itemUI
        public GameObject itemGroup_child01_itemUI;
        public GameObject itemGroup_child01_itemUI_addition;
        public GameObject itemGroup_child02_itemUI;
        public GameObject itemGroup_child02_itemUI_addition;



        //public GameObject initiate;
        public GameObject itemUiToBeCreated;
        public GameObject itemUi_additionToBeCreated;
        public GameObject itemGroupToBeCreated;

        public Button equipButton;

        public EachItem test = Game.GetEachItem();

        public save currentSession = Game.mainsessionData;

        public int currentcount = 0;
        public int realcount { get; set; }

        public bool samecount;
        public List<string> listOfInventory = new List<string>();
        public List<items> inventoryList;


        public void Start()
        {
            inventoryList = Game.GetItemsInInventory();
            //Game.ProcessSaveData(Game.demoData2);
            Game.ProcessSaveData();
            Game.GetSave();

            if (GameObject.FindWithTag("ItemList") == null)
            {
                
            }
            else
            {
                itemListing = GameObject.FindWithTag("ItemList").transform;
            }
        }

        public void GetAllItems()
        {
            foreach (var item in inventoryList)
            {

            }
        }

        public bool CheckforInventory()
        {
            if (currentcount == test.realcount)
            {
                return true;
            }
            return false;
        }
        public void ActivateUI(items itemIteration)
        {
            //Debug.Log("First" + itemGroup_child01_itemUI.name);

            //Get itemUI game object
            itemGroup_child01_itemUI = itemGroup.transform.GetChild(0).gameObject;
            //Get itemUI/Image game object
            itemGroup_child01_itemUI = itemGroup_child01_itemUI.transform.GetChild(0).gameObject;
            //Setting Item Image
            string itemImage = itemIteration.displaySpritePath;
            //Debug.Log("This is : " + itemIteration.itemId + " With Sprite Path :" + itemImage);

            AssetManager.LoadSprite(itemImage, (Sprite s) =>
            {
                //Debug.Log("Sprite Path" + itemImage);
                itemGroup_child01_itemUI.GetComponent<Image>().sprite = s;
            });

            //Get itemUI game object
            itemGroup_child01_itemUI_addition = itemGroup.transform.GetChild(0).gameObject;
            //Get itemUI/text game object
            itemGroup_child01_itemUI_addition = itemGroup_child01_itemUI_addition.transform.GetChild(1).gameObject;
            //Setting Item text
            itemGroup_child01_itemUI_addition.GetComponent<Text>().text = itemIteration.displayName;

            //Get itemUI game object
            itemGroup_child02_itemUI = itemGroup.transform.GetChild(1).gameObject;

            //Change button name
            itemGroup_child02_itemUI = itemGroup_child02_itemUI.transform.GetChild(1).gameObject;
            itemGroup_child02_itemUI.name = itemIteration.itemId;

            //Get itemUI game object
            itemGroup_child02_itemUI = itemGroup.transform.GetChild(1).gameObject;
            //Get itemUI/text game object
            itemGroup_child02_itemUI = itemGroup_child02_itemUI.transform.GetChild(0).gameObject;
            itemGroup_child02_itemUI = itemGroup_child02_itemUI.transform.GetChild(0).gameObject;
            //Set Item Name
            itemGroup_child02_itemUI.name = itemIteration.itemId + "_Stat1";
            //Setting Item Name
            itemGroup_child02_itemUI.GetComponent<Text>().text = "+" + itemIteration.defenseBuff.ToString() + "Vitality";

            //Get itemUI game object
            itemGroup_child02_itemUI = itemGroup.transform.GetChild(1).gameObject;
            //Get itemUI/text game object
            itemGroup_child02_itemUI = itemGroup_child02_itemUI.transform.GetChild(0).gameObject;
            itemGroup_child02_itemUI = itemGroup_child02_itemUI.transform.GetChild(1).gameObject;
            //Set Item Name
            itemGroup_child02_itemUI.name = itemIteration.itemId + "_Stat2";
            //Setting Item Name
            itemGroup_child02_itemUI.GetComponent<Text>().text = "+" + itemIteration.physicaldmgBuff.ToString() + "Power";

            //Get itemUI game object
            itemGroup_child02_itemUI = itemGroup.transform.GetChild(1).gameObject;
            //Get itemUI/text game object
            itemGroup_child02_itemUI = itemGroup_child02_itemUI.transform.GetChild(0).gameObject;
            itemGroup_child02_itemUI = itemGroup_child02_itemUI.transform.GetChild(2).gameObject;
            //Set Item Name
            itemGroup_child02_itemUI.name = itemIteration.itemId + "_Stat3";
            //Setting Item Name
            itemGroup_child02_itemUI.GetComponent<Text>().text = "+" + itemIteration.magicdmgBuff.ToString() + "Intelligence";


            //Instantiation of itemGroup object
            itemGroup.name = itemIteration.itemId;

            itemGroupToBeCreated = Instantiate(itemGroup, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            itemGroupToBeCreated.transform.SetParent(GameObject.FindGameObjectWithTag("ItemList").transform, false);


        }

        public void DeActivateInvent()
        {
            //Debug.Log("Destroys");
            GameObject.Destroy(itemGroupToBeCreated);
        }

        public void DisableItemList()
        {
            foreach (Transform child in itemListing)
            {
                //Debug.Log(child.gameObject.name);
                GameObject.Destroy(child.gameObject);

            }
        }

        public void AddInventoryfromButton()
        {
            string itemEquip = OnClicked(equipButton);

            string[] itemEquipList = itemEquip.Split("(");
            Game.AddItemToInventory(itemEquipList[0]);
        }

        public void ReplaceEquipment()
        {
            string itemEquip = OnClicked(equipButton);
            //Debug.Log("Equippin item: "+itemEquip);
            //string[] itemEquipList = itemEquip.Split("(");

            switch (Game.Getitemsbyid(itemEquip).itemType)
            {
                case "weapon":
                    Game.AddItemToInventory(Game.mainsessionData.weapon);
                    Game.mainsessionData.weapon = itemEquip;
                    Game.SaveToJSON<save>(Game.saveList);
                    //Debug.Log("Equipped item: " + Game.mainsessionData.weapon);

                    break;
                case "helmet":
                    Game.AddItemToInventory(Game.mainsessionData.helmet);
                    Game.mainsessionData.helmet = itemEquip;
                    Game.SaveToJSON<save>(Game.saveList);
                    //Debug.Log("Equipped item: " + Game.mainsessionData.helmet);
                    break;
                case "armour":
                    Game.AddItemToInventory(Game.mainsessionData.armour);
                    Game.mainsessionData.armour = itemEquip;
                    Game.SaveToJSON<save>(Game.saveList);
                    //Debug.Log("Equipped item: " + Game.mainsessionData.armour);
                    break;
            }
            Debug.Log("items: " + Game.mainsessionData.inventory);
            Debug.Log("items: " + Game.mainsessionData.weapon);
        }
        public void RemoveFromItemList()
        {
            string replaceString = "";
            string itemEquip = OnClicked(equipButton);
            string[] itemEquipList = itemEquip.Split("(");

            string inventoryList = Game.mainsessionData.inventory;
            foreach (var a in Game.GetItemsInInventory())
            {
                Debug.Log("Item : " + a.itemId);
                if (!(a.itemId == itemEquipList[0]))
                {
                    Debug.Log("Will be removed");
                    replaceString += a.itemId;
                    replaceString += ",";

                }
            }
            Debug.Log("Finished list : " + replaceString);
            Debug.Log("Inventory : " + Game.GetItemsInInventory());
            Game.mainsessionData.inventory = replaceString;
            Debug.Log("Inventory : " + Game.GetItemsInInventory());
            if (!(replaceString == ""))
            {
                replaceString = replaceString.Remove(replaceString.Length - 1);
            }
            
            ReplaceEquipment();
            //DestroyOneItem();
            //DeActivateInvent();

            DisableItemList();


            //itemGroup = GameObject.FindWithTag("ItemList");

            //List<items> listinventory = Game.GetItemsInInventory();
            //for (int i = 0; i < Game.GetItemsInInventory().Count; i++)
            //{
            //    Debug.Log("This is : " + listinventory[i].itemId + " With Sprite Path :" + listinventory[i].displaySpritePath);
            //    ActivateUI(listinventory[i]);

            //    Game.ProcessSaveData();
            //    Game.GetSave();
            //    //Debug.Log(i);
            //}
        }


        public void DestroyOneItem()
        {
            itemListing = GameObject.FindGameObjectWithTag("ItemList").transform;
            string itemEquip = OnClicked(equipButton);

            foreach (Transform child in itemListing)
            {
                //Debug.Log(child.gameObject.name);
                if (child.gameObject.name == (itemEquip + "(Clone)"))
                {
                    Debug.Log("This child: " + child.gameObject.name);
                    GameObject.Destroy(child.gameObject);
                    //Destroy(GetComponent<Transform>().gameObject);
                    break;
                }


            }
        }

        public string OnClicked(Button button)
        {
            return button.name;
        }
    }
}
