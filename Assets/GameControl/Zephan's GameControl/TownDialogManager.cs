using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
// Zephan
public enum TownDialogState { Inactive,Reading, SelectingChoice, EndOfDialog, FinalDialog }
public class TownDialogManager : MonoBehaviour
{
	public GameObject dialogBox;
	[SerializeField] GameObject dialogText;
	[SerializeField] GameObject SkipText;
	[SerializeField] GameObject NextText;
	[SerializeField] GameObject EndText;
	[SerializeField] GameObject choiceSelector;
	[SerializeField] List<GameObject> choiceButtons;
	public event Action OnShowDialog;
	public event Action OnCloseDialog;
	public event Action OnCloseWarningDialog;
	public event Action StartBattleQuest;
	public event Action OnHeal;
	public event Action RewardCollected;
	public static TownDialogManager Instance { get; private set; }
	TownDialogState dialogState;
	//private Coroutine currentDialogCoroutine;
	bool skipOnNext;
	private bool allowSkip = false;
	private bool isWarningDialog;
	private Dialog currentDialog;
	private Dialog selectedDialog;
	List<Dialog> dialogList;
	List<Dialog> dialogChoiceList;
	int currentLine = 0;
	private bool allowNext = false;
	private int currentChoice;
	private int numberOfButtons;

