using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Inventory
{
	item, skills, stats, itemEquipment, itemItems
}

namespace pattayaA3
{
	public class inventorybox : MonoBehaviour
	{
		[SerializeField] DataManager _dm;

		//Text
		[SerializeField] Text charactername;
		[SerializeField] Text itemchoice;
		[SerializeField] Text skillchoice;
		[SerializeField] Text statchoice;
		[SerializeField] Text menuchoice;
		[SerializeField] Text equipchoice;
		[SerializeField] Text equipitemchoice;
		//Item text
		[SerializeField] Text item1;
		[SerializeField] Text item2;
		[SerializeField] Text item3;
		//Skill text
		[SerializeField] Text skill1;
		[SerializeField] Text skill2;
		[SerializeField] Text skill3;
		[SerializeField] Text skill4;
		//Stat text
		[SerializeField] Text stat1;
		[SerializeField] Text stat2;
		[SerializeField] Text stat3;
		[SerializeField] Text stat4;

        #region AttributeStuff
        //Attribute Stuff
        [SerializeField] GameObject confirm_top;
        [SerializeField] Text confirm_text;

        [SerializeField] Text added_stat_text;
        [SerializeField] Text added_point_text;

        [SerializeField] GameObject add_vitality_button;
        [SerializeField] Text add_vitality_text;
        [SerializeField] GameObject subtract_vitality_button;
        [SerializeField] Text subtract_vitality_text;

        [SerializeField] GameObject add_power_button;
        [SerializeField] Text add_power_text;
        [SerializeField] GameObject subtract_power_button;
        [SerializeField] Text subtract_power_text;

        [SerializeField] GameObject add_intelligence_button;
        [SerializeField] Text add_intelligence_text;
        [SerializeField] GameObject subtract_intelligence_button;
        [SerializeField] Text subtract_intelligence_text;

        [SerializeField] GameObject add_attspeed_button;
        [SerializeField] Text add_attspeed_text;
        [SerializeField] GameObject subtract_attspeed_button;
        [SerializeField] Text subtract_attspeed_text;

        #endregion
        Inventory currentmenu = Inventory.item;
		//Objects
		[SerializeField] GameObject character;
		[SerializeField] GameObject charactersprite;
		[SerializeField] GameObject itemObject;
		[SerializeField] GameObject skillchoiceObject;
		[SerializeField] GameObject statchoiceObject;
		[SerializeField] GameObject menuchoiceObject;
		[SerializeField] GameObject equipchoiceObject;
		[SerializeField] GameObject equipitemchoiceObject;
		[SerializeField] GameObject inventoryBaseObject;
		[SerializeField] GameObject inventoryInnerObject;
		[SerializeField] GameObject scroll;
		//Item object
		[SerializeField] GameObject item1Object;
		[SerializeField] GameObject item2Object;
		[SerializeField] GameObject item3Object;
		//Skill object
		[SerializeField] GameObject skill1Object;
		[SerializeField] GameObject skill2Object;
		[SerializeField] GameObject skill3Object;
		[SerializeField] GameObject skill4Object;
		//Stat object
		[SerializeField] GameObject stat1Object;
		[SerializeField] GameObject stat2Object;
		[SerializeField] GameObject stat3Object;
		[SerializeField] GameObject stat4Object;

		Inventory currentmenu = Inventory.item;

        public Transform itemListing;

        string CheckItemInventory;
        save mainsessions;

        public GameObject eachItemObject;
        public EachItem eachItem;
        public Invent invent;
        public bool runonce;
        public bool confirmation;

