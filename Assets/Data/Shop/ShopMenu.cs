using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Shop
{
    Buy, Sell
}
public enum Shop_Items
{
    Weapon, Armour, Helmet
}

public class ShopMenu : MonoBehaviour
{
    //Buy button
    public Text buy_text;
    public GameObject buy_object;

    //Sell button
    public Text sell_text;
    public GameObject sell_object;

    //Weapon button
    public Text weapon_text;
    public GameObject weapon_object;

    //Armour button
    public Text armour_text;
    public GameObject armour_object;

    //Helmet button
    public Text helmet_text;
    public GameObject helmet_object;

    //Scroll Object
    public GameObject scroll;

    //State
    Shop shop_state = Shop.Buy;
    Shop_Items shop_items_state = Shop_Items.Weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyTab()
    {
        shop_state = Shop.Buy;
    }
    public void SellTab()
    {
        shop_state = Shop.Sell;
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
}
