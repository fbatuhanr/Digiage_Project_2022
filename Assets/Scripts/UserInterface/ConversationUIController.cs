using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    
    
    [SerializeField] private Image pressKey;
    [SerializeField] private Color pressKeyTargetColor;
    [SerializeField] private float pressKeyDuration;
    
    private void Start()
    {
        //pressKeyColor = 
        pressKey.DOColor(pressKeyTargetColor, pressKeyDuration).SetLoops(-1, LoopType.Yoyo);
    }

    public void LookToScreen()
    {
        transform.eulerAngles = Vector3.zero;
    }
    
    private void Update()
    {
        transform.eulerAngles = Vector3.zero;
    }

    public void ChangeLabel(string content)
    {
        label.SetText(content);
    }
}
