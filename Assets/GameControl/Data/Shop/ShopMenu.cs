using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public enum Shop
{
    Buy, Sell
}
public enum Shop_Items
{
    Weapon, Armour, Helmet, Inventory
}

public class ShopMenu : MonoBehaviour
{
    ////Weapon Item List
    //public List<items> WeaponList;
    ////Weapon Item List
    //public List<items> ArmourList;
    ////Weapon Item List
    //public List<items> HelmetList;
    //ItemShopList
    public List<items> ShopList;

    //Buy button
    public Text buy_text;
    public GameObject buy_object;

    //Sell button
    public Text sell_text;
    public GameObject sell_object;

    public Transform itemListing;

    ////Weapon button
    //public Text weapon_text;
    //public GameObject weapon_object;

    ////Armour button
    //public Text armour_text;
    //public GameObject armour_object;

    ////Helmet button
    //public Text helmet_text;
    //public GameObject helmet_object;

    //Gold
    public Text gold;

    //Shopitem
    public ShopItem shopItem; 
    public GameObject shopObject;

    string CheckItemInventory;

    //Scroll Object
    [SerializeField] public GameObject scroll;

    //State
    public Shop shop_state;
    public Shop_Items shop_items_state;

    //Runonce
    public bool runonce;

    // Start is called before the first frame update
    void Start()
    {
        //Game.ProcessSaveData(Game.demoData2);
        Game.ProcessSaveData();
        Game.GetSave();

        SetInventory();
        CheckMenu();
        CheckItemInventory = Game.mainsessionData.inventory;
        shopItem = shopObject.GetComponent<ShopItem>();
        //CheckMenu();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("This is shop state : "+shop_state);
        ConstantlyCheck();
        if (runonce == false)
        {
            CheckMenu();
        }
        //CheckMenu();
        //Debug.Log("this is higher power : " + shopState);
    }

    public void CheckMenu()
    {
        //DisableItemList();
        switch (shop_state)
        {
            case Shop.Buy:
                EnableBuy();
                //Debug.Log("nice");
                //DisableItemList();
                break;
            case Shop.Sell:
                EnableSell();
                //Debug.Log("mom");
                //EnableSkillMenu(true);
                //menuchoice.text = "Skills";
                //DisableItemList();
                break;
            default:
                //EnableItemMenu(true);
                break;
        }
    }

    public void SetInventory()
    {
        gold.text = "Gold : " + Game.mainsessionData.gold;
        shopItem = shopObject.GetComponent<ShopItem>();
        shop_state = Shop.Buy;

        ShopList = Game.GetItemList();
        //WeaponList = Game.GetitemsbyitemType("weapon");
        //ArmourList = Game.GetitemsbyitemType("armour");
        //HelmetList = Game.GetitemsbyitemType("helmet");
    }

    public void DisableItemList()
    {
        foreach (Transform child in itemListing)
        {
            //Debug.Log(child.gameObject.name);
            GameObject.Destroy(child.gameObject);

        }
    }

    public void ChangeRunOnceToFalse()
    {
        runonce = false;
    }

    public void EnableBuy()
    {
        DisableItemList();
        scroll.SetActive(enabled);
        if (!runonce)
        {
            ShopList = Game.GetItemList();
            List<items> listinventory = ShopList;
            for (int i = 0; i < listinventory.Count; i++)
            {
                //ShopList = Game.GetItemList();
                //Debug.Log("This is Sparta : " + listinventory[i].itemId + " With Sprite Path :" + listinventory[i].displaySpritePath);
                shopItem.ActivateUI(listinventory[i], shop_state);

                //Game.ProcessSaveData(Game.demoData2);
                Game.ProcessSaveData();
                Game.GetSave();
                //Debug.Log(i);
            }
            //eachItem.ActivateUI(a);

            runonce = true;
        }
    }
    public void EnableSell()
    {
        DisableItemList();
        scroll.SetActive(enabled);
        if (!(Game.GetItemsInInventory() == null))
        {
            if (!runonce)
            {
                ShopList = Game.GetItemsInInventory();
                List<items> listinventory = ShopList;
                for (int i = 0; i < listinventory.Count; i++)
                {
                    //hopList = Game.GetItemsInInventory();
                    //Debug.Log("This is Sparta : " + listinventory[i].itemId + " With Sprite Path :" + listinventory[i].displaySpritePath);
                    shopItem.ActivateUI(listinventory[i], shop_state);
                    //Debug.Log("This is ramadan : ");
                    //Game.ProcessSaveData(Game.demoData2);
                    Game.ProcessSaveData();
                    Game.GetSave();
                    //Debug.Log(i);
                }
                //eachItem.ActivateUI(a);

                runonce = true;
            }
        }
    }
    public void ConstantlyCheck()
    {
        if (!(CheckItemInventory == Game.mainsessionData.inventory))
        {
            //Debug.Log("lmao");
            DisableItemList();
            runonce = false;
            //Debug.Log("lmao");
            CheckItemInventory = Game.mainsessionData.inventory;
            gold.text = "Gold : " + Game.mainsessionData.gold;
        }
    }

    public void BuyTab()
    {
        shop_state = Shop.Buy;
        shopItem.takingFromHigerPower.shop_state = Shop.Buy;
        ChangeRunOnceToFalse();
    }
    public void SellTab()
    {
        shop_state = Shop.Sell;
        shopItem.takingFromHigerPower.shop_state = Shop.Sell;
        ChangeRunOnceToFalse();
    }
    public void WeaponTab()
    {
        shop_items_state = Shop_Items.Weapon;
    }
    public void ArmourTab()
    {
        shop_items_state = Shop_Items.Armour;
    }
    public void HelmetTab()
    {
        shop_items_state = Shop_Items.Helmet;
    }

    public void InventoryTab()
    {
        shop_items_state = Shop_Items.Inventory;
    }


}
