using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class GroundedActorController : BaseActorController
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float jumpMovementMultiplier = 0.7f;
    public float knockback = 25f;
    
    public Vector2 groundCheckBox = new Vector2(0.5f, 0.05f);
    public LayerMask groundMask;
    public Transform groundCheck;
    
    protected static readonly int IsRunningAnim = Animator.StringToHash("IsRunning");
    protected static readonly int OnGroundAnim = Animator.StringToHash("OnGround");
    protected static readonly int GetHitAnim = Animator.StringToHash("IsHit");

    protected bool _isGrounded;
    protected new void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        base.Start();
    }

    protected new void Update()
    {   
        base.Update();
        _animator.SetBool(OnGroundAnim, _isGrounded);
        // if (_animator.GetBool(OnGroundAnim))
        // {
        //     _animator.SetBool(GetHitAnim, false);
        // }
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

    protected void BulletKnockback(Collision2D col)
    {
        var bulletDir = col.gameObject.GetComponent<Projectile>().direction;
        HitKnockback(bulletDir);
    }

    protected void HitKnockback(Vector3 dir)
    {
        // Keep only right or left
        dir = Vector3.Project(dir, Vector3.right).normalized;
        
        Vector2 knockbackDir =
            new Vector2(dir.x > 0 ? 1.0f : -1.0f, 1.0f).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDir * knockback);
        _animator.SetBool(GetHitAnim, true); 
        _animator.Play("PlayerHit");
    }
}