        private void Start()
        {
            Game.ProcessSaveData();
            Game.GetSave();


            SetInventoryText();

            eachItem = eachItemObject.GetComponent<EachItem>();

            CheckItemInventory = Game.mainsessionData.inventory;
            CheckMenu();
        }
        private void Update()
		{
            //CheckMenu();
			//UpdateEquipment();
            ConstantlyCheck();
			//Debug.Log("Session Weapon:" + Game.Getitemsbyid(Game.GetitemsbyName(mainsession.weapon).itemId).displayName);
			//Debug.Log("Inventory: " + mainsession.inventory);
            if (runonce == false)
            {
                CheckMenu();
            }
		}
		public void SetInventoryText()
        {
            //save Game.mainsessionData = Game.mainsessionData;
            List<string> ListOfSkillsGroup = GetListOfSkillsPartOne(Game.mainsessionData.actorType);
            List<string> ListOfSkillsSeperated = GetRealSkillStringList(ListOfSkillsGroup);

            charactername.text = Game.mainsessionData.seshname;
            //itemchoice.text = Game.mainsessionData.seshname;
            //skillchoice.text = Game.mainsessionData.seshname;
            //statchoice.text = Game.mainsessionData.seshname;
            equipchoice.text = "Equipment";
            equipitemchoice.text = "Items";
            Game.mainsessionData.vitality_added += Game.mainsessionData.vitality;
            Game.mainsessionData.power_added += Game.mainsessionData.power;
            Game.mainsessionData.intelligence_added += Game.mainsessionData.intelligence;
            Game.mainsessionData.attspeed_added += Game.mainsessionData.attspeed;

            item1.text = Game.Getitemsbyid(Game.mainsessionData.weapon).displayName;
            item2.text = Game.Getitemsbyid(Game.mainsessionData.helmet).displayName;
            item3.text = Game.Getitemsbyid(Game.mainsessionData.armour).displayName;

			skill1.text = Game.GetSkillById(ListOfSkillsGroup[0]).skillname;
			skill2.text = Game.GetSkillById(ListOfSkillsGroup[1]).skillname;
			skill3.text = Game.GetSkillById(ListOfSkillsGroup[2]).skillname;
			skill4.text = Game.GetSkillById(ListOfSkillsGroup[3]).skillname;

            stat1.text = "Vitality: " + Game.mainsessionData.vitality_added;
            stat2.text = "Power: " + Game.mainsessionData.power_added;
            stat3.text = "Intelligence: " + Game.mainsessionData.intelligence_added;
            stat4.text = "Attack Speed: " + Game.mainsessionData.attspeed_added;

            //added_stat_text.text = "" + Game.mainsessionData.attributePoint;
            added_stat_text.text = "TEst";

			switch (currentmenu)
			{
				case Inventory.item:
					menuchoice.text = "Item";
					break;
				case Inventory.skills:
					menuchoice.text = "Skills";
					break;
				case Inventory.stats:
					menuchoice.text = "Stats";
					break;
				default:
					menuchoice.text = "Item";
					break;
			}
		}

        public void UpdateSprite(string path)
        {
            AssetManager.LoadSprite(path, (Sprite s) =>
            {
                Debug.Log(path);
                this.GetComponent<Image>().sprite = s;
            });
        }
        //public void EnableInventoryMenu(bool enabled)
        //{
        //    //Text
        //    charactername.enabled = enabled;
        //    menuchoice.enabled = enabled;
        //    itemchoice.enabled = enabled;
        //    skillchoice.enabled = enabled;
        //    statchoice.enabled = enabled;
        //    equipchoice.enabled = !enabled;
        //    equipitemchoice.enabled = !enabled;
        //    //Item boxes
        //    item1.enabled = enabled;
        //    item2.enabled = enabled;
        //    item3.enabled = enabled;
        //    //Skill boxes
        //    skill1.enabled = enabled;
        //    skill2.enabled = enabled;
        //    skill3.enabled = enabled;
        //    skill4.enabled = enabled;
        //    //Stat boxes
        //    stat1.enabled = enabled;
        //    stat2.enabled = enabled;
        //    stat3.enabled = enabled;
        //    stat4.enabled = enabled;

