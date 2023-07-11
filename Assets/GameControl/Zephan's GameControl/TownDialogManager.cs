using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Zephan
public class TownDialogManager : MonoBehaviour
{
	[SerializeField] GameObject dialogBox;
	[SerializeField] Text dialogText;
	[SerializeField] GameObject SkipText;
	[SerializeField] GameObject NextText;
	[SerializeField] GameObject EndText;
	public event Action OnShowDialog;
	public event Action OnCloseDialog;
	public static TownDialogManager Instance {  get; private set; }
	bool isTyping;
	private bool Skip;
	private bool lastDialog;
	private Dialog currentDialog;
	private void Awake()
	{
		Instance = this; 
	}
	List<Dialog> dialog1s;
	int currentLine = 0;
	public void HandleUpdate()
	{
		if(Input.GetKeyDown(KeyCode.F) && !isTyping)
		{
			//++currentLine;
			//Debug.Log("Next Line");
			if (lastDialog) 
			{
				//Debug.Log("Starting Next Line");
				//currentLine = 0;
				lastDialog = false;
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
		else if (Input.GetKeyDown(KeyCode.Space) | Input.GetMouseButtonDown(0) && isTyping)
		{
			Skip = true;
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
		isTyping = true;
		dialogText.text = "";
		SkipText.SetActive(true);
		NextText.SetActive(false);
		EndText.SetActive(false);
		foreach (var letter in line.ToCharArray())
		{
			if(Skip) // skippable dialogue
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
			lastDialog = true;
			SkipText.SetActive(false);
			NextText.SetActive(false);
			EndText.SetActive(true);
		}
		else
		{
			SkipText.SetActive(false);
			NextText.SetActive(true);
		}
		isTyping = false;
	}
}
