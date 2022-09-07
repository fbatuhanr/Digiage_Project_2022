using System;
using NaughtyAttributes;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [AnimatorParam("_animator")]
    public int paramHash;

    
}