        //    //Objects
        //    character.SetActive(enabled);
        //    charactersprite.SetActive(enabled);
        //    itemObject.SetActive(enabled);
        //    skillchoiceObject.SetActive(enabled);
        //    statchoiceObject.SetActive(enabled);
        //    menuchoiceObject.SetActive(enabled);
        //    scroll.SetActive(!enabled);
        //    //Item object
        //    item1Object.SetActive(enabled);
        //    item2Object.SetActive(enabled);
        //    item3Object.SetActive(enabled);
        //    //Skill object
        //    skill1Object.SetActive(enabled);
        //    skill2Object.SetActive(enabled);
        //    skill3Object.SetActive(enabled);
        //    skill4Object.SetActive(enabled);
        //    //Stat object
        //    stat1Object.SetActive(enabled);
        //    stat2Object.SetActive(enabled);
        //    stat3Object.SetActive(enabled);
        //    stat4Object.SetActive(enabled);
        //}
        public void EnableItemMenu(bool enabled)
        {
            //Text
            charactername.enabled = enabled;
            menuchoice.enabled = enabled;
            itemchoice.enabled = enabled;
            skillchoice.enabled = enabled;
            statchoice.enabled = enabled;
            equipchoice.enabled = !enabled;
            equipitemchoice.enabled = !enabled;
            //Item boxes
            item1.enabled = enabled;
            item2.enabled = enabled;
            item3.enabled = enabled;
            //Skill boxes
            skill1.enabled = !enabled;
            skill2.enabled = !enabled;
            skill3.enabled = !enabled;
            skill4.enabled = !enabled;
            //Stat boxes
            stat1.enabled = !enabled;
            stat2.enabled = !enabled;
            stat3.enabled = !enabled;
            stat4.enabled = !enabled;

            //Objects
            character.SetActive(enabled);
            charactersprite.SetActive(enabled);
            itemObject.SetActive(enabled);
            skillchoiceObject.SetActive(enabled);
            statchoiceObject.SetActive(enabled);
            menuchoiceObject.SetActive(enabled);
            equipchoiceObject.SetActive(!enabled);
            equipitemchoiceObject.SetActive(!enabled);
            inventoryBaseObject.SetActive(!enabled);
            inventoryInnerObject.SetActive(!enabled);
            scroll.SetActive(!enabled);
            //Item object
            item1Object.SetActive(enabled);
            item2Object.SetActive(enabled);
            item3Object.SetActive(enabled);
            //Skill object
            skill1Object.SetActive(!enabled);
            skill2Object.SetActive(!enabled);
            skill3Object.SetActive(!enabled);
            skill4Object.SetActive(!enabled);
            //Stat object
            stat1Object.SetActive(!enabled);
            stat2Object.SetActive(!enabled);
            stat3Object.SetActive(!enabled);
            stat4Object.SetActive(!enabled);

            //Attribute
            confirm_top.SetActive(!enabled);
            confirm_text.enabled = !enabled;

            added_point_text.enabled = !enabled;
            added_stat_text.enabled = !enabled;

            add_vitality_button.SetActive(!enabled);
            add_vitality_text.enabled = !enabled;

            add_power_button.SetActive(!enabled);
            add_power_text.enabled = !enabled;

            add_intelligence_button.SetActive(!enabled);
            add_intelligence_text.enabled = !enabled;

            add_attspeed_button.SetActive(!enabled);
            add_attspeed_text.enabled = !enabled;

            subtract_vitality_button.SetActive(!enabled);
            subtract_vitality_text.enabled = !enabled;

            subtract_power_button.SetActive(!enabled);
            subtract_power_text.enabled = !enabled;

            subtract_intelligence_button.SetActive(!enabled);
            subtract_intelligence_text.enabled = !enabled;

            subtract_attspeed_button.SetActive(!enabled);
            subtract_attspeed_text.enabled = !enabled;

            runonce = false;
        }
        public void EnableSkillMenu(bool enabled)
        {
            //Text
            charactername.enabled = enabled;
            menuchoice.enabled = enabled;
            itemchoice.enabled = enabled;
            skillchoice.enabled = enabled;
            statchoice.enabled = enabled;
            equipchoice.enabled = !enabled;
            equipitemchoice.enabled = !enabled;
            //Item boxes
            item1.enabled = !enabled;
            item2.enabled = !enabled;
            item3.enabled = !enabled;
            //Skill boxes
            skill1.enabled = enabled;
            skill2.enabled = enabled;
            skill3.enabled = enabled;
            skill4.enabled = enabled;
            //Stat boxes
            stat1.enabled = !enabled;
            stat2.enabled = !enabled;
            stat3.enabled = !enabled;
            stat4.enabled = !enabled;

            //Objects
            character.SetActive(enabled);
            charactersprite.SetActive(enabled);
            itemObject.SetActive(enabled);
            skillchoiceObject.SetActive(enabled);
            statchoiceObject.SetActive(enabled);
            menuchoiceObject.SetActive(enabled);
            equipchoiceObject.SetActive(!enabled);
            equipitemchoiceObject.SetActive(!enabled);
            inventoryBaseObject.SetActive(!enabled);
            inventoryInnerObject.SetActive(!enabled);
            scroll.SetActive(!enabled);
            //Item object
            item1Object.SetActive(!enabled);
            item2Object.SetActive(!enabled);
            item3Object.SetActive(!enabled);
            //Skill object
            skill1Object.SetActive(enabled);
            skill2Object.SetActive(enabled);
            skill3Object.SetActive(enabled);
            skill4Object.SetActive(enabled);
            //Stat object
            stat1Object.SetActive(!enabled);
            stat2Object.SetActive(!enabled);
            stat3Object.SetActive(!enabled);
            stat4Object.SetActive(!enabled);

            //Attribute
            confirm_top.SetActive(!enabled);
            confirm_text.enabled = !enabled;

            added_point_text.enabled = !enabled;
            added_stat_text.enabled = !enabled;

            add_vitality_button.SetActive(!enabled);
            add_vitality_text.enabled = !enabled;

            add_power_button.SetActive(!enabled);
            add_power_text.enabled = !enabled;

            add_intelligence_button.SetActive(!enabled);
            add_intelligence_text.enabled = !enabled;

            add_attspeed_button.SetActive(!enabled);
            add_attspeed_text.enabled = !enabled;

            subtract_vitality_button.SetActive(!enabled);
            subtract_vitality_text.enabled = !enabled;

            subtract_power_button.SetActive(!enabled);
            subtract_power_text.enabled = !enabled;

            subtract_intelligence_button.SetActive(!enabled);
            subtract_intelligence_text.enabled = !enabled;

            subtract_attspeed_button.SetActive(!enabled);
            subtract_attspeed_text.enabled = !enabled;

            runonce = false;
        }
        public void EnableStatMenu(bool enabled)
        {
            //Text
            charactername.enabled = enabled;
            menuchoice.enabled = enabled;
            itemchoice.enabled = enabled;
            skillchoice.enabled = enabled;
            statchoice.enabled = enabled;
            equipchoice.enabled = !enabled;
            equipitemchoice.enabled = !enabled;
            //Item boxes
            item1.enabled = !enabled;
            item2.enabled = !enabled;
            item3.enabled = !enabled;
            //Skill boxes
            skill1.enabled = !enabled;
            skill2.enabled = !enabled;
            skill3.enabled = !enabled;
            skill4.enabled = !enabled;
            //Stat boxes
            stat1.enabled = enabled;
            stat2.enabled = enabled;
            stat3.enabled = enabled;
            stat4.enabled = enabled;

            //Objects
            character.SetActive(enabled);
            charactersprite.SetActive(enabled);
            itemObject.SetActive(enabled);
            skillchoiceObject.SetActive(enabled);
            statchoiceObject.SetActive(enabled);
            menuchoiceObject.SetActive(enabled);
            equipchoiceObject.SetActive(!enabled);
            equipitemchoiceObject.SetActive(!enabled);
            inventoryBaseObject.SetActive(!enabled);
            inventoryInnerObject.SetActive(!enabled);
            scroll.SetActive(!enabled);
            //Item object
            item1Object.SetActive(!enabled);
            item2Object.SetActive(!enabled);
            item3Object.SetActive(!enabled);
            //Skill object
            skill1Object.SetActive(!enabled);
            skill2Object.SetActive(!enabled);
            skill3Object.SetActive(!enabled);
            skill4Object.SetActive(!enabled);
            //Stat object
            stat1Object.SetActive(enabled);
            stat2Object.SetActive(enabled);
            stat3Object.SetActive(enabled);
            stat4Object.SetActive(enabled);

            //Attribute
            if (Game.mainsessionData.attributePoint > 0 && confirmation==false)
            {
                confirm_top.SetActive(enabled);
                confirm_text.enabled = enabled;

                add_vitality_button.SetActive(enabled);
                add_vitality_text.enabled = enabled;

                add_power_button.SetActive(enabled);
                add_power_text.enabled = enabled;

                add_intelligence_button.SetActive(enabled);
                add_intelligence_text.enabled = enabled;

                add_attspeed_button.SetActive(enabled);
                add_attspeed_text.enabled = enabled;

                subtract_vitality_button.SetActive(enabled);
                subtract_vitality_text.enabled = enabled;

                subtract_power_button.SetActive(enabled);
                subtract_power_text.enabled = enabled;

                subtract_intelligence_button.SetActive(enabled);
                subtract_intelligence_text.enabled = enabled;

                subtract_attspeed_button.SetActive(enabled);
                subtract_attspeed_text.enabled = enabled;
            }
            else if (Game.mainsessionData.attributePoint == 0 && confirmation == false)
            {
                confirm_top.SetActive(enabled);
                confirm_text.enabled = enabled;

                add_vitality_button.SetActive(!enabled);
                add_vitality_text.enabled = !enabled;

                add_power_button.SetActive(!enabled);
                add_power_text.enabled = !enabled;

                add_intelligence_button.SetActive(!enabled);
                add_intelligence_text.enabled = !enabled;

                add_attspeed_button.SetActive(!enabled);
                add_attspeed_text.enabled = !enabled;

                subtract_vitality_button.SetActive(enabled);
                subtract_vitality_text.enabled = enabled;

                subtract_power_button.SetActive(enabled);
                subtract_power_text.enabled = enabled;

                subtract_intelligence_button.SetActive(enabled);
                subtract_intelligence_text.enabled = enabled;

                subtract_attspeed_button.SetActive(enabled);
                subtract_attspeed_text.enabled = enabled;
            }
            else if (Game.mainsessionData.attributePoint == 0 && confirmation == true)
            {
                confirm_top.SetActive(!enabled);
                confirm_text.enabled = !enabled;

                add_vitality_button.SetActive(!enabled);
                add_vitality_text.enabled = !enabled;

                add_power_button.SetActive(!enabled);
                add_power_text.enabled = !enabled;

                add_intelligence_button.SetActive(!enabled);
                add_intelligence_text.enabled = !enabled;

                add_attspeed_button.SetActive(!enabled);
                add_attspeed_text.enabled = !enabled;

                subtract_vitality_button.SetActive(!enabled);
                subtract_vitality_text.enabled = !enabled;

                subtract_power_button.SetActive(!enabled);
                subtract_power_text.enabled = !enabled;

                subtract_intelligence_button.SetActive(!enabled);
                subtract_intelligence_text.enabled = !enabled;

                subtract_attspeed_button.SetActive(!enabled);
                subtract_attspeed_text.enabled = !enabled;
            }
            else if (Game.mainsessionData.attributePoint > 0 && confirmation == true)
            {
                confirm_top.SetActive(enabled);
                confirm_text.enabled = enabled;

                add_vitality_button.SetActive(enabled);
                add_vitality_text.enabled = enabled;

                add_power_button.SetActive(enabled);
                add_power_text.enabled = enabled;

                add_intelligence_button.SetActive(enabled);
                add_intelligence_text.enabled = enabled;

                add_attspeed_button.SetActive(enabled);
                add_attspeed_text.enabled = enabled;

                subtract_vitality_button.SetActive(enabled);
                subtract_vitality_text.enabled = enabled;

                subtract_power_button.SetActive(enabled);
                subtract_power_text.enabled = enabled;

                subtract_intelligence_button.SetActive(enabled);
                subtract_intelligence_text.enabled = enabled;

                subtract_attspeed_button.SetActive(enabled);
                subtract_attspeed_text.enabled = enabled;
            }
            else
            {
                //confirm_top.SetActive(!enabled);
                //confirm_text.enabled = !enabled;

                //add_vitality_button.SetActive(!enabled);
                //add_vitality_text.enabled = !enabled;

                //add_power_button.SetActive(!enabled);
                //add_power_text.enabled = !enabled;

                //add_intelligence_button.SetActive(!enabled);
                //add_intelligence_text.enabled = !enabled;

                //add_attspeed_button.SetActive(!enabled);
                //add_attspeed_text.enabled = !enabled;

                //subtract_vitality_button.SetActive(!enabled);
                //subtract_vitality_text.enabled = !enabled;

                //subtract_power_button.SetActive(!enabled);
                //subtract_power_text.enabled = !enabled;

                //subtract_intelligence_button.SetActive(!enabled);
                //subtract_intelligence_text.enabled = !enabled;

                //subtract_attspeed_button.SetActive(!enabled);
                //subtract_attspeed_text.enabled = !enabled;
            }

            added_point_text.enabled = enabled;
            added_stat_text.enabled = enabled;
            //Debug.Log(Game.mainsessionData.attributePoint);
            //stat1.text = "Vitality: " + Game.mainsessionData.vitality_added;
            //stat2.text = "Power: " + Game.mainsessionData.power_added;
            //stat3.text = "Intelligence: " + Game.mainsessionData.intelligence_added;
            //stat4.text = "Attack Speed: " + Game.mainsessionData.attspeed_added;

            stat1.text = "Vitality: " + Game.mainsessionData.vitality;
            stat2.text = "Power: " + Game.mainsessionData.power;
            stat3.text = "Intelligence: " + Game.mainsessionData.intelligence;
            stat4.text = "Attack Speed: " + Game.mainsessionData.attspeed;
            added_stat_text.text = "" + Game.mainsessionData.attributePoint;
            //added_stat_text.enabled=enabled;

            

            runonce = false;
        }
        public void EnableEquipmentMenu(bool enabled)
        {
            //Text
            charactername.enabled = enabled;
            menuchoice.enabled = !enabled;
            itemchoice.enabled = enabled;
            skillchoice.enabled = enabled;
            statchoice.enabled = enabled;
            equipchoice.enabled = enabled;
            equipitemchoice.enabled = enabled;
            //Item boxes
            item1.enabled = enabled;
            item2.enabled = enabled;
            item3.enabled = enabled;
            //Skill boxes
            skill1.enabled = !enabled;
            skill2.enabled = !enabled;
            skill3.enabled = !enabled;
            skill4.enabled = !enabled;
            //Stat boxes
            stat1.enabled = !enabled;
            stat2.enabled = !enabled;
            stat3.enabled = !enabled;
            stat4.enabled = !enabled;

            //Objects
            character.SetActive(enabled);
            charactersprite.SetActive(enabled);
            itemObject.SetActive(enabled);
            skillchoiceObject.SetActive(enabled);
            statchoiceObject.SetActive(enabled);
            menuchoiceObject.SetActive(!enabled);
            equipchoiceObject.SetActive(enabled);
            equipitemchoiceObject.SetActive(enabled);
            inventoryBaseObject.SetActive(!enabled);
            inventoryInnerObject.SetActive(!enabled);
            scroll.SetActive(!enabled);
            //Item object
            item1Object.SetActive(enabled);
            item2Object.SetActive(enabled);
            item3Object.SetActive(enabled);
            //Skill object
            skill1Object.SetActive(!enabled);
            skill2Object.SetActive(!enabled);
            skill3Object.SetActive(!enabled);
            skill4Object.SetActive(!enabled);
            //Stat object
            stat1Object.SetActive(!enabled);
            stat2Object.SetActive(!enabled);
            stat3Object.SetActive(!enabled);
            stat4Object.SetActive(!enabled);

            //Attribute
            confirm_top.SetActive(!enabled);
            confirm_text.enabled = !enabled;

            added_point_text.enabled = !enabled;
            added_stat_text.enabled = !enabled;

            add_vitality_button.SetActive(!enabled);
            add_vitality_text.enabled = !enabled;

            add_power_button.SetActive(!enabled);
            add_power_text.enabled = !enabled;

            add_intelligence_button.SetActive(!enabled);
            add_intelligence_text.enabled = !enabled;

            add_attspeed_button.SetActive(!enabled);
            add_attspeed_text.enabled = !enabled;
            subtract_vitality_button.SetActive(!enabled);
            subtract_vitality_text.enabled = !enabled;

            subtract_power_button.SetActive(!enabled);
            subtract_power_text.enabled = !enabled;

            subtract_intelligence_button.SetActive(!enabled);
            subtract_intelligence_text.enabled = !enabled;

            subtract_attspeed_button.SetActive(!enabled);
            subtract_attspeed_text.enabled = !enabled;

            runonce = false;
        }
        public void EnableEquipmentItemMenu(bool enabled)
        {
            //Text
            charactername.enabled = enabled;
            menuchoice.enabled = !enabled;
            itemchoice.enabled = enabled;
            skillchoice.enabled = enabled;
            statchoice.enabled = enabled;
            equipchoice.enabled = enabled;
            equipitemchoice.enabled = enabled;
            //Item boxes
            item1.enabled = !enabled;
            item2.enabled = !enabled;
            item3.enabled = !enabled;
            //Skill boxes
            skill1.enabled = !enabled;
            skill2.enabled = !enabled;
            skill3.enabled = !enabled;
            skill4.enabled = !enabled;
            //Stat boxes
            stat1.enabled = !enabled;
            stat2.enabled = !enabled;
            stat3.enabled = !enabled;
            stat4.enabled = !enabled;

            //Objects
            character.SetActive(enabled);
            charactersprite.SetActive(enabled);
            itemObject.SetActive(enabled);
            skillchoiceObject.SetActive(enabled);
            statchoiceObject.SetActive(enabled);
            menuchoiceObject.SetActive(!enabled);
            equipchoiceObject.SetActive(enabled);
            equipitemchoiceObject.SetActive(enabled);
            inventoryBaseObject.SetActive(enabled);
            inventoryInnerObject.SetActive(enabled);
            scroll.SetActive(enabled);
            //Item object
            item1Object.SetActive(!enabled);
            item2Object.SetActive(!enabled);
            item3Object.SetActive(!enabled);
            //Skill object
            skill1Object.SetActive(!enabled);
            skill2Object.SetActive(!enabled);
            skill3Object.SetActive(!enabled);
            skill4Object.SetActive(!enabled);
            //Stat object
            stat1Object.SetActive(!enabled);
            stat2Object.SetActive(!enabled);
            stat3Object.SetActive(!enabled);
            stat4Object.SetActive(!enabled);

            //Attribute
            confirm_top.SetActive(!enabled);
            confirm_text.enabled = !enabled;

            added_point_text.enabled = !enabled;
            added_stat_text.enabled = !enabled;

            add_vitality_button.SetActive(!enabled);
            add_vitality_text.enabled = !enabled;

            add_power_button.SetActive(!enabled);
            add_power_text.enabled = !enabled;

            add_intelligence_button.SetActive(!enabled);
            add_intelligence_text.enabled = !enabled;

            add_attspeed_button.SetActive(!enabled);
            add_attspeed_text.enabled = !enabled;

            subtract_vitality_button.SetActive(!enabled);
            subtract_vitality_text.enabled = !enabled;

            subtract_power_button.SetActive(!enabled);
            subtract_power_text.enabled = !enabled;

            subtract_intelligence_button.SetActive(!enabled);
            subtract_intelligence_text.enabled = !enabled;

            subtract_attspeed_button.SetActive(!enabled);
            subtract_attspeed_text.enabled = !enabled;

            Debug.Log("This is inventory "+Game.mainsessionData.inventory);
            //Debug.Log("This is inventory Count " + Game.GetItemsInInventory().Count);


            if (!runonce)
            {
                List<items> listinventory = Game.GetItemsInInventory();
                for (int i = 0; i < Game.GetItemsInInventory().Count; i++)
                {
                    Debug.Log("This is : " + listinventory[i].itemId + " With Sprite Path :" + listinventory[i].displaySpritePath);
                    eachItem.ActivateUI(listinventory[i]);

                    Game.ProcessSaveData();
                    Game.GetSave();
                    //Debug.Log(i);
                }
                //eachItem.ActivateUI(a);
                
                runonce = true;
            }
            //eachItem.ActivateUI();
        }

