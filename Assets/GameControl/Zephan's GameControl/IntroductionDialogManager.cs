using pattayaA3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Zephan
public enum IntroDialogState { Typing, SelectingChoice, EndOfDialog, LastDialog}
public class IntroductionDialogManager : MonoBehaviour
{
	[SerializeField] GameObject dialogBox;
	[SerializeField] Text dialogText;
	[SerializeField] GameObject SkipText;
	[SerializeField] GameObject NextText;
	[SerializeField] GameObject NameText;
	[SerializeField] GameObject EndText;
	[SerializeField] GameObject ChoiceText;
	[SerializeField] GameObject ChoiceSelector;
	[SerializeField] List<GameObject> choiceButtons;
	[SerializeField] GameObject leftSpeaker;
	[SerializeField] GameObject rightSpeaker;
	public event Action OnShowDialog;
	public event Action OnCloseDialog;
	//public static TownDialogManager Instance {  get; private set; }
	private bool Skip;
	private Dialog currentDialog;
	[SerializeField] StartMenuController startMenuController;
	//private void Awake()
	//{
	//	Instance = this; 
	//}
	IntroDialogState dialogState;
	List<Dialog> dialogList;
	List<Dialog> dialogChoiceList;
	int currentChoice;
	private string chosenClassText;
	private bool canUpdateClass;
	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) && dialogState == IntroDialogState.EndOfDialog)
		{
			currentDialog = Game.GetDialogByDialogList(currentDialog.nextdialogueId, dialogList); // assigning nextdialog to currentDialog from dialogList
			StartCoroutine(TypeDialog(currentDialog.dialogueText));
		}
		else if (dialogState == IntroDialogState.SelectingChoice)
		{
			Debug.Log("Handling Choice");
			HandleChoiceDialog();
		}
		else if (Input.GetKeyDown(KeyCode.Space) && dialogState == IntroDialogState.Typing)
		{
			Skip = true;
		}
		else if (dialogState == IntroDialogState.LastDialog)
		{
			if(Input.GetKeyDown(KeyCode.Return))
			startMenuController.StartLevel("Town");
		}

	}
	public IEnumerator ShowDialog(string dialogueType)
	{
		yield return new WaitForEndOfFrame();
		OnShowDialog?.Invoke();
		//this.dialog = dialog; // change this line, need to read from a list of your own data
		dialogList = Game.GetDialogByType(dialogueType);
		currentDialog = dialogList[0];
		dialogBox.SetActive(true);
		AssetManager.LoadSprite(currentDialog.displaySpritePathLeft, (Sprite s) =>
		{
			leftSpeaker.GetComponent<Image>().sprite = s;
		});
		AssetManager.LoadSprite(currentDialog.displaySpritePathRight, (Sprite s) =>
		{
			rightSpeaker.GetComponent<Image>().sprite = s;
		});
		StartCoroutine(TypeDialog(dialogList[0].dialogueText));
	}

	public IEnumerator TypeDialog(string line) // animating dialog to reveal letter by letter
	{
		dialogState = IntroDialogState.Typing;
		dialogText.text = "";
		NameText.GetComponent<Text>().text = currentDialog.currentSpeakerName; 
		SkipText.SetActive(true);
		NextText.SetActive(false);
		EndText.SetActive(false);
		ChoiceText.SetActive(false);
		ChoiceSelector.SetActive(false);
		CheckForCurrentSpeaker(currentDialog);
		foreach (var letter in line.ToCharArray())
		{
			if (Skip) // skippable dialogue
			{
				dialogText.text = line;
				Skip = false;
				SkipText.SetActive(false);
				//yield return new WaitForSeconds(1.2f);
				NextText.SetActive(true);
				break;
			}
			else
			{
				dialogText.text += letter;
				yield return new WaitForSeconds(1f / 30);
			}
			
		}
		if (currentDialog.nextdialogueId == "-1")
		{
			LastDialogSettings();
		}
		else if (currentDialog.nextdialogueId == "-2")
		{
			ChoiceDialogSettings();
		}
		else
		{
			EndOfCurrentDialogSettings();
		}
	}

	public void CheckForCurrentSpeaker(Dialog currentDialog)
	{
		if(currentDialog.currentSpeakerName == "NPC")
		{
			rightSpeaker.GetComponent<Image>().color = Color.white;
			leftSpeaker.GetComponent<Image>().color = Color.grey;
		}
		else
		{
			leftSpeaker.GetComponent<Image>().color = Color.white;
			rightSpeaker.GetComponent<Image>().color = Color.grey;
		}
	}	
	
	void HandleChoiceDialog() //for vertical choice selector
	{
			
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (currentChoice < dialogChoiceList.Count)
				++currentChoice;
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (currentChoice > 0)
				--currentChoice;
		}
		UpdateChoiceSelection(currentChoice); 

		if (Input.GetKeyDown(KeyCode.Return))
		{
			canUpdateClass = true;
			var chosenDialog = dialogChoiceList[currentChoice];
			UpdateClassSelection(chosenDialog, canUpdateClass);
			currentChoice = 0;
			currentDialog = Game.GetDialogByDialogList(chosenDialog.dialogueId, dialogList);//setting to next dialog
			dialogState = IntroDialogState.EndOfDialog;// assigning nextdialog to currentDialog from dialogList
			StartCoroutine(TypeDialog(currentDialog.dialogueText));
			
		}
	}
	public IEnumerator CheckForReturnKey()
	{
		canUpdateClass = false;
		yield return new WaitForSeconds(0.2f);
		yield return Input.GetKeyDown(KeyCode.Return);
	}
	public void UpdateClassSelection(Dialog chosenDialog, bool canUpdateClass)
	{
		Debug.Log($"this is chosenDialog text from class selection {chosenDialog.dialogueText}");	
		if(canUpdateClass)
		{
			switch (chosenDialog.dialogueText)
			{
				case "I want to be a Warrior.":
					chosenClassText = "playerWarrior_1";
					break;
				case "I want to be a Magician.":
					chosenClassText = "playerWizard_1";
					break;
				case "I want to be an Archer.":
					chosenClassText = "playerArcher_1";
					break;
				case "Hmm... let me choose again":
					chosenClassText = "";
					break;
				case "Yes I'm sure.":
					Debug.Log($"this is chosenClassText {chosenClassText}");
					Game.StartNewGame(chosenClassText);
					break;
			}
		}
		 


	}
	public void UpdateChoiceSelection(int selectedChoice)
	{
		for (int i = 0; i < choiceButtons.Count; i++)
		{
			if (i == selectedChoice)
			{
				choiceButtons[i].GetComponent<Text>().color = Color.blue;
			}
			else
				choiceButtons[i].GetComponent<Text>().color = Color.black;
		}
		//dialog Text is handled in TypeDialog already
	}
	public void SetDialogChoice(List<Dialog> choices)
	{
		for (int i = 0; i < choiceButtons.Count; i++)
		{
			if (i < choices.Count)
			{
				choiceButtons[i].SetActive(true);
				//Debug.Log(choices[i].dialogueText);
				choiceButtons[i].GetComponent<Text>().text = choices[i].dialogueText;
				//Debug.Log(dialogState);
			}

			else
			{
				choiceButtons[i].SetActive(false);
			}

		}
	}
	void LastDialogSettings()
	{
		SkipText.SetActive(false);
		NextText.SetActive(false);
		EndText.SetActive(true);
		dialogState = IntroDialogState.LastDialog;
	}
	void ChoiceDialogSettings()
	{
		dialogState = IntroDialogState.SelectingChoice;
		dialogChoiceList = Game.GetListOfChoicesByDialog(currentDialog);
		//Debug.Log(dialogChoiceList[0].dialogueId);
		SkipText.SetActive(false);
		NextText.SetActive(false);
		ChoiceText.SetActive(true);
		ChoiceSelector.SetActive(true);
		SetDialogChoice(dialogChoiceList);
	}
	void EndOfCurrentDialogSettings()
	{
		SkipText.SetActive(false);
		NextText.SetActive(true);
		dialogState = IntroDialogState.EndOfDialog;
	}
	//void HandleChoiceDialog() //for a square choice dialog formation
	//{
	//	if (Input.GetKeyDown(KeyCode.RightArrow))
	//	{
	//		if (currentChoice < dialogChoiceList.Count - 1)
	//		{
	//			++currentChoice;
	//		}

	//	}
	//	else if (Input.GetKeyDown(KeyCode.LeftArrow))
	//	{
	//		if (currentChoice > 0)
	//			--currentChoice;
	//	}
	//	else if (Input.GetKeyDown(KeyCode.DownArrow))
	//	{
	//		if (currentChoice < dialogChoiceList.Count - 2)
	//			currentChoice += 2;
	//	}
	//	else if (Input.GetKeyDown(KeyCode.UpArrow))
	//	{
	//		if (currentChoice > 1)
	//			currentChoice -= 2;
	//	}
	//	UpdateChoiceSelection(currentChoice, dialogChoiceList[currentChoice]);

	//	if (Input.GetKeyDown(KeyCode.Return))
	//	{
	//		var chosenDialog = dialogChoiceList[currentChoice];

	//		currentDialog = Game.GetDialogByDialogList(chosenDialog.dialogueId, dialog1s);//setting to next dialog
	//		dialogState = DialogState.EndOfDialog;// assigning nextdialog to currentDialog from dialogList
	//		StartCoroutine(TypeDialog(currentDialog.dialogueText));

	//	}
	//}
	
}


