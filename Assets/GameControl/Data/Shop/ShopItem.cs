using pattayaA3;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class ShopItem : MonoBehaviour
{
    public ShopMenu shopmenu;

    public Transform itemListing;
    public Transform itemListingObject;

    public GameObject itemUi;
    public GameObject itemUi_addition;
    public GameObject itemGroup;

    //First Part, itemUI
    public GameObject itemGroup_child01_itemUI;
    public GameObject itemGroup_child02_itemUI;
    public GameObject itemGroup_child03_itemUI;
    public GameObject itemGroup_child04_itemUI;
    public GameObject itemGroup_child05_itemUI;

    //public GameObject itemGroup_child01_itemUI_addition;
    //public GameObject itemGroup_child02_itemUI;
    //public GameObject itemGroup_child02_itemUI_addition;

    //public GameObject initiate;
    public GameObject itemUiToBeCreated;
    public GameObject itemUi_additionToBeCreated;
    public GameObject itemGroupToBeCreated;

    public Button buttonforBuyandSell;

    public ShopMenu takingFromHigerPower;
    public GameObject takingFromHigerPowerObject;
    public Shop shopEnum;

    public Button sellButton;
    public Button buyButton;

    public bool cycleOne;

    // Start is called before the first frame update
    void Start()
    {
        //Game.ProcessSaveData(Game.demoData2);
        Game.ProcessSaveData();
        Game.GetSave();

        itemListing = GameObject.FindWithTag("ShopList").transform;
        takingFromHigerPower = GameObject.FindWithTag("ShopBG").GetComponent<ShopMenu>();
        //takingFromHigerPower = GameObject.FindWithTag("ShopBG").;

        //takingFromHigerPower = takingFromHigerPowerObject.gameObject.GetComponent<ShopMenu>() as ShopMenu;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("This is shop state : " + takingFromHigerPower.shop_state);

    }

    public void DecideButton()
    {
        string click = OnClicked(buttonforBuyandSell);
        if (takingFromHigerPower.shop_state == Shop.Buy)
        {
            BuyItem(click);
        }
        else if (takingFromHigerPower.shop_state == Shop.Sell)
        {
            //SellItem(click);
            RemoveFromItemList();
        }
    }

    public void BuyItem(string click)
    {
        //string click = OnClicked(buyButton);
        string[] itemEquipList = click.Split("_");

        Debug.Log("Buying : "+Game.Getitemsbyid(itemEquipList[0]).displayName);
        Debug.Log("Cost : " + Game.Getitemsbyid(itemEquipList[0]).cost);
        if (Game.mainsessionData.gold >= Game.Getitemsbyid(itemEquipList[0]).cost)
        {
            Debug.Log("Buy");
            Game.mainsessionData.inventory = Game.AddItemToInventory(itemEquipList[0], Game.mainsessionData.inventory);
            Game.mainsessionData.gold = Game.mainsessionData.gold - Game.Getitemsbyid(itemEquipList[0]).cost;
            Game.SaveToJSON<save>(Game.saveList);
        }
        shopmenu = GameObject.FindWithTag("ShopBG").GetComponent<ShopMenu>();
        shopmenu.runonce = false;
    }
    public void SellItem(string click)
    {
        //string sellclick = OnClicked(buttonforBuyandSell);
        string[] itemEquipList = click.Split("_");
        //Debug.Log("Sell");
        Game.mainsessionData.gold = Game.mainsessionData.gold + Game.Getitemsbyid(itemEquipList[0]).cost;
        Game.SaveToJSON<save>(Game.saveList);
    }
    public string OnClicked(Button button)
    {
        return button.name;
    }

    public void RemoveFromItemList()
    {
        string itemEquip = OnClicked(buttonforBuyandSell);
        string[] itemEquipList = itemEquip.Split("_");
        //SellItem(itemEquip);

        string replaceString = "";
        
        //string[] itemEquipList = itemEquip.Split("(");

        string inventoryList = Game.mainsessionData.inventory;
        foreach (var a in Game.GetItemsInInventory())
        {
            //if (!cycleOne)
            //{
                
                if (!(a.itemId == itemEquipList[0]))
                {
                    //Debug.Log("Will be removed");
                    replaceString += a.itemId;
                    replaceString += ",";

                }
                else
                {
                    Debug.Log("This Item will be removed : " + a.displayName);
                    //cycleOne = true;
                }
            //}
            //else
            //{
            //    replaceString += a.itemId;
            //    replaceString += ",";
            //}
            
        }
        if (!(replaceString == ""))
        {
            replaceString = replaceString.Remove(replaceString.Length - 1);
        }
        Debug.Log("Finished list : " + replaceString);

        Game.mainsessionData.inventory = replaceString;
        SellItem(itemEquip);
        DisableItemList();

        shopmenu = GameObject.FindWithTag("ShopBG").GetComponent<ShopMenu>();
        shopmenu.runonce = false;
        //DestroyOneItem(itemEquipList[0]);
        //DisableItemList();

    }

    public void DisableItemList()
    {
        Debug.Log("I will destroy");
        foreach (Transform child in itemListing)
        {
            //Debug.Log(child.gameObject.name);
            GameObject.Destroy(child.gameObject);

        }
    }

    public void DestroyOneItem(string itemid)
    {
        GameObject destruction = GameObject.Find(itemid + "_Group");
        GameObject.Destroy(destruction);
    }

    public void UpdateSprite(string path, Image actorImage)
    {
        AssetManager.LoadSprite(path, (Sprite s) =>
        {
            //Debug.Log(path);
            actorImage.sprite = s;
        });
    }


    public GameObject ActivateUI(items itemIteration, Shop shopState)
    {
        //Debug.Log("First" + itemGroup_child01_itemUI.name);

        //Get itemUI game object //Image
        itemGroup_child01_itemUI = itemGroup.transform.GetChild(0).gameObject;
        //Get itemUI/Image game object
        itemGroup_child01_itemUI = itemGroup_child01_itemUI.transform.GetChild(0).gameObject;
        itemGroup_child01_itemUI.name = itemIteration.itemId;
        //Setting Item Image
        string itemImage = itemIteration.displaySpritePath;

        Debug.Log("This is : " + itemIteration.itemId + " With Sprite Path :" + itemImage);

        //AssetManager.LoadSprite(itemImage, (Sprite s) =>
        //{
        //    //Debug.Log("Sprite Path" + itemImage);
        //    itemGroup_child01_itemUI.GetComponent<Image>().sprite = s;
        //});
        //UpdateSprite(itemImage, itemGroup_child01_itemUI.GetComponent<Image>());
        //Image testing_image = GameObject.Find(itemIteration.itemId).GetComponent<Image>();
        //Image testing_object = testing_image.GetComponent<Image>();
        //UpdateSprite(itemImage, itemGroup_child01_itemUI.GetComponent<Image>());

        //Get itemUI game object //Name
        itemGroup_child02_itemUI = itemGroup.transform.GetChild(0).gameObject;
        //Get itemUI/Image game object
        itemGroup_child02_itemUI = itemGroup_child02_itemUI.transform.GetChild(1).gameObject;
        //Setting Item Image
        //string itemImage = itemIteration.displaySpritePath;
        itemGroup_child02_itemUI.GetComponent<Text>().text = itemIteration.displayName;

        //Get itemUI game object 3RD OBJECT and 1ST SUB-OBJECT
        itemGroup_child03_itemUI = itemGroup.transform.GetChild(0).gameObject;
        //Get itemUI/Image game object
        itemGroup_child03_itemUI = itemGroup_child03_itemUI.transform.GetChild(2).gameObject;
        //Get itemUI/Image game object/Item 1
        itemGroup_child03_itemUI = itemGroup_child03_itemUI.transform.GetChild(0).gameObject;
        //Setting Item Image
        //string itemImage = itemIteration.displaySpritePath;
        itemGroup_child03_itemUI.GetComponent<Text>().text = "+ " + itemIteration.vitalityBuff.ToString() + " Vitality";

        //Get itemUI game object 3RD OBJECT and 2nd SUB-OBJECT
        itemGroup_child03_itemUI = itemGroup.transform.GetChild(0).gameObject;
        //Get itemUI/Image game object
        itemGroup_child03_itemUI = itemGroup_child03_itemUI.transform.GetChild(2).gameObject;
        //Get itemUI/Image game object/Item 1
        itemGroup_child03_itemUI = itemGroup_child03_itemUI.transform.GetChild(1).gameObject;
        //Setting Item Image
        //string itemImage = itemIteration.displaySpritePath;
        itemGroup_child03_itemUI.GetComponent<Text>().text = "+ " + itemIteration.physicaldmgBuff.ToString() + " Power";

        //Get itemUI game object 3RD OBJECT and 3rd SUB-OBJECT
        itemGroup_child03_itemUI = itemGroup.transform.GetChild(0).gameObject;
        //Get itemUI/Image game object
        itemGroup_child03_itemUI = itemGroup_child03_itemUI.transform.GetChild(2).gameObject;
        //Get itemUI/Image game object/Item 1
        itemGroup_child03_itemUI = itemGroup_child03_itemUI.transform.GetChild(2).gameObject;
        //Setting Item Image
        //string itemImage = itemIteration.displaySpritePath;
        itemGroup_child03_itemUI.GetComponent<Text>().text = "+ " + itemIteration.magicdmgBuff.ToString() + " Intelligence";

        //Get itemUI game object 4TH OBJECT
        itemGroup_child04_itemUI = itemGroup.transform.GetChild(0).gameObject;
        //Get itemUI/Image game object
        itemGroup_child04_itemUI = itemGroup_child04_itemUI.transform.GetChild(3).gameObject;
        //Setting Item Image
        //string itemImage = itemIteration.displaySpritePath;
        itemGroup_child04_itemUI.GetComponent<Text>().text = itemIteration.cost + " Gold";

        itemGroup_child05_itemUI = itemGroup.transform.GetChild(0).gameObject;
        itemGroup_child05_itemUI = itemGroup_child05_itemUI.transform.GetChild(4).gameObject;
        if (shopState == Shop.Buy)
        {
            itemGroup_child05_itemUI.name = itemIteration.itemId + "_BuyButton";
        }
        else if (shopState == Shop.Sell)
        {
            itemGroup_child05_itemUI.name = itemIteration.itemId + "_SellButton";
        }
        //Debug.Log("this is the child buy button : " + itemGroup_child05_itemUI);
        itemGroup_child05_itemUI = itemGroup_child05_itemUI.transform.GetChild(0).gameObject;

        //Debug.Log("this is higher power : " + takingFromHigerPower.shop_state);
        //if (takingFromHigerPower.shop_state == Shop.Buy)
        //{
        //    itemGroup_child05_itemUI.GetComponent<Text>().text = "Buy";
        //}
        //else if (takingFromHigerPower.shop_state == Shop.Sell)
        //{
        //    itemGroup_child05_itemUI.GetComponent<Text>().text = "Sell";
        //}
        //Debug.Log("this is higher power : " + shopState);
        if (shopState == Shop.Buy)
        {
            itemGroup_child05_itemUI.GetComponent<Text>().text = "Buy";
        }
        else if (shopState == Shop.Sell)
        {
            itemGroup_child05_itemUI.GetComponent<Text>().text = "Sell";
        }

        ////Get itemUI game object 5TH OBJECT and 2ND SUB-OBJECT
        //itemGroup_child03_itemUI = itemGroup.transform.GetChild(0).gameObject;
        ////Get itemUI/Image game object
        //itemGroup_child03_itemUI = itemGroup_child01_itemUI.transform.GetChild(4).gameObject;
        ////Get itemUI/Image game object/Item 1
        //itemGroup_child03_itemUI = itemGroup_child01_itemUI.transform.GetChild(1).gameObject;
        ////Setting Item Image
        ////string itemImage = itemIteration.displaySpritePath;
        //itemGroup_child03_itemUI.GetComponent<Text>().text = "+ " + itemIteration.magicdmgBuff.ToString() + " Intelligence";

        GameObject passOn;

        //Instantiation of itemGroup object
        //itemGroup.name = itemIteration.itemId;
        itemGroup.name = itemIteration.itemId + "_Group";

        passOn = itemGroupToBeCreated = Instantiate(itemGroup, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        itemGroupToBeCreated.transform.SetParent(GameObject.FindGameObjectWithTag("ShopList").transform, false);

        return passOn;
    }
}
