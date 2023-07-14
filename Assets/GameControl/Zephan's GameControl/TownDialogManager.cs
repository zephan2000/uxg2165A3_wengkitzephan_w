using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
// Zephan
public enum TownDialogState { Inactive,Reading, SelectingChoice, EndOfDialog, LastDialog }
public class TownDialogManager : MonoBehaviour
{
	public GameObject dialogBox;
	[SerializeField] Text dialogText;
	[SerializeField] GameObject SkipText;
	[SerializeField] GameObject NextText;
	[SerializeField] GameObject EndText;
	public event Action OnShowDialog;
	public event Action OnCloseDialog;
	public event Action OnCloseWarningDialog;
	public static TownDialogManager Instance { get; private set; }
	TownDialogState dialogState;
	//private Coroutine currentDialogCoroutine;
	bool isTyping;
	private bool allowSkip = false;
	private bool isWarningDialog;
	private Dialog currentDialog;
	List<Dialog> dialogList;
	int currentLine = 0;
	private bool allowNext = false;
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
		if (dialogState == TownDialogState.LastDialog)
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
			allowSkip = true;
			StartCoroutine(TypeDialog(currentDialog));
		}
	}
	public IEnumerator TypeDialog(Dialog currentDialog) // animating dialog to reveal letter by letter
	{
		Debug.Log(currentDialog.dialogueText);
		if (dialogState != TownDialogState.Reading)
			dialogState = TownDialogState.Reading;

		dialogText.text = "";
		//Debug.Log($"this is the dialogId {currentDialog.dialogueId} after clearing: {dialogText.text}");
		SkipText.SetActive(true);
		NextText.SetActive(false);
		EndText.SetActive(false);
		foreach (var letter in currentDialog.dialogueText)
		{
			Debug.Log($"this is the dialogState when starting animation: {dialogState}");
			Debug.Log($"this is the dialogId {currentDialog.dialogueId} after entering for loop: {dialogText.text}");
			if (allowSkip == true && Input.GetKey(KeyCode.Space)) // skippable dialogue
			{
				//Debug.Log($"this is allowSkip status: {allowSkip} while dialogue is: {currentDialog.dialogueText}");
				//if (!allowSkip) yield return null;
				allowSkip = false;
				//Debug.Log($"this is the dialogtext when skipping: {(string)dialogText.text}");
				Debug.Log($"this is the dialogState when skipping: {dialogState}, with currentDialog: {currentDialog.dialogueText}");
				dialogText.text = currentDialog.dialogueText;
				SkipText.SetActive(false);
				//yield return new WaitForSeconds(1.2f);
				NextText.SetActive(true);
				break;
			}

			else
			{
				Debug.Log($"this is the dialogtext when animating: {(string)dialogText.text}");
				dialogText.text += letter;
				yield return new WaitForSeconds(1f / 10);
			}

		}
		Debug.Log($"this is currentdialogId's nextDialogId {currentDialog.nextdialogueId}");

		if (currentDialog.nextdialogueId == "-1")
		{
			SkipText.SetActive(false);
			NextText.SetActive(true);
			allowSkip = true;
			Debug.Log($"this is the dialogState when setting dialogState to last dialog: {dialogState}");
			dialogState = TownDialogState.LastDialog;

		}
		else
		{
			SkipText.SetActive(false);
			NextText.SetActive(false);
			EndText.SetActive(true);
			dialogState = TownDialogState.EndOfDialog;

			allowNext = true;
		}
		isTyping = false;
	}
	//public IEnumerator TypeDialog(Dialog currentDialog) // animating dialog to reveal letter by letter
	//{
	//	Debug.Log(currentDialog.dialogueText);
	//	if (dialogState != TownDialogState.Reading)
	//		dialogState = TownDialogState.Reading;

	//	dialogText.text = "";
	//	//Debug.Log($"this is the dialogId {currentDialog.dialogueId} after clearing: {dialogText.text}");
	//	SkipText.SetActive(true);
	//	NextText.SetActive(false);
	//	EndText.SetActive(false);
	//	foreach (var letter in currentDialog.dialogueText)
	//	{ 
	//		Debug.Log($"this is the dialogState when starting animation: {dialogState}");
	//		Debug.Log($"this is the dialogId {currentDialog.dialogueId} after entering for loop: {dialogText.text}");
	//		if (allowSkip == true && Input.GetKey(KeyCode.Space)) // skippable dialogue
	//		{
	//			//Debug.Log($"this is allowSkip status: {allowSkip} while dialogue is: {currentDialog.dialogueText}");
	//			//if (!allowSkip) yield return null;
	//			allowSkip = false;
	//			//Debug.Log($"this is the dialogtext when skipping: {(string)dialogText.text}");
	//			Debug.Log($"this is the dialogState when skipping: {dialogState}, with currentDialog: {currentDialog.dialogueText}");
	//			dialogText.text = currentDialog.dialogueText;
	//			SkipText.SetActive(false);
	//			//yield return new WaitForSeconds(1.2f);
	//			NextText.SetActive(true);
	//			break;
	//		}

	//		else
	//		{
	//			Debug.Log($"this is the dialogtext when animating: {(string)dialogText.text}");
	//			dialogText.text += letter;
	//			yield return new WaitForSeconds(1f / 30);
	//		}
			
	//	}
	//	Debug.Log($"this is currentdialogId's nextDialogId {currentDialog.nextdialogueId}");
		
	//	yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
	//	NextDialog(currentDialog);
	//	//if (currentDialog.nextdialogueId == "-1")
	//	//{
	//	//	Debug.Log($"this is the dialogState when setting dialogState to last dialog: {dialogState}");
	//	//	dialogState = TownDialogState.LastDialog;
			
	//	//}
	//	//else
	//	//{
	//	//	dialogState = TownDialogState.EndOfDialog;
			
	//	//	allowNext = true;
	//	//}
	//	//isTyping = false;
	//}

	//private void NextDialog(Dialog currentDialog)
	//{
	//	Debug.Log($"{currentDialog}");
	//	if (currentDialog.nextdialogueId == "-2") return;
	//	if (currentDialog.nextdialogueId != "-1")
	//	{
	//		SkipText.SetActive(false);
	//		NextText.SetActive(true);
	//		allowSkip = true;	
	//		StartCoroutine(TypeDialog(Game.GetDialogByDialogList(currentDialog.nextdialogueId, dialogList)));
			
	//	}
			
	//	else
	//	{
	//		SkipText.SetActive(false);
	//		NextText.SetActive(false);
	//		EndText.SetActive(true);
	//		dialogState = TownDialogState.LastDialog;
	//	}
			
	//}

	
}
