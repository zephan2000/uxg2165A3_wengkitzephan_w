using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Zephan
public class DialogManager : MonoBehaviour
{
	[SerializeField] GameObject dialogBox;
	[SerializeField] Text dialogText;
	[SerializeField] GameObject SkipText;
	[SerializeField] GameObject NextText;
	public event Action OnShowDialog;
	public event Action OnCloseDialog;
	public static DialogManager Instance {  get; private set; }
	bool isTyping;
	private bool Skip;
	private void Awake()
	{
		Instance = this; 
	}
	Dialog dialog;
	int currentLine = 0;
	public void HandleUpdate()
	{
		if(Input.GetKeyDown(KeyCode.F) && !isTyping)
		{
			++currentLine;
			//Debug.Log("Next Line");
			if(currentLine < dialog.Lines.Count)
			{
				//Debug.Log("Starting Next Line");
				StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
			}
			else
			{
				//Debug.Log("End");
				currentLine = 0;
				dialogBox.SetActive(false);
				OnCloseDialog?.Invoke();
			}
		}
		else if (Input.GetKeyDown(KeyCode.Space) | Input.GetMouseButtonDown(0) && isTyping)
		{
			Skip = true;
		}

	}
	public IEnumerator ShowDialog(Dialog dialog)
	{
		yield return new WaitForEndOfFrame(); 
		OnShowDialog?.Invoke();
		this.dialog = dialog;
		dialogBox.SetActive(true);
		StartCoroutine(TypeDialog(dialog.Lines[0]));
	}

	public IEnumerator TypeDialog(string line) // animating dialog to reveal letter by letter
	{
		isTyping = true;
		dialogText.text = "";
		SkipText.SetActive(true);
		NextText.SetActive(false);
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
		SkipText.SetActive(false);
		//yield return new WaitForSeconds(1.2f);
		NextText.SetActive(true);
		isTyping = false;
	}
}
