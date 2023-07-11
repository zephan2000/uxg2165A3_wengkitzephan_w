using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Inventory
{
    item, skills, stats
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

        session mainsession = Game.GetSession();

        public void SetInventoryText()
        {
            session currentsession = Game.GetSession();
            List<string> ListOfSkillsGroup = GetListOfSkillsPartOne(currentsession.actorType);
            List<string> ListOfSkillsSeperated = GetRealSkillStringList(ListOfSkillsGroup);

            charactername.text = currentsession.seshname;
            //itemchoice.text = currentsession.seshname;
            //skillchoice.text = currentsession.seshname;
            //statchoice.text = currentsession.seshname;

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
        public void EnableInventoryMenu(bool enabled)
        {
            //Text
            charactername.enabled = enabled;
            menuchoice.enabled = enabled;
            itemchoice.enabled = enabled;
            skillchoice.enabled = enabled;
            statchoice.enabled = enabled;
            //Item boxes
            item1.enabled = enabled;
            item2.enabled = enabled;
            item3.enabled = enabled;
            //Skill boxes
            skill1.enabled = enabled;
            skill2.enabled = enabled;
            skill3.enabled = enabled;
            skill4.enabled = enabled;
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
            //Item object
            item1Object.SetActive(enabled);
            item2Object.SetActive(enabled);
            item3Object.SetActive(enabled);
            //Skill object
            skill1Object.SetActive(enabled);
            skill2Object.SetActive(enabled);
            skill3Object.SetActive(enabled);
            skill4Object.SetActive(enabled);
            //Stat object
            stat1Object.SetActive(enabled);
            stat2Object.SetActive(enabled);
            stat3Object.SetActive(enabled);
            stat4Object.SetActive(enabled);
        }
        public void EnableItemMenu(bool enabled)
        {
            //Text
            charactername.enabled = enabled;
            menuchoice.enabled = enabled;
            itemchoice.enabled = enabled;
            skillchoice.enabled = enabled;
            statchoice.enabled = enabled;
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
        }
        public void EnableSkillMenu(bool enabled)
        {
            //Text
            charactername.enabled = enabled;
            menuchoice.enabled = enabled;
            itemchoice.enabled = enabled;
            skillchoice.enabled = enabled;
            statchoice.enabled = enabled;
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
        }
        public void EnableStatMenu(bool enabled)
        {
            //Text
            charactername.enabled = enabled;
            menuchoice.enabled = enabled;
            itemchoice.enabled = enabled;
            skillchoice.enabled = enabled;
            statchoice.enabled = enabled;
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
        }

        public void ItemPress()
        {
            currentmenu = Inventory.item;
        }
        public void SkillPress()
        {
            currentmenu = Inventory.skills;
        }
        public void StatPress()
        {
            currentmenu = Inventory.stats;
        }

        public void CheckMenu()
        {
            switch (currentmenu)
            {
                case Inventory.item:
                    menuchoice.text = "Item";
                    EnableItemMenu(true);
                    break;
                case Inventory.skills:
                    EnableSkillMenu(true);
                    menuchoice.text = "Skills";
                    break;
                case Inventory.stats:
                    EnableStatMenu(true);
                    menuchoice.text = "Stats";
                    break;
                default:
                    EnableItemMenu(true);
                    menuchoice.text = "Item";
                    break;
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

        public void UpdateEquiptment()
        {
            session currentsession = Game.GetSession();
            List<string> ListOfSkillsGroup = GetListOfSkillsPartOne(currentsession.actorType);
            item1.text = Game.GetitemsbyName(currentsession.weapon).displayName;
            item2.text = Game.GetitemsbyName(currentsession.helmet).displayName;
            item3.text = Game.GetitemsbyName(currentsession.armour).displayName;
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
            session currentsession = Game.GetSession();
            Game.SetSessionWeaponVariable("item02");

        }
    }
}
