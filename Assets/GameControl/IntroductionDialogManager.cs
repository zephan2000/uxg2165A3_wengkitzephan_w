using pattayaA3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Zephan
public enum DialogState { Typing, SelectingChoice, EndOfDialog, LastDialog}
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
	public event Action OnShowDialog;
	public event Action OnCloseDialog;
	//public static TownDialogManager Instance {  get; private set; }
	private bool Skip;
	private dialog1 currentDialog;
	[SerializeField] StartMenuController startMenuController;
	//private void Awake()
	//{
	//	Instance = this; 
	//}
	DialogState dialogState;
	List<dialog1> dialog1s;
	List<dialog1> dialogChoiceList;
	int currentChoice;
	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.F) && dialogState == DialogState.EndOfDialog)
		{
			if (dialogState == DialogState.LastDialog)
			{
				//Debug.Log("Starting Next Line");
				//currentLine = 0;
				dialogBox.SetActive(false);
				OnCloseDialog?.Invoke();
			}
			else
			{
				//Debug.Log("End");
				currentDialog = Game.GetDialogByDialogList(currentDialog.nextdialogueId, dialog1s); // assigning nextdialog to currentDialog from dialogList
				StartCoroutine(TypeDialog(currentDialog.dialogueText));
			}
		}
		else if (dialogState == DialogState.SelectingChoice)
		{
			Debug.Log("Handling Choice");
			HandleChoiceDialog();
		}
		else if (Input.GetKeyDown(KeyCode.Space) | Input.GetMouseButtonDown(0) && dialogState == DialogState.Typing)
		{
			Skip = true;
		}
		else if (dialogState == DialogState.LastDialog)
		{
			if(Input.GetKeyDown(KeyCode.F))
			startMenuController.StartLevel("Town");
		}

	}
	public IEnumerator ShowDialog(string dialogueType)
	{
		yield return new WaitForEndOfFrame();
		OnShowDialog?.Invoke();
		//this.dialog = dialog; // change this line, need to read from a list of your own data
		dialog1s = Game.GetDialogByType(dialogueType);
		currentDialog = dialog1s[0];
		dialogBox.SetActive(true);
		StartCoroutine(TypeDialog(dialog1s[0].dialogueText));
	}

	public IEnumerator TypeDialog(string line) // animating dialog to reveal letter by letter
	{
		dialogState = DialogState.Typing;
		dialogText.text = "";
		NameText.GetComponent<Text>().text = currentDialog.currentSpeakerName; 
		SkipText.SetActive(true);
		NextText.SetActive(false);
		EndText.SetActive(false);
		ChoiceText.SetActive(false);
		ChoiceSelector.SetActive(false);
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
			dialogText.text += letter;
			yield return new WaitForSeconds(1f / 30);
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
		UpdateChoiceSelection(currentChoice, dialogChoiceList[currentChoice]); 

		if (Input.GetKeyDown(KeyCode.Return))
		{
			var chosenDialog = dialogChoiceList[currentChoice];
			currentChoice = 0;
			currentDialog = Game.GetDialogByDialogList(chosenDialog.dialogueId, dialog1s);//setting to next dialog
			dialogState = DialogState.EndOfDialog;// assigning nextdialog to currentDialog from dialogList
			StartCoroutine(TypeDialog(currentDialog.dialogueText));
			
		}
	}
	public void UpdateChoiceSelection(int selectedChoice, dialog1 dialogChoice)
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
	public void SetDialogChoice(List<dialog1> choices)
	{
		for (int i = 0; i < choiceButtons.Count; i++)
		{
			if (i < choices.Count)
			{
				choiceButtons[i].SetActive(true);
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
		dialogState = DialogState.LastDialog;
	}
	void ChoiceDialogSettings()
	{
		dialogState = DialogState.SelectingChoice;
		dialogChoiceList = Game.GetListOfChoicesByDialog(currentDialog);
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
		dialogState = DialogState.EndOfDialog;
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

