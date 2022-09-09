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
            
            Destroy(other.GetComponent<BoxCollider>());
        }
        else if (other.TryGetComponent(out MonologTrigger monologTrigger))
        {
            _currentSubDialog = monologTrigger.dialog.firstSubDialog;
            
            if(IsActionExist)
                pressKeyForInteractionPanel.Enable();
            
            ActivateDialog();
            
            Destroy(other.GetComponent<BoxCollider>());
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
            Debug.Log(_currentDialogTrigger);
            if (_currentDialogTrigger != null) _currentDialogTrigger.Activate(_currentSubDialog);
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
            {
                if (IsActionExist)
                {
                    if (_currentSubDialog.actions[0].actioner == DialogAction.Actioner.Caveman)
                    {
                        var actionMethodName = _currentSubDialog.actions[0].methodName;
                        CavemanController.Instance.Invoke(actionMethodName, 0f);
                    }
                }
                
                SetNewSubDialog(0);
            }
        }
        else if (IsActionExist)
        {
            if (_currentSubDialog.actions.Length > 0)
            {
                var actionMethodName = _currentSubDialog.actions[0].methodName;
                
                if (_currentSubDialog.actions[0].actioner == DialogAction.Actioner.Player)
                {
                    Debug.Log("actioner is player: " + actionMethodName);
                    playerAnimation.Invoke(actionMethodName, 0f);
                }
                else if (_currentSubDialog.actions[0].actioner == DialogAction.Actioner.Caveman)
                {
                    Debug.Log("actioner is caveman: " + actionMethodName);
                    
                    /* Set player and caveman movements*/
                    playerMovement.movementType = MovementType.Running;
                    playerMovement.horizontalSpeed *= 2;
                    playerMovement.verticalSpeed *= 2;
                    
                    CavemanController.Instance.Invoke(actionMethodName, 0f);
                }
            }
            
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
