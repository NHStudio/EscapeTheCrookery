using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BaseEnemyClass : MonoBehaviour, IDamageable
{
    public int moveSpeed;
    public int attackDamage;
    public int hitPoints;
    public float attackRadius;
    public float followRadius;
    public bool facingRight;
    protected bool _isDead = false;
    
    protected bool CheckFollowRadius(float playerPosition, float enemyPosition)
    {
        return Mathf.Abs(playerPosition - enemyPosition) < followRadius;
    }
    
    protected bool CheckAttackRadius(float playerPosition, float enemyPosition)
    {
        return Mathf.Abs(playerPosition - enemyPosition) < attackRadius;
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

    public bool IsDead()
    {
        return _isDead;
    }
    
}
