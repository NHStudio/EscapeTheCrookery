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

    public IWeapon MainWeapon { get; set; }
    public IWeapon SecondaryWeapon { get; set; }

    public virtual bool TakeItem(ItemsMeta.Item item)
    {
        return false;
    }

    protected void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        if (MainWeapon is not null)
        {
            _animator.SetBool(IsShootingAnim, MainWeapon.IsFiring);
        }
        
        _spriteRenderer.flipX = Facing == ActorFacing.Left;
    }
}