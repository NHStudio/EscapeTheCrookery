using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public abstract class BaseActorParameters : MonoBehaviour, IDamageable
{
    public abstract int AttackDamage { get; }

    [SerializeField]
    [Header("Warning: Hit Points is set procedurally. Modify this property only while runtime for debugging purposes")]
    private int hitPoints;
    public int HitPoints
    {
        get => hitPoints;
        set
        {
            hitPoints = value;
            OnHealthChange?.Invoke(hitPoints);
        }
    }
    
    public event Action<int> OnHealthChange;
    
    private bool dead;
    
    public bool Dead
    {
        get => dead;
        set
        {
            if (dead) return;
            dead = value;
            if (!dead) return;
            _actorController.OnDeath();
            Destroy(gameObject);
        }
    }
    
    protected BaseActorController _actorController;

    protected void Awake()
    {
        _actorController = GetComponent(typeof(BaseActorController)) as BaseActorController;
    }

    public virtual void TakeDamage(int damage)
    {
        HitPoints -= damage;

        if (HitPoints <= 0)
        {
            Dead = true;
        }
    }
}