using System;
using NaughtyAttributes;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [AnimatorParam("_animator")]
    public int running, fallingDown, fallingToLanding, mutantJumping, sadWalk, textingWhileStanding, yelling, wipingSweat, shrugging, laughing, standUp, pushing, disappointed, walking;

    public void SetAnim(int anim, bool value=true, bool returnToBaseBeforeStart=true)
    {
        if(returnToBaseBeforeStart) ReturnBaseAnim();
        
        _animator.SetBool(anim, value);
    }

    public void ReturnBaseAnim()
    {
        foreach (var parameter in _animator.parameters) {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                _animator.SetBool(parameter.name, false);
        }
    }
}
