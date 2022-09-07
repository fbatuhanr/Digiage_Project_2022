using System;
using DG.Tweening;
using UnityEngine;

public enum Status { Enabled, Disabled }
public enum AnimType { PositionX, PositionY, Scale }

public class UIPanelAnimationController : MonoBehaviour
{
    [HideInInspector] public Status status;
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform enableTarget, disableTarget;

    [SerializeField] private AnimType animType = AnimType.Scale;
    [SerializeField] private float durationOnEnable = .5f, durationOnDisable = .5f;
    [SerializeField] private float delayOnEnable = 0, delayOnDisable = 0;
    [SerializeField] private Ease easeTypeOnEnable, easeTypeOnDisable;

    private Sequence _mySequence;

    private void Start()
    {
        _mySequence = DOTween.Sequence();
    }

    public void Enable()
    {
        switch (animType)
        {
            case AnimType.PositionX:
                _mySequence.Append(background.DOAnchorPosX(enableTarget.anchoredPosition.x, durationOnEnable).SetEase(easeTypeOnEnable).SetDelay(delayOnEnable));
                break;
            case AnimType.PositionY:
                _mySequence.Append(background.DOAnchorPosY(enableTarget.anchoredPosition.y, durationOnEnable).SetEase(easeTypeOnEnable).SetDelay(delayOnEnable));
                break;
            case  AnimType.Scale:
                _mySequence.Append(background.DOScale(Vector3.one, durationOnEnable).SetEase(easeTypeOnEnable).SetDelay(delayOnEnable));
                break;
        }

        status = Status.Enabled;
    }

    public void Disable()
    {
        switch (animType)
        {
            case AnimType.PositionX:
                _mySequence.Append(background.DOAnchorPosX(disableTarget.anchoredPosition.x, durationOnDisable).SetEase(easeTypeOnDisable).SetDelay(delayOnDisable));
                break;
            case AnimType.PositionY:
                _mySequence.Append(background.DOAnchorPosY(disableTarget.anchoredPosition.y, durationOnDisable).SetEase(easeTypeOnDisable).SetDelay(delayOnDisable));
                break;
            case  AnimType.Scale:
                _mySequence.Append(background.DOScale(Vector3.zero, durationOnDisable).SetEase(easeTypeOnDisable).SetDelay(delayOnDisable));
                break;
        }
        
        status = Status.Disabled;
    }
}