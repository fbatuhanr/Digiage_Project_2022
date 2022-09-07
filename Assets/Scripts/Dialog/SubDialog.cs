using System;
using UnityEngine;


[CreateAssetMenu(fileName = "SubDialogData", menuName = "ScriptableObjects/Dialog/SubDialogScriptableObject", order = 0)]
public class SubDialog : ScriptableObject
{
    public Speaker firstSpeaker;
    public string firstSentence;
    public string[] answers;
    public DialogAction[] actions;
    public SubDialog[] nextDialogs;
}