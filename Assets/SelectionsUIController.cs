using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectionsUIController : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private RectTransform background;
    [SerializeField] private TextMeshProUGUI leftSelectionLabel, rightSelectionLabel;

    private UIPanelAnimationController _uiPanelAnimationController;
    
    private void Start()
    {
        
        _uiPanelAnimationController = GetComponent<UIPanelAnimationController>();
    }

    public void Activate(SubDialog subDialog)
    {
        leftSelectionLabel.SetText(subDialog.answers[0]);
        rightSelectionLabel.SetText(subDialog.answers[1]);
        _uiPanelAnimationController.Enable();
    }

    public void Deactivate()
    {
        _uiPanelAnimationController.Disable();
    }


    public void SelectLeftOption(int index)
    {
        playerInteraction.SetNewSubDialog(index);
        Deactivate();
    }

    public void SelectRightOption(int index)
    {
        playerInteraction.SetNewSubDialog(index);
        Deactivate();
    }
}
