using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum MovementType
{
    Walking,
    Running
}
public class PlayerMovement : MonoBehaviour
{
    public MovementType movementType;
    
    private Rigidbody _rigidbody;
    
    [Header("Movement Speed: ")]
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    [SerializeField] private PlayerAnimation playerAnimation;

    private float HorizontalInput => Input.GetAxis("Horizontal"); 
    private float VerticalInput => Input.GetAxis("Vertical"); 
    
    private float HorizontalRawInput => Input.GetAxisRaw("Horizontal");
    private float VerticalRawInput => Input.GetAxisRaw("Vertical");
    private float AxisInputRawMagnitude => new Vector2(HorizontalRawInput, VerticalRawInput).magnitude;
    

    private float HorizontalMovement => HorizontalInput * horizontalSpeed;
    private float VerticalMovement => VerticalInput*verticalSpeed;

    private bool _isPlayerLocked;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_isPlayerLocked)
        {
            HandleMovement();
            HandleRotation();
            HandleMovementAnimationByVelocity();
        }
    }
    public void LockPlayer()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _isPlayerLocked = true;
    }
    public void UnlockPlayer()
    {
        _isPlayerLocked = false;
    }


    private void HandleMovement()
    {
        _rigidbody.velocity = new Vector3(HorizontalMovement, 0, VerticalMovement);
    }
    private void HandleRotation()
    {
        if(AxisInputRawMagnitude > 0)
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity)*Quaternion.Euler(0,-90f,0);
    }

    private void HandleMovementAnimationByVelocity()
    {
        var currentVelocity = _rigidbody.velocity.magnitude;
        
        if (currentVelocity > 0)
        {
            switch (movementType)
            {
                case MovementType.Walking:
                    playerAnimation.SetAnim(playerAnimation.walking);
                    break;
                case MovementType.Running:
                    playerAnimation.SetAnim(playerAnimation.running);
                    break;
            }
        }
        else
        {
            playerAnimation.ReturnBaseAnim();
        }
    }
}