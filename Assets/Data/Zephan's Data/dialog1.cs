using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialog1 : MonoBehaviour
{
	//dialogueId	nextdialogueId	dialogueType	currentSpeakerName	displaySpritePathLeft	displaySpritePathRight	dialogueText	choices
	public string dialogueId { get; }
	public string nextdialogueId { get; }
	public string dialogueType { get; }
	public string currentSpeakerName { get; }
	public string displaySpritePathLeft { get; }
	public string displaySpritePathRight { get; }
	public string dialogueText { get; }
	public string choices { get; }
	public dialog1(string dialogueId, string nextdialogueId, string dialogueType, string currentSpeakerName, string displaySpritePathLeft, string displaySpritePathRight, string dialogueText, string choices)
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
