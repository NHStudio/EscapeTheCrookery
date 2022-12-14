using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BaseEnemyParameters : BaseActorParameters
{
    // Warning: set procedurally
    public int attackDamage;
    
    public override int AttackDamage => attackDamage;

    private new void Awake()
    {
        base.Awake();
        attackDamage = PlayerStatsManager.Instance.stats.baseEnemyDamage;
        HitPoints = PlayerStatsManager.Instance.stats.enemyBaseHealth;
    }
}