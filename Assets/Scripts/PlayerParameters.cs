using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerParameters : BaseActorParameters
{
    public float attackMultiplier = 2.0f;
    
    public float invunerableTime = 7.0f;
    public float invunerableBlinkPeriod = 0.5f;
    public float attackMultiplierTime = 15.0f;
    
    [Header("Warning: Attack Damage is set procedurally. Modify this property only while runtime for debugging purposes")]
    public int attackDamage;

    public override int AttackDamage => attackDamage;

    public float currAttackMultiplier = 1.0f;
    public bool invunerable = false;

    private float _attackMultiplierEndTime;
    private float _invunerabilityEndTime;
    
    private float _invulNextBlinkTime;
    private bool _invulblinkState;

    private SpriteRenderer _spriteRenderer;

    private new void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        attackDamage = PlayerStatsManager.Instance.stats.basePlayerDamage;
        HitPoints = PlayerStatsManager.Instance.stats.playerBaseHealth;
    }

    private void Update()
    {
        if (currAttackMultiplier != 1.0f && Time.time > _attackMultiplierEndTime)
        {
            currAttackMultiplier = 1.0f;
        }

        if (invunerable && Time.time > _invunerabilityEndTime)
        {
            invunerable = false;
            _spriteRenderer.color = Color.white;
            _invulblinkState = true;
        } else if (invunerable && Time.time > _invulNextBlinkTime)
        {
            _invulNextBlinkTime = Time.time + invunerableBlinkPeriod;
            
            _spriteRenderer.color = _invulblinkState ? Color.magenta : Color.white;
            _invulblinkState = !_invulblinkState;
        }
    }
    
    public override void TakeDamage(int damage)
    {
        if (invunerable) return;
        base.TakeDamage(damage);
    }
    
    public void ApplyInvunerability()
    {
        invunerable = true;
        _invunerabilityEndTime = Time.time + invunerableTime;
    }
    
    public void ApplyAttackMultiplier()
    {
        currAttackMultiplier = attackMultiplier;
        _attackMultiplierEndTime = Time.time + attackMultiplierTime;
    }
}