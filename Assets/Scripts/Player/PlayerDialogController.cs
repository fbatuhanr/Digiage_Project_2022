using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogController : MonoBehaviour
{
    [SerializeField] private UIPanelAnimationController uiPanelAnimationController;
    [SerializeField] private ConversationUIController conversationUIController;
    
    public void Activate(SubDialog subDialog)
    {
        conversationUIController.LookToScreen();
        conversationUIController.ChangeLabel(subDialog.firstSentence);
        uiPanelAnimationController.Enable();
    }

    public void Deactivate()
    {
        uiPanelAnimationController.Disable();
    }
}
