using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerParameters : MonoBehaviour, IDamageable
{
    public int attackDamage;
    public int hitPoints;
    private bool _isDead = false;

    public bool IsDead()
    {
        return _isDead;
    }
    
    public void Kill()
    {
        _isDead = true;
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Kill();
        }
    }
    
}
