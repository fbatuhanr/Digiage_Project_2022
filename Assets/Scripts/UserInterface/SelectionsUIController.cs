using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SelectionsUIController : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private RectTransform background;
    [SerializeField] private TextMeshProUGUI leftSelectionLabel, rightSelectionLabel;

    private UIPanelAnimationController _panelScaleAnimationController;
    private UIPanelAnimationController _panelColorAnimationController;

    [SerializeField] private TextMeshProUGUI dialogQuestionLabel;
    
    [SerializeField] private RectTransform dotsMaskPanel;
    private float _maskTotalWidth;

    public float timeForSelection;
    private float _timeCounter;
    private bool _isTimeCounterEnable;
    
    private void Start()
    {
        _panelScaleAnimationController = GetComponents<UIPanelAnimationController>()[0];
        _panelColorAnimationController = GetComponents<UIPanelAnimationController>()[1];

        _isTimeCounterEnable = false;
        _timeCounter = 0;
        _maskTotalWidth = dotsMaskPanel.sizeDelta.x;
        dotsMaskPanel.sizeDelta = new Vector2(0, dotsMaskPanel.sizeDelta.y);
    }

    private void Update()
    {
        if (_isTimeCounterEnable)
        {
            dotsMaskPanel.sizeDelta = new Vector2(Mathf.Lerp(0, _maskTotalWidth, _timeCounter/timeForSelection), dotsMaskPanel.sizeDelta.y);
            _timeCounter += Time.deltaTime;

            if (_timeCounter / timeForSelection >= 1f)
            {
                TimeIsUp();
                _isTimeCounterEnable = false;
            }
        }
    }

    private void TimeIsUp()
    {
        if (Random.Range(0, 2) == 0)
            SelectLeftOption(0);
        else
            SelectRightOption(1);
    }

    public void Activate(SubDialog subDialog)
    {
        dialogQuestionLabel.SetText(subDialog.firstSentence);
        dialogQuestionLabel.gameObject.SetActive(true);
        
        leftSelectionLabel.SetText(subDialog.answers[0]);
        rightSelectionLabel.SetText(subDialog.answers[1]);
        
        _panelScaleAnimationController.Enable();
        _panelColorAnimationController.Enable();

        _isTimeCounterEnable = true;
        _timeCounter = 0;
    }

    public void Deactivate()
    {
        dialogQuestionLabel.gameObject.SetActive(false);
        
        _panelScaleAnimationController.Disable();
        _panelColorAnimationController.Disable();
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
