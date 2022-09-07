using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    [Header("Movement Speed: ")]
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    
    private float HorizontalInput { get { return Input.GetAxis("Horizontal"); } }
    private float VerticalInput { get { return Input.GetAxis("Vertical"); } }
    
    private float HorizontalMovement { get { return HorizontalInput * horizontalSpeed; } }
    private float VerticalMovement { get { return VerticalInput*verticalSpeed; } }

    private bool _isPlayerLocked;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(!_isPlayerLocked) 
            _rigidbody.velocity = new Vector3(HorizontalMovement, VerticalMovement, 0);
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
}