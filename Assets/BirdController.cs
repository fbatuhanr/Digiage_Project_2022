using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [AnimatorParam("_animator")] 
    public int fly;

    [SerializeField] private Transform birdModel;
    [SerializeField] private Transform[] targetPoints;
    [SerializeField] private ParticleSystem featherExplosion;

    [SerializeField] private float moveDuration, rotateDuration;
    [SerializeField] private Ease moveEaseType, rotateEaseType;

    private Sequence _mySequence;
    private void Start()
    {
        _mySequence = DOTween.Sequence().SetAutoKill(false);
    }
    
    public void Escape()
    {
        _animator.SetTrigger(fly);
        
        featherExplosion.Play();

        foreach (var target in targetPoints) 
            _mySequence
                .Append(birdModel.transform.DOMove(target.position, moveDuration).SetEase(moveEaseType))
                .Join(birdModel.transform.DORotate(target.eulerAngles, rotateDuration).SetEase(rotateEaseType))
                /*.OnComplete(()=>Destroy(gameObject))*/;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _mySequence.Kill();
            _mySequence.Rewind();
            //Escape();
        }
    }
}
