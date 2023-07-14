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
	private Coroutine currentDialogCoroutine;
	bool isTyping;
	private bool Skip;
	private bool isWarningDialog;
	private Dialog currentDialog;
	private void Awake()
	{
		Instance = this;
	}
	List<Dialog> dialogList;
	int currentLine = 0;
	public void HandleUpdate()
	{
		//if (Input.GetKeyDown(KeyCode.F) && dialogState == TownDialogState.EndOfDialog)
		//{
		//	Debug.Log("moving to next Dialog");
		//	StartCoroutine(TypeDialog(Game.GetDialogByDialogList(currentDialog.nextdialogueId, dialogList))); // making sure that dialog is read straight from the list
		//}
		//else if (dialogState == TownDialogState.LastDialog)
		//{
		//	//Debug.Log("Starting Next Line");
		//	//currentLine = 0;
		//	//Debug.Log($"this is the dialogState when last dialog: {dialogState}");
		//	if (Input.GetKeyDown(KeyCode.Space))
		//	{
		//		Debug.Log($"this is isWarningDialog state when pressing space: {isWarningDialog}");
		//		if (isWarningDialog)
		//		{
		//			Debug.Log($"this is the dialogState when isWarningDialog: {dialogState}");
		//			OnCloseWarningDialog?.Invoke();
		//			isWarningDialog = false;
		//		}
		//		else
		//			OnCloseDialog?.Invoke();

		//		dialogBox.SetActive(false);
		//		Debug.Log("closing dialogue");
		//	}
		//}

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
			currentDialogCoroutine = StartCoroutine(TypeDialog(currentDialog));
		}
	}

	public IEnumerator TypeDialog(Dialog currentDialog) // animating dialog to reveal letter by letter
	{
		Debug.Log(currentDialog.dialogueText);
		if(dialogState != TownDialogState.Reading)
			dialogState = TownDialogState.Reading;
		
		dialogText.text = "";
		Debug.Log($"this is the dialogId {currentDialog.dialogueId} after clearing: {dialogText.text}");
		SkipText.SetActive(true);
		NextText.SetActive(false);
		EndText.SetActive(false);
		if(currentDialog.nextdialogueId != "-2") 
		{
			foreach (var letter in currentDialog.dialogueText)
			{
				Debug.Log($"this is the dialogState when starting animation: {dialogState}");
				if (Input.GetKey(KeyCode.Space)) // skippable dialogue
				{
					Debug.Log($"this is the dialogtext when skipping: {(string)dialogText.text}");
					Debug.Log($"this is the dialogState when skipping: {dialogState}");
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
					yield return new WaitForSeconds(1f / 30);
				}

			}
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
			NextDialog(currentDialog.nextdialogueId);
		}
		
		//if (currentDialog.nextdialogueId == "-1" && dialogState != TownDialogState.LastDialog)
		//{
		//	Debug.Log($"this is the dialogState when setting dialogState to last dialog: {dialogState}");
		//	dialogState = TownDialogState.LastDialog;
		//	SkipText.SetActive(false);
		//	NextText.SetActive(false);
		//	EndText.SetActive(true);
		//}
		//else
		//{
		//	dialogState = TownDialogState.EndOfDialog;
		//	SkipText.SetActive(false);
		//	NextText.SetActive(true);
		//}
		//isTyping = false;

	}

	private void NextDialog(string nextDialogId)
	{
		Debug.Log($"{nextDialogId}");
		if (nextDialogId == "-2") return;
		if (nextDialogId != "-1")
			StartCoroutine(TypeDialog(Game.GetDialogByDialogList(nextDialogId,dialogList)));
		else
			EndDialogSettings();
	}

	private void EndDialogSettings()
	{
		Debug.Log($"this is the current Coroutine: {currentDialogCoroutine}");
		StopCoroutine(currentDialogCoroutine);
		dialogState = TownDialogState.Inactive;
		if (isWarningDialog)
		{
			Debug.Log($"this is the dialogState when isWarningDialog: {dialogState}");
			OnCloseWarningDialog?.Invoke();
			isWarningDialog = false;
		}
		else
			OnCloseDialog?.Invoke();

		dialogBox.SetActive(false);
	}
}
