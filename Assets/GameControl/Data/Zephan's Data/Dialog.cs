using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
	//dialogueId	nextdialogueId	dialogueType	currentSpeakerName	displaySpritePathLeft	displaySpritePathRight	dialogueText	choices
	public string dialogueId;
	public string nextdialogueId;
	public string dialogueType;
	public string currentSpeakerName;
	public string displaySpritePathLeft;
	public string displaySpritePathRight;
	public string dialogueText;	
	public string choices;
	public Dialog(string dialogueId, string nextdialogueId, string dialogueType, string currentSpeakerName, string displaySpritePathLeft, string displaySpritePathRight, string dialogueText, string choices)
	{
		this.dialogueId = dialogueId;
		this.nextdialogueId = nextdialogueId;
		this.dialogueType = dialogueType;
		this.currentSpeakerName = currentSpeakerName;
		this.displaySpritePathLeft = displaySpritePathLeft;
		this.displaySpritePathRight = displaySpritePathRight;
		this.dialogueText = dialogueText;
		this.choices = choices;
	}
}
