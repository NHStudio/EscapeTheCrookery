using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public abstract class BaseActorParameters : MonoBehaviour, IDamageable
{
    public abstract int AttackDamage { get; }

    // Warning: set procedurally
    public int HitPoints { get; set; }
    
    public event Action<int> OnTakeDamage;
    
    private bool dead;
    
    public bool Dead
    {
        get => dead;
        set
        {
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
        OnTakeDamage?.Invoke(HitPoints);

        if (HitPoints <= 0)
        {
            Dead = true;
        }
    }
}