        public void UpdateItemList()
        {
            //List<items> listinventory = Game.GetItemsInInventory();
            //for (int i = 0; i < Game.GetItemsInInventory().Count; i++)
            //{
            //    Debug.Log("This is : " + listinventory[i].itemId + " With Sprite Path :" + listinventory[i].displaySpritePath);
            //    eachItem.ActivateUI(listinventory[i]);

            //    Game.ProcessSaveData();
            //    Game.GetSave();
            //}
            Debug.Log("test");
            runonce=false;
        }

        #region Button Presses
        public void ItemPress()
        {
            currentmenu = Inventory.itemEquipment;
            CheckMenu();
        }
        public void SkillPress()
        {
            currentmenu = Inventory.skills;
            CheckMenu();
        }
        public void StatPress()
        {
            currentmenu = Inventory.stats;
            CheckMenu();
        }
        public void EquipmentPress()
        {
            currentmenu = Inventory.itemEquipment;
            CheckMenu();
        }
        public void EquipmentItemPress()
        {
            currentmenu = Inventory.itemItems;
            CheckMenu();
        }

        public void Add_Vitality()
        {
            if (Game.mainsessionData.attributePoint > 0)
            {
                Game.mainsessionData.attributePoint--;
                Game.mainsessionData.vitality++;
                //Game.mainsessionData.vitality = Game.mainsessionData.vitality_added;
                Game.SaveToJSON<save>(Game.saveList);

                //Debug.Log("This is Vitality: " + Game.mainsessionData.vitality);
                //Debug.Log("This is Vitality_added: " + Game.mainsessionData.vitality_added);
            }
        }
        public void Remove_Vitality()
        {
            if (Game.mainsessionData.vitality_added > 0)
            {
                Game.mainsessionData.attributePoint++;
                Game.mainsessionData.vitality--;
                //ame.mainsessionData.vitality = Game.mainsessionData.vitality_added;
                //Game.mainsessionData.vitality_added += Game.mainsessionData.vitality;
                Game.SaveToJSON<save>(Game.saveList);

                //Debug.Log("This is Vitality: " + Game.mainsessionData.vitality_added);
                //Debug.Log("This is Vitality_added: " + Game.mainsessionData.vitality_added);
            }
        }
        public void Add_Power()
        {
            if (Game.mainsessionData.attributePoint > 0)
            {
                Game.mainsessionData.attributePoint--;
                Game.mainsessionData.power++;
                Game.SaveToJSON<save>(Game.saveList);
            }
        }
        public void Remove_Power()
        {
            if (Game.mainsessionData.power_added > 0)
            {
                Game.mainsessionData.attributePoint++;
                Game.mainsessionData.power--;
                Game.SaveToJSON<save>(Game.saveList);
            }
        }
        public void Add_Intelligence()
        {
            if (Game.mainsessionData.attributePoint > 0)
            {
                Game.mainsessionData.attributePoint--;
                Game.mainsessionData.intelligence++;
                Game.SaveToJSON<save>(Game.saveList);
            }
        }
        public void Remove_Intelligence()
        {
            if (Game.mainsessionData.intelligence > 0)
            {
                Game.mainsessionData.attributePoint++;
                Game.mainsessionData.intelligence--;
                Game.SaveToJSON<save>(Game.saveList);
            }
        }
        public void Add_AttSpeed()
        {
            if (Game.mainsessionData.attributePoint > 0)
            {
                Game.mainsessionData.attributePoint--;
                Game.mainsessionData.attspeed++;
                Game.SaveToJSON<save>(Game.saveList);
            }
        }
        public void Remove_Add_AttSpeed()
        {
            if (Game.mainsessionData.attspeed > 0)
            {
                Game.mainsessionData.attributePoint++;
                Game.mainsessionData.attspeed--;
                Game.SaveToJSON<save>(Game.saveList);
            }
        }
        public void Confirmation()
        {

            confirmation = true;

            Debug.Log("Comfirmed");

            //Game.mainsessionData.attspeed += Game.mainsessionData.attspeed_added;
            Game.mainsessionData.attspeed_added += Game.mainsessionData.attspeed;
            //Game.mainsessionData.attspeed_added = 0;

            //Game.mainsessionData.power += Game.mainsessionData.power_added;
            Game.mainsessionData.power_added += Game.mainsessionData.power;
            //Game.mainsessionData.power_added = 0;

            //Game.mainsessionData.intelligence += Game.mainsessionData.intelligence_added;
            Game.mainsessionData.intelligence_added += Game.mainsessionData.intelligence;
            //Game.mainsessionData.intelligence_added = 0;

            //Game.mainsessionData.vitality += Game.mainsessionData.vitality_added;
            Game.mainsessionData.vitality_added += Game.mainsessionData.vitality;
            //Game.mainsessionData.vitality = 0;

            Game.SaveToJSON<save>(Game.saveList);
        }
        #endregion

