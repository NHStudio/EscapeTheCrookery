using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public enum PlayerFacing
    {
        Left = -1,
        Right = 1
    }
    
    public float speed = 5f;
    public float jumpForce = 5f;
    public float jumpMovementMultiplier = 0.7f;
    public Vector2 groundCheckBox = new(0.5f, 0.05f);
    public LayerMask groundMask;
    public Transform groundCheck;

    public PlayerFacing Facing { get; set; } = PlayerFacing.Right;

    private Rigidbody2D _rb;
    private Animator _animator;
    private float _turnSmoothVelocity;
    private Vector3 _velocity;
    private bool _isGrounded;

    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsShooting = Animator.StringToHash("IsShooting");
    private static readonly int OnGround = Animator.StringToHash("OnGround");
    private SpriteRenderer _spriteRenderer;

    private IWeapon _currWeapon;

    private PlayerParameters _playerParameters;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currWeapon = GetComponent(typeof(IWeapon)) as IWeapon;
        _playerParameters = GetComponent<PlayerParameters>();
    }

    private void Update()
    {
        if (_playerParameters.IsDead())
        {
            return;
        }
        if (_currWeapon is not null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _currWeapon.MainAttackStart();
            }
            
            if (Input.GetButtonUp("Fire1"))
            {
                _currWeapon.MainAttackEnd();
            }
            
            _animator.SetBool(IsShooting, _currWeapon.IsFiring);
        }
    }

    private void FixedUpdate()
    {
        var hit = Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0, groundMask);

        _isGrounded = hit is not null;
        
        var x = Input.GetAxisRaw("Horizontal");
        var tr = transform;

        Vector3 moveForce = Time.deltaTime * speed * tr.right;
        if (!_isGrounded)
        {
            moveForce *= jumpMovementMultiplier;
        }
        
        if (x != 0)
        {
            // As int: -1 or 1
            Facing = (PlayerFacing)x;
            _rb.AddForce((int)Facing * moveForce);
        }
        
        if (Input.GetButton("Jump") && _isGrounded)
        {
            _rb.AddForce(jumpForce * Vector2.up);
        }
        
        _animator.SetBool(IsRunning, x != 0);
        _animator.SetBool(OnGround, _isGrounded);
        _spriteRenderer.flipX = Facing == PlayerFacing.Left;
    }

}