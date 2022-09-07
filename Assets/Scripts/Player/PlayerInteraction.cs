using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private  PlayerDialogController playerDialogController;
    [SerializeField] private SelectionsUIController selectionsUIController;

    [SerializeField] private UIPanelAnimationController pressKeyForInteractionPanel;

    private DialogTrigger _currentDialogTrigger;
    private SubDialog _currentSubDialog;
    private void Update()
    {
        if (_currentDialogTrigger != null)
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

            ActivateDialog();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out DialogTrigger dialogTrigger))
        {
            playerDialogController.Deactivate();
            dialogTrigger.Deactivate();
            
            pressKeyForInteractionPanel.Disable();
        }
    }
    
    private bool IsNextDialogExist => _currentSubDialog.nextDialogs.Length > 0;
    private bool IsAnswerOrActionExist => _currentSubDialog.answers.Length > 0 || _currentSubDialog.actions.Length > 0;

    private void NextDialog()
    {
        if (IsNextDialogExist)
        {
            if (IsAnswerOrActionExist)
            {
                selectionsUIController.Activate(_currentSubDialog);
                DeactivateDialog();
            }
            else
                SetNewSubDialog(0);
        }
        else
            DeactivateDialog();
    }

    public void SetNewSubDialog(int index)
    {
        _currentSubDialog = _currentSubDialog.nextDialogs[index];
        ActivateDialog();
    }

    private void ActivateDialog()
    {
        switch (_currentSubDialog.firstSpeaker)
        {
            case Speaker.Angel:
                playerDialogController.Activate(_currentSubDialog);
                _currentDialogTrigger.Deactivate();
                break;
            case Speaker.Evil:
                _currentDialogTrigger.Activate(_currentSubDialog);
                playerDialogController.Deactivate();
                break;
        }
    }

    private void DeactivateDialog()
    {
        playerDialogController.Deactivate();
        _currentDialogTrigger.Deactivate();
    }
}
