using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public abstract class BaseActorParameters : MonoBehaviour, IDamageable
{
    public abstract int AttackDamage { get; }

    public int maxHitPoints;
    public int hitPoints;
    public HealthBar healthBar;
    public bool Dead { get; set; } = false;

    public virtual void TakeDamage(int damage)
    {
        hitPoints -= damage;
        healthBar.SetHealth(hitPoints);
        if (hitPoints <= 0)
        {
            Dead = true;
            GameObject.Destroy(gameObject);
        }
    }
}