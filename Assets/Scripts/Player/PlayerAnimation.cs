using System;
using NaughtyAttributes;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator _animator;

    [AnimatorParam("_animator")]
    public int 
        running, 
        fallingDown, 
        fallingToLanding, 
        fallingIdle, 
        jumpingUp, 
        sadWalk, 
        textingWhileStanding, 
        yelling, 
        wipingSweat, 
        shrugging, 
        laughing, 
        standUp, 
        pushing, 
        disappointed, 
        walking,
        crouching;

    public void SetAnim(int anim, bool value=true, bool returnToBaseBeforeStart=true)
    {
        if(returnToBaseBeforeStart) ReturnBaseAnim();
        
        _animator.SetBool(anim, value);
    }

    public void SetAnimTrigger(int anim)
    {
        _animator.SetTrigger(anim);
    }

    public void ReturnBaseAnim()
    {
        foreach (var parameter in _animator.parameters) {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                _animator.SetBool(parameter.name, false);
        }
    }

    public void Crouch()
    {
        SetAnim(crouching);
    }
}
