using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObjects/Dialog/DialogScriptableObject", order = 0)]
public class Dialog : ScriptableObject
{
    public SubDialog firstSubDialog;
}

[Serializable] public struct DialogAction
{
    public string content;
    public string methodName;
}

public enum Speaker
{
    Angel,
    Evil
}