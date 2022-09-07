using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro.Examples;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    public Transform rock;

    public Transform target;
    public float duration;
    public Ease easeType;

    private void Start()
    {
        //rock.DOScale(Vector3.one * 5, 1f).SetEase(Ease.InOutBounce).SetDelay(3f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Anim();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Shake();
        }
    }

    private void Anim()
    {
        rock.DOMove(target.position, duration).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
    }

    private void Shake()
    {
        rock.DOShakePosition(1f, Vector3.one);
    }
}