        public void CheckMenu()
        {
            switch (currentmenu)
            {
                case Inventory.item:
                    //menuchoice.text = "Item";
                    //CheckMenu();
                    EnableEquipmentMenu(true);
                    DisableItemList();
                    //eachItem.DeActivateInvent();
                    break;
                case Inventory.skills:
                    //CheckMenu();
                    EnableSkillMenu(true);
                    menuchoice.text = "Skills";
                    DisableItemList();
                    //eachItem.DeActivateInvent();
                    break;
                case Inventory.stats:
                    //CheckMenu();
                    EnableStatMenu(true);
                    menuchoice.text = "Stats";
                    DisableItemList();
                    //eachItem.DeActivateInvent();
                    break;
                case Inventory.itemEquipment:
                    //CheckMenu();
                    EnableEquipmentMenu(true);
                    DisableItemList();
                    UpdateEquipment();
                    //eachItem.DeActivateInvent();
                    //.text = "Stats";
                    break;
                case Inventory.itemItems:
                    //CheckMenu();
                    EnableEquipmentItemMenu(true);

                    //menuchoice.text = "Stats";
                    break;
                default:
                    //CheckMenu();
                    EnableItemMenu(true);
                    //DisableItemList();
                    //eachItem.DeActivateInvent();
                    //menuchoice.text = "Item";
                    break;
            }
        }

