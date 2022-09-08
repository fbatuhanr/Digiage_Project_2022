using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public enum UIPanelStatus { Enabled, Disabled }
public enum AnimType { PositionX, PositionY, Scale, Color }

public class UIPanelAnimationController : MonoBehaviour
{
    [HideInInspector] public UIPanelStatus uiPanelStatus;
    [SerializeField] private RectTransform background;

    [SerializeField] private RectTransform enableTarget, disableTarget;

    [SerializeField] private AnimType animType = AnimType.Scale;

    [ShowIf("animType", AnimType.Color)] 
    [SerializeField] private Image targetImage;
    [ShowIf("animType", AnimType.Color)] 
    [SerializeField] private Color enableColor, disableColor;

    [Header("Anim Durations: ")] 
    [SerializeField] private float durationOnEnable = .5f;
    [SerializeField] private float durationOnDisable = .5f;

    [Header("Anim Delays:")] 
    [SerializeField] private float delayOnEnable = 0;
    [SerializeField] private float delayOnDisable = 0;

    [Header("Anim Types:")] 
    [SerializeField] private Ease easeTypeOnEnable;
    [SerializeField] private Ease easeTypeOnDisable;

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
            case AnimType.Color:
                _mySequence.Append(targetImage.DOColor(enableColor, durationOnEnable).SetEase(easeTypeOnEnable).SetDelay(delayOnEnable));
                break;
        }

        uiPanelStatus = UIPanelStatus.Enabled;
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
            case AnimType.Color:
                _mySequence.Append(targetImage.DOColor(disableColor, durationOnEnable).SetEase(easeTypeOnEnable).SetDelay(delayOnEnable));
                break;
        }
        
        uiPanelStatus = UIPanelStatus.Disabled;
    }
}