using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class FirstSceneFlowController : MonoBehaviour
{
    private GameObject player;
    
    private PlayerAnimation _playerAnimation;
    private Rigidbody _playerRigidbody;
    
    [SerializeField] private float playerFallPowerFromPortal;
    private bool _isFalledDown = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    { 
        _playerAnimation = player.GetComponent<PlayerAnimation>();
        _playerRigidbody = player.GetComponent<Rigidbody>();
        
        CameraManager.Instance.PortalPriority();
        Invoke(nameof(StartFallingDownFromPortal), 2f);
    }

    public void StartFallingDownFromPortal()
    {
        CameraManager.Instance.BeginningPriority();
        _playerRigidbody.AddForce(Vector3.back*playerFallPowerFromPortal, ForceMode.VelocityChange);
        _playerAnimation.SetAnimTrigger(_playerAnimation.fallingDown);
    }

    private void FinishFallingDownFromPortal()
    {
        CameraManager.Instance.PlayerFollowPriority();
        GameManager.Instance.isCinematic = false;
    }

    private void Update()
    {
        
        if(_playerAnimation._animator.GetCurrentAnimatorStateInfo(0).IsName("Falling Down") 
           && _playerAnimation._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
           && !_isFalledDown)
        {
            FinishFallingDownFromPortal();
            _isFalledDown = true;
        }
    }
}
