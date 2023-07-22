using pattayaA3;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenuController : MonoBehaviour
{
	public GameObject savePrefab;
	public GameObject saveMenu;
	List<GameObject> saveList = new List<GameObject>();
	List<SaveButton> saveButtons = new List<SaveButton>();
	List<GameObject> saveListButtons = new List<GameObject>();	
	Dictionary<GameObject, SaveButton> saveDictionary = new Dictionary<GameObject, SaveButton>();
	public StartMenuController startMenu;
	public void OpenMenu()
	{

		foreach (save saveData in Game.saveList)
		{
			GameObject saveId = null;
			GameObject saveDate = null;
			GameObject playerLevel = null;
			GameObject playerName = null;
			GameObject playerClass = null;
			GameObject saveInfo = null; 

			saveInfo = Instantiate(savePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			saveInfo.transform.SetParent(GameObject.FindGameObjectWithTag("SaveList").transform, false);

			Transform saveRoot = saveInfo.transform;
			foreach (Transform child in saveInfo.transform)
			{
				switch (child.gameObject.name)
				{
					case "saveId":
						saveId = child.gameObject;
						saveInfo.name = saveData.saveId;
						saveId.GetComponent<Text>().text = saveData.saveId;
						Debug.Log("saveId running");
						saveId.transform.SetParent(saveRoot, false);
						Debug.Log("saveId assign parent");
						break;
					case "SaveDate":
						saveDate = child.gameObject;
						saveDate.GetComponent<Text>().text = saveData.saveId;
						saveDate.transform.SetParent(saveRoot, false);//place holder
						break;
					case "playerLevel":
						playerLevel = child.gameObject;
						playerLevel.GetComponent<Text>().text = saveData.levelId;
						playerLevel.transform.SetParent(saveRoot, false);
						break;
					case "playerName":
						playerName = child.gameObject;
						playerName.GetComponent<Text>().text = saveData.seshname;
						playerName.transform.SetParent(saveRoot, false);
						break;
					case "playerClass":
						playerClass = child.gameObject;
						playerClass.GetComponent<Text>().text = saveData.actorType;
						playerClass.transform.SetParent(saveRoot, false);
						break;
				}
			}
			SaveButton saveButton = new SaveButton(saveId, saveDate, playerLevel, playerName, playerClass);
			saveInfo.GetComponent<Button>().onClick.AddListener(delegate { LoadSave(OnClicked(saveInfo)); });// need to add listener to each button, assign loadSaveFile function
			saveDictionary.Add(saveInfo, saveButton);
			saveList.Add(saveInfo);
			saveButtons.Add(saveButton);
		}
	}
	public string OnClicked(GameObject obj)
	{
		Debug.Log($"this is button's name {obj.gameObject.name}");
		return obj.gameObject.name;
	}

	public void LoadSave(string currentSaveId)
	{
		Debug.Log($"this is currentButton's name {currentSaveId}");
		foreach (KeyValuePair<GameObject,SaveButton> saveDictionaryObj in saveDictionary)
		{
			Debug.Log($"this is saveDictionaryobj's name {saveDictionaryObj.Key.transform.GetChild(0).GetComponent<Text>().text}");
			if (saveDictionaryObj.Key.transform.GetChild(0).GetComponent<Text>().text == currentSaveId)
			{
				Game.LoadSave(saveDictionaryObj.Value.saveId.GetComponent<Text>().text);
				break;
			}
		}
		CloseMenu();
		foreach (GameObject saveInfo in saveList)
		{
			GameObject.Destroy(saveInfo);
		}
		startMenu.StartLevel("Town");
	}
	// load Save file function, onClick check for 

	public void CloseMenu()
	{
		foreach (SaveButton saveButton in saveButtons)
		{
			GameObject.Destroy(saveButton.saveId);
			GameObject.Destroy(saveButton.saveDate);
			GameObject.Destroy(saveButton.playerLevel);
			GameObject.Destroy(saveButton.playerName);
			GameObject.Destroy(saveButton.playerClass);
		}
		foreach (GameObject saveInfo in saveList)
		{
			GameObject.Destroy(saveInfo);
		}
		
		saveList.Clear();
		saveDictionary.Clear();
		saveButtons.Clear();
		saveMenu.SetActive(false);
	}
}

public class SaveButton
{
	public GameObject saveId;
	public GameObject saveDate;
	public GameObject playerLevel;
	public GameObject playerName;
	public GameObject playerClass;

	public SaveButton (GameObject saveId, GameObject saveDate, GameObject playerLevel, GameObject playerName, GameObject playerClass)
	{
		this.saveId = saveId;
		this.saveDate = saveDate;
		this.playerLevel = playerLevel;
		this.playerName = playerName;
		this.playerClass = playerClass;
	}
}
