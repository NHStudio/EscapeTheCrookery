using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class GroundedActorController : BaseActorController
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float jumpMovementMultiplier = 0.7f;
    
    public Vector2 groundCheckBox = new Vector2(0.5f, 0.05f);
    public LayerMask groundMask;
    public Transform groundCheck;
    
    protected static readonly int IsRunningAnim = Animator.StringToHash("IsRunning");
    protected static readonly int OnGroundAnim = Animator.StringToHash("OnGround");
    
    protected bool _isGrounded;

    protected new void Start()
    {
        base.Start();
    }

    protected new void Update()
    {   
        base.Update();
        _animator.SetBool(OnGroundAnim, _isGrounded);
    }

    protected void FixedUpdate()
    {
        var groundHit = Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0, groundMask);
        _isGrounded = groundHit is not null;
    }
    
    protected void Move(int x)
    {
        Debug.Assert(x is -1 or 0 or 1);
        
        var tr = transform;
        Vector3 moveForce = Time.deltaTime * speed * tr.right;
        if (!_isGrounded)
        {
            moveForce *= jumpMovementMultiplier;
        }
        
        if (x != 0)
        {
            // As int: -1 or 1
            Facing = (ActorFacing)x;
            _rb.AddForce((int)Facing * moveForce);
        }
        
        _animator.SetBool(IsRunningAnim, x != 0);
    }
    
    protected void Jump()
    {
        if (!_isGrounded)
        {
            return;
        }
        
        _rb.AddForce(jumpForce * Vector2.up);
    }
}