	private void Awake()
	{
		Instance = this;
	}
	public void HandleUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Space) && dialogState == TownDialogState.EndOfDialog && allowNext == true)
		{
			if (!allowNext) return;
			if (allowNext) allowNext = false;
			//if(allowSkip) allowSkip = false;
			Debug.Log($"moving to next Dialog, this is current Dialog {currentDialog.dialogueId}");
			currentDialog = Game.GetDialogByDialogList(currentDialog.nextdialogueId, dialogList); // assigning nextdialog to currentDialog from dialogList
			StartCoroutine(TypeDialog(currentDialog)); // making sure that dialog is read straight from the list
		}
		else if (dialogState == TownDialogState.SelectingChoice)
		{
			//Debug.Log("Handling Choice");
			HandleChoiceDialog();
		}
		else if (Input.GetKeyDown(KeyCode.Space) && dialogState == TownDialogState.Reading)
		{
			skipOnNext = true;
		}
		if (dialogState == TownDialogState.FinalDialog)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Debug.Log($"this is isWarningDialog state when pressing space: {isWarningDialog}");
				if (isWarningDialog)
				{
					Debug.Log($"this is the dialogState when isWarningDialog: {dialogState}");
					OnCloseWarningDialog?.Invoke();
					isWarningDialog = false;
				}
				else
					OnCloseDialog?.Invoke();

				dialogBox.SetActive(false);
				Debug.Log("closing dialogue");
			}
		}

	}
	public IEnumerator ShowDialog(string dialogueType)
	{
		Debug.Log($"Reading Dialog, checking dialog state: {dialogState}");
		if (dialogState != TownDialogState.Reading)
		{
			yield return new WaitForEndOfFrame();
			OnShowDialog?.Invoke();
			if (dialogueType == "WARNING")
				isWarningDialog = true;
			dialogList = Game.GetDialogByType(dialogueType);
			Debug.Log($"List found {dialogList[0].dialogueText} ");
			currentDialog = dialogList[0];
			dialogBox.SetActive(true);
			//allowSkip = true;
			StartCoroutine(TypeDialog(currentDialog));
		}
	}
	public IEnumerator TypeDialog(Dialog currentDialog) // animating dialog to reveal letter by letter
	{
		Debug.Log($"This is the currentDialog's dialogueId: {currentDialog.dialogueId}");
		Debug.Log(currentDialog.dialogueText);
		
		if (dialogState != TownDialogState.Reading)
			dialogState = TownDialogState.Reading;
		dialogText.GetComponent<Text>().text = "";
		//Debug.Log($"this is the dialogId {currentDialog.dialogueId} after clearing: {dialogText.text}");
		SkipText.SetActive(true);
		NextText.SetActive(false);
		EndText.SetActive(false);
		if(currentDialog.dialogueType == "QC")
			CheckForSpecialDialog();
		foreach (var letter in currentDialog.dialogueText)
		{
			//Debug.Log($"this is the allowSkip state: {allowSkip}, this is the current letter {(string)dialogText.GetComponent<Text>().text} for dialogText {currentDialog.dialogueText}");
			Debug.Log($"this is the dialogState when starting animation: {dialogState}, with currentDialog: {currentDialog.dialogueId}");
			//Debug.Log($"this is the dialogId {currentDialog.dialogueId} after entering for loop: {dialogText.GetComponent<Text>().text}");
			if (allowSkip && skipOnNext) // skippable dialogue
			{
				//Debug.Log($"this is allowSkip status: {allowSkip} while dialogue is: {currentDialog.dialogueText}");
				//if (!allowSkip) break;
				allowSkip = false;
				//Debug.Log($"this is the dialogtext when skipping: {(string)dialogText.text}");
				Debug.Log($"this is the dialogState when skipping: {dialogState}, with currentDialog: {currentDialog.dialogueText}");
				dialogText.GetComponent<Text>().text = currentDialog.dialogueText;
				yield return new WaitForSeconds(0.2f);
				skipOnNext = false;
				SkipText.SetActive(false);
				NextText.SetActive(true);
				break;
			}

			else
			{
				//Debug.Log($"this is the dialogtext when animating: {(string)dialogText.text}");
				dialogText.GetComponent<Text>().text += letter;
				allowSkip = true;
				yield return new WaitForSeconds(1f / 30);
			}

		}
		Debug.Log($"this is currentdialogId's nextDialogId {currentDialog.nextdialogueId}");
		yield return new WaitForSeconds(0.2f);
		if (currentDialog.nextdialogueId == "-1")
		{
			SkipText.SetActive(false);
			NextText.SetActive(false);
			EndText.SetActive(true);
			Debug.Log($"this is the dialogState when setting dialogState to last dialog: {dialogState}, with dialogId: {currentDialog.dialogueId}");
			dialogState = TownDialogState.FinalDialog;

		}
		else if (currentDialog.nextdialogueId == "-2")
		{
			ChoiceDialogSettings();
		}
		else
		{
			SkipText.SetActive(false);
			NextText.SetActive(true);
			dialogState = TownDialogState.EndOfDialog;
			allowSkip = false;
			skipOnNext = false;
			yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
			allowNext = true;
		}
	}

	void ChoiceDialogSettings()
	{
		dialogState = TownDialogState.SelectingChoice;
		dialogChoiceList = Game.GetListOfChoicesByDialog(currentDialog);
		//Debug.Log(dialogChoiceList[0].dialogueId);
		dialogText.SetActive(false);
		SkipText.SetActive(false);
		NextText.SetActive(true);
		//choiceText.SetActive(true);
		choiceSelector.SetActive(true);
		SetDialogChoice(dialogChoiceList);
	}

	public void CheckForSpecialDialog()
	{
		switch(currentDialog.dialogueId)
		{
			case "QUEST0001":
				currentDialog.dialogueText = currentDialog.dialogueText.Replace("[level]", Game.mainsessionData.levelId);
				break;

			case "QC0001":
				int expReward = Game.startedQuest.expReward;
				currentDialog.dialogueText = currentDialog.dialogueText.Replace("[exp]", expReward.ToString());
				Game.startedQuest = null;
				Game.mainsessionData.exp += expReward;
				Game.UpdateCompletedQuest();
				RewardCollected?.Invoke();
				break;
		}
	}

	public void RestoreHealth(Dialog dialogId)
	{
		if (dialogId.dialogueText == "Restore Health")
		{
			OnHeal?.Invoke();
			Debug.Log("running heal");
		}
			
	}
	public void CheckForQuestTrigger(string dialogId)
	{
		foreach(Quest quest in Game.questList)
		{
			if (dialogId == quest.questdialogIdTrigger)
			{
				Game.startedQuest = quest;
				Game.battleQuestProgress = 0;
				Game.mainsessionData.startedQuest = Game.startedQuest.questId + "_" + Game.battleQuestProgress.ToString();
				StartBattleQuest?.Invoke();
				break;
			}
		}
		 //trigger start quest event
	}



	void HandleChoiceDialog() //for vertical choice selector
	{

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (currentChoice < numberOfButtons)
				++currentChoice;
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (currentChoice > 0)
				--currentChoice;
		}
		UpdateChoiceSelection(currentChoice);

		if (Input.GetKeyDown(KeyCode.Return)) // implement functions now
		{
			var chosenDialog = dialogChoiceList[currentChoice];
			CheckForQuestTrigger(chosenDialog.dialogueId);
			currentChoice = 0;
			numberOfButtons = 0;	
			currentDialog = Game.GetDialogByDialogList(chosenDialog.nextdialogueId, dialogList);
			Debug.Log($"This is the chosenDialog: {chosenDialog.dialogueId}, with the currentDialog: {currentDialog}");//setting to next dialog
			dialogState = TownDialogState.EndOfDialog;
			if(chosenDialog.dialogueText == "Restore Health")
			{
				RestoreHealth(chosenDialog);
			}
			if(currentDialog.nextdialogueId != "-2")
			{
				dialogText.SetActive(true);
				choiceSelector.SetActive(false);
			}  // assigning nextdialog to currentDialog from dialogList
			StartCoroutine(TypeDialog(currentDialog));

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
				numberOfButtons++;
				//Debug.Log(dialogState);
			}

			else
			{
				choiceButtons[i].SetActive(false);
			}

		}
	}
}
