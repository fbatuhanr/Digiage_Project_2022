using System;
using Unity.VisualScripting;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.Rendering.VirtualTexturing;
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
    public float horizontalSpeed;
    public float verticalSpeed;

    [SerializeField] private PlayerAnimation playerAnimation;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce = 1;
    public bool _isOnGround = false;
    public bool _isJumped = false, _isFalling = false;
    public float groundRayDistance;

    private float HorizontalInput => Input.GetAxis("Horizontal"); 
    private float VerticalInput => Input.GetAxis("Vertical"); 
    
    private float HorizontalRawInput => Input.GetAxisRaw("Horizontal");
    private float VerticalRawInput => Input.GetAxisRaw("Vertical");
    private float AxisInputRawMagnitude => new Vector2(HorizontalRawInput, VerticalRawInput).magnitude;
    

    private float HorizontalMovement => HorizontalInput * horizontalSpeed;
    private float VerticalMovement => VerticalInput * verticalSpeed;

    private bool _isPlayerLocked;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.Instance.isCinematic) return;
        if (_isPlayerLocked) return;
        
        if (IsOnTheGround())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody.velocity += jumpForce * Vector3.up;
                playerAnimation.SetAnim(playerAnimation.jumpingUp);
                _isJumped = true;
            }

            if (_isFalling)
            {
                Debug.Log("hit the ground when its falling");
                playerAnimation.ReturnBaseAnim();

                _isJumped = false;
                _isFalling = false;
            }
            else if(!_isJumped)
            {
                HandleMovementAnimationByVelocity();
            }
        }
        else
        {
            if (!_isFalling && _rigidbody.velocity.y < 0)
            {
                Debug.Log("velocity < 0, isFalling!");
                playerAnimation.SetAnim(playerAnimation.fallingIdle);
                _isFalling = true;
            }
        }
        HandleMovement();
        HandleRotation();
    }

    private bool IsOnTheGround()
    {
        var origin = transform.position + (Vector3.up * 0.5f);
        var direction = transform.up * -1;
        var distance = groundRayDistance;
        if (Physics.Raycast(origin, direction, distance, groundLayer))
        {
            Debug.DrawRay(origin, direction*distance, Color.green);
            Debug.Log("Ground true");
            return true;
        }
        else
        {
            Debug.DrawRay(origin, direction*distance, Color.red);
            Debug.Log("Ground false");
            return false;
        }
    }
    

    public void LockPlayer()
    {
        _isPlayerLocked = true;
        
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        playerAnimation.ReturnBaseAnim();
    }
    public void UnlockPlayer()
    {
        _isPlayerLocked = false;
    }


    private void HandleMovement()
    {
        _rigidbody.velocity = new Vector3(HorizontalMovement, _isJumped || _isFalling ? _rigidbody.velocity.y : 0, VerticalMovement);
    }
    private void HandleRotation()
    {
        if(AxisInputRawMagnitude > 0)
            transform.rotation = Quaternion.LookRotation(new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z))*Quaternion.Euler(0,-90f,0);
    }

    private void HandleMovementAnimationByVelocity()
    {
        /*var currentVelocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.z).magnitude;
        var currentVelocity = AxisInputRawMagnitude;*/
        if (AxisInputRawMagnitude > 0)
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