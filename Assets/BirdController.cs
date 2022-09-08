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

    [SerializeField] private float moveDuration, rotateDuration;
    [SerializeField] private Ease moveEaseType, rotateEaseType;

    private Sequence _mySequence;
    private void Start()
    {
        _mySequence = DOTween.Sequence();
    }
    
    public void Escape()
    {
        _animator.SetTrigger(fly);

        foreach (var target in targetPoints)
            _mySequence.Append(birdModel.transform.DOMove(target.position, moveDuration).SetEase(moveEaseType));
    }
}
