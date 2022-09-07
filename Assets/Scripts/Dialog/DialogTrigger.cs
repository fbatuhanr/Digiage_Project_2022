using System;
using UnityEngine;


public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    
    [SerializeField] private UIPanelAnimationController uiPanelAnimationController;
    [SerializeField] private ConversationUIController conversationUIController;

    public void Activate(SubDialog subDialog)
    {
        conversationUIController.ChangeLabel(subDialog.firstSentence);
        uiPanelAnimationController.Enable();
    }

    public void Deactivate()
    {
        uiPanelAnimationController.Disable();
    }
}