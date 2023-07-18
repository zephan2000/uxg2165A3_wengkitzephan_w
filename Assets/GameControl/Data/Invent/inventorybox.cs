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

        //Skills
        List<skills> skillList = new List<skills>();

        public Transform itemListing;

        string CheckItemInventory;
        save mainsession = Game.mainsessionData;

        public GameObject eachItemObject;
        public EachItem eachItem;
        public Invent invent;
        public bool runonce;

        private void Start()
        {
            SetInventoryText();

            eachItem = eachItemObject.GetComponent<EachItem>();

            CheckItemInventory = Game.mainsessionData.inventory;
        }
        private void Update()
		{
            CheckMenu();
			UpdateEquipment();
            ConstantlyCheck();
			//Debug.Log("Session Weapon:" + Game.Getitemsbyid(Game.GetitemsbyName(mainsession.weapon).itemId).displayName);
			//Debug.Log("Inventory: " + mainsession.inventory);
		}
		public void SetInventoryText()
        {
            save currentsession = Game.mainsessionData;
            List<string> ListOfSkillsGroup = GetListOfSkillsPartOne(currentsession.actorType);
            List<string> ListOfSkillsSeperated = GetRealSkillStringList(ListOfSkillsGroup);

            charactername.text = currentsession.seshname;
            //itemchoice.text = currentsession.seshname;
            //skillchoice.text = currentsession.seshname;
            //statchoice.text = currentsession.seshname;
            equipchoice.text = "Equipment";
            equipitemchoice.text = "Items";

            item1.text = Game.Getitemsbyid(currentsession.weapon).displayName;
            item2.text = Game.Getitemsbyid(currentsession.helmet).displayName;
            item3.text = Game.Getitemsbyid(currentsession.armour).displayName;

            skill1.text = Game.GetSkillById(ListOfSkillsGroup[0]).skillname;
            skill2.text = Game.GetSkillById(ListOfSkillsGroup[1]).skillname;
            skill3.text = Game.GetSkillById(ListOfSkillsGroup[2]).skillname;
            skill4.text = Game.GetSkillById(ListOfSkillsGroup[3]).skillname;

            stat1.text = "Vitality:" + currentsession.vitality;
            stat2.text = "Power:" + currentsession.power;
            stat3.text = "Intelligence:" + currentsession.intelligence;
            stat4.text = "Attack Speed:" + currentsession.attspeed;

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

            if (!runonce)
            {
                for (int i = 0; i < Game.GetItemsInInventory().Count; i++)
                {
                    eachItem.ActivateUI(Game.GetItemsInInventory()[i]);
                }
                //eachItem.ActivateUI(a);
                runonce = true;
            }
            //eachItem.ActivateUI();
        }

        public void ItemPress()
        {
            currentmenu = Inventory.itemEquipment;
        }
        public void SkillPress()
        {
            currentmenu = Inventory.skills;
        }
        public void StatPress()
        {
            currentmenu = Inventory.stats;
        }
        public void EquipmentPress()
        {
            currentmenu = Inventory.itemEquipment;
        }
        public void EquipmentItemPress()
        {
            currentmenu = Inventory.itemItems;
        }

        public void CheckMenu()
        {
            switch (currentmenu)
            {
                case Inventory.item:
                    //menuchoice.text = "Item";
                    EnableEquipmentMenu(true);
                    DisableItemList();
                    //eachItem.DeActivateInvent();
                    break;
                case Inventory.skills:
                    EnableSkillMenu(true);
                    menuchoice.text = "Skills";
                    DisableItemList();
                    //eachItem.DeActivateInvent();
                    break;
                case Inventory.stats:
                    EnableStatMenu(true);
                    menuchoice.text = "Stats";
                    DisableItemList();
                    //eachItem.DeActivateInvent();
                    break;
                case Inventory.itemEquipment:
                    EnableEquipmentMenu(true);
                    DisableItemList();
                    //eachItem.DeActivateInvent();
                    //.text = "Stats";
                    break;
                case Inventory.itemItems:
                    EnableEquipmentItemMenu(true);
                    //menuchoice.text = "Stats";
                    break;
                default:
                    EnableItemMenu(true);
                    DisableItemList();
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
            save currentsession = Game.mainsessionData;
            List<string> ListOfSkillsGroup;
            //Debug.Log("Actor Type: " + currentmenu);
            ListOfSkillsGroup = GetListOfSkillsPartOne(currentsession.actorType);
            item1.text = Game.Getitemsbyid(currentsession.weapon).displayName;
            item2.text = Game.Getitemsbyid(currentsession.helmet).displayName;
            item3.text = Game.Getitemsbyid(currentsession.armour).displayName;
            //item1.text = Game.Getcurrentsession.weapon;
            //item2.text = currentsession.helmet;
            //item3.text = currentsession.armour;


            skill1.text = Game.GetSkillById(ListOfSkillsGroup[0]).skillname;
            skill2.text = Game.GetSkillById(ListOfSkillsGroup[1]).skillname;
            skill3.text = Game.GetSkillById(ListOfSkillsGroup[2]).skillname;
            skill4.text = Game.GetSkillById(ListOfSkillsGroup[3]).skillname;

            stat1.text = "Vitality:" + currentsession.vitality;
            stat2.text = "Power:" + currentsession.power;
            stat3.text = "Intelligence:" + currentsession.intelligence;
            stat4.text = "Attack Speed:" + currentsession.attspeed;


        }

        public void ChangeWeapon()
        {
            save currentsession = Game.mainsessionData;
            Game.SetSessionWeaponVariable("item02");

        }

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
                DisableItemList();
                runonce = false;
                CheckItemInventory = Game.mainsessionData.inventory;
            }
        }
    }
}
