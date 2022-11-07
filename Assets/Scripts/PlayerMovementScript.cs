using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public Vector2 groundCheckBox = new(0.5f, 0.05f);
    public LayerMask groundMask;
    public Transform groundCheck;

    private Rigidbody2D _rb;
    private Animator _animator;
    private float _turnSmoothVelocity;
    private Vector3 _velocity;
    private bool _isGrounded;

    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int OnGround = Animator.StringToHash("OnGround");
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var hit = Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0, groundMask);

        _isGrounded = hit != null;

        var x = Input.GetAxis("Horizontal");

        var tr = transform;

        _rb.AddForce(tr.right * (x * Time.deltaTime * speed));

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.AddForce(jumpForce * Vector2.up);
        }

        _animator.SetBool(IsRunning, x != 0);
        _animator.SetBool(OnGround, _isGrounded);
        _spriteRenderer.flipX = x < 0;
    }
}