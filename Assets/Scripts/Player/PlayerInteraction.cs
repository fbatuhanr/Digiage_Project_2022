using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private PlayerDialogController playerDialogController;
    [SerializeField] private SelectionsUIController selectionsUIController;

    [SerializeField] private UIPanelAnimationController pressKeyForInteractionPanel;

    [SerializeField] private float monologDuration;

    private DialogTrigger _currentDialogTrigger;
    private SubDialog _currentSubDialog;

    private void Update()
    {
        if (_currentSubDialog != null)
        {
            if (Input.GetKeyDown(interactKey))
            {
                NextDialog();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DialogTrigger dialogTrigger))
        {
            _currentDialogTrigger = dialogTrigger;
            _currentSubDialog = _currentDialogTrigger.dialog.firstSubDialog;
            
            pressKeyForInteractionPanel.Enable();
            
            playerMovement.LockPlayer();

            ActivateDialog();
            
            Destroy(dialogTrigger);
        }
        else if (other.TryGetComponent(out MonologTrigger monologTrigger))
        {
            _currentSubDialog = monologTrigger.dialog.firstSubDialog;
            
            if(IsActionExist)
                pressKeyForInteractionPanel.Enable();
            
            ActivateDialog();

            Destroy(monologTrigger);
        }
        else if (other.TryGetComponent(out BirdController birdController))
        {
            birdController.Escape();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out DialogTrigger dialogTrigger))
        {
            playerDialogController.Deactivate();
            dialogTrigger.Deactivate();
            
            pressKeyForInteractionPanel.Disable();
            
            selectionsUIController.Deactivate();
            
            playerMovement.UnlockPlayer();
        }
        else if (other.TryGetComponent(out MonologTrigger monologTrigger))
        {
            
        }
    }
    
    private bool IsNextDialogExist => _currentSubDialog.nextDialogs.Length > 0;
    private bool IsAnswerExist => _currentSubDialog.answers.Length > 0;
    private bool IsActionExist => _currentSubDialog.actions.Length > 0;

    private void ActivateDialog()
    {
        if (_currentSubDialog.firstSpeaker == Speaker.Angel)
        {
            playerDialogController.Activate(_currentSubDialog);
            if(_currentDialogTrigger != null) _currentDialogTrigger.Deactivate();
        }
        else if (_currentSubDialog.firstSpeaker == Speaker.Evil)
        {
            playerDialogController.Deactivate();
            if(_currentDialogTrigger != null) _currentDialogTrigger.Activate(_currentSubDialog);
        }

        if (!IsNextDialogExist)
        {
            playerMovement.UnlockPlayer();
            // Invoke(nameof(DeactivateDialog), 3f);
        }
    }


    private void DeactivateDialog()
    {
        playerDialogController.Deactivate();
        if(_currentDialogTrigger != null) _currentDialogTrigger.Deactivate();
    }
    
    
    private void NextDialog()
    {
        if (IsNextDialogExist)
        {
            if (IsAnswerExist)
            {
                selectionsUIController.Activate(_currentSubDialog);
                pressKeyForInteractionPanel.Disable();
                DeactivateDialog();
            }
            else
                SetNewSubDialog(0);
        }
        else if (IsActionExist)
        {
            var _methodName = _currentSubDialog.actions[0].methodName;
            Debug.Log(_methodName);
            playerAnimation.Invoke(_methodName, 0f);
            
            pressKeyForInteractionPanel.Disable();
            DeactivateDialog();
        }
        else 
            DialogFinished();
    }


    public void SetNewSubDialog(int index)
    {
        if (_currentSubDialog.nextDialogs.Length > 0)
        {
            _currentSubDialog = _currentSubDialog.nextDialogs[index];
            ActivateDialog();
        }
    }
    
    private void DialogFinished()
    {
        playerMovement.UnlockPlayer();
        pressKeyForInteractionPanel.Disable();
        DeactivateDialog();
    }
}
