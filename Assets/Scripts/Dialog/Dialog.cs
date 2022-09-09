using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObjects/Dialog/DialogScriptableObject", order = 0)]
public class Dialog : ScriptableObject
{
    public SubDialog firstSubDialog;
}

[Serializable] public struct DialogAction
{
    public enum Actioner
    {
        Player,
        Caveman      
    }

    public Actioner actioner;
    public string methodName;
}

public enum Speaker
{
    Angel,
    Evil
}