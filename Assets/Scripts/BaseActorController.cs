using System;
using DefaultNamespace;
using UnityEngine;

public class BaseActorController : MonoBehaviour
{
    public enum ActorFacing
    {
        Left = -1,
        Right = 1
    }

    public ActorFacing Facing;
    
    protected static readonly int IsShootingAnim = Animator.StringToHash("IsShooting");

    protected Rigidbody2D _rb;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    
    protected IWeapon _currWeapon;

    protected void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currWeapon = GetComponent(typeof(IWeapon)) as IWeapon;
    }

    protected void Update()
    {
        if (_currWeapon is not null)
        {
            _animator.SetBool(IsShootingAnim, _currWeapon.IsFiring);
        }
        
        _spriteRenderer.flipX = Facing == ActorFacing.Left;
    }
}