        public void DisableItemList()
        {
            foreach (Transform child in itemListing)
            {
                //Debug.Log(child.gameObject.name);
                GameObject.Destroy(child.gameObject);

            }
        }

        

        public static List<string> GetListOfSkillsPartOne(string actorType)
        {
            //Debug.Log("Running List of LS function");
            string listingString = Game.GetSkillListByType(actorType);
            //Debug.Log(BLSstring);
            string[] listingArray = listingString.Split(','); // this will turn BLSstring into a list, now we turn this into a list of LearnableSkill
                                                              //List<LearnableSkill> ListOfLS = new List<LearnableSkill>();
            List<string> ListofStrings = new List<string>();
            foreach (var LS in listingArray)
            {
                //Split into MoveBase and Level
                //LearnableSkill newLS = new LearnableSkill(LS); // returning ID of move here and level has to be casted as int later
                //ListOfLS.Add(newLS);

				ListofStrings.Add(LS);
			}
			return ListofStrings;
		}
		public List<string> GetRealSkillStringList(List<string> stringList)
		{
			List<string> testString2 = new List<string>();
			foreach (var test in stringList)
			{
				//Debug.Log(test);
				string[] LSarray = test.Split('_');
				testString2.Add(LSarray[0]);
			}
			return testString2;
		}

        public void UpdateEquipment()
        {
            //Game.ProcessSaveData();
            //Game.GetSave();

            //List<string> ListOfSkillsGroup;
            //Debug.Log("Actor Type: " + currentmenu);
            //ListOfSkillsGroup = GetListOfSkillsPartOne(Game.mainsessionData.actorType);
            item1.text = Game.Getitemsbyid(Game.mainsessionData.weapon).displayName;
            item2.text = Game.Getitemsbyid(Game.mainsessionData.helmet).displayName;
            item3.text = Game.Getitemsbyid(Game.mainsessionData.armour).displayName;
            //item1.text = Game.GetGame.mainsessionData.weapon;
            //item2.text = Game.mainsessionData.helmet;
            //item3.text = Game.mainsessionData.armour;


            //skill1.text = Game.GetSkillById(ListOfSkillsGroup[0]).skillname;
            //skill2.text = Game.GetSkillById(ListOfSkillsGroup[1]).skillname;
            //skill3.text = Game.GetSkillById(ListOfSkillsGroup[2]).skillname;
            //skill4.text = Game.GetSkillById(ListOfSkillsGroup[3]).skillname;

            //stat1.text = "Vitality:" + Game.mainsessionData.vitality;
            //stat2.text = "Power:" + Game.mainsessionData.power;
            //stat3.text = "Intelligence:" + Game.mainsessionData.intelligence;
            //stat4.text = "Attack Speed:" + Game.mainsessionData.attspeed;


        }

        //public void ChangeWeapon()
        //{
        //    save Game.mainsessionData = Game.mainsessionData;
        //    Game.SetSessionWeaponVariable("item02");

        //}

        public void AddItem(string itemId)
        {
            save currensession = Game.mainsessionData;
            Game.AddItemToInventory(itemId);
            //eachItem.ActivateUI();
        }

        public void ConstantlyCheck()
        {
            if (!(CheckItemInventory == Game.mainsessionData.inventory))
            {
                Debug.Log("Test");
                DisableItemList();
                runonce = false;
                CheckItemInventory = Game.mainsessionData.inventory;
            }
        }
    }
}
