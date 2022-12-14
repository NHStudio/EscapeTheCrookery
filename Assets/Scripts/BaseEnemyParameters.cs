using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BaseEnemyParameters : BaseActorParameters
{
    [Header("Warning: Attack Damage is set procedurally. Modify this property only while runtime for debugging purposes")]
    public int attackDamage;

    public float enemyKindDamageMultiplier = 1.0f;
    
    public override int AttackDamage => (int)(attackDamage * enemyKindDamageMultiplier);

    private new void Awake()
    {
        base.Awake();
        attackDamage = PlayerStatsManager.Instance.stats.baseEnemyDamage;
        HitPoints = PlayerStatsManager.Instance.stats.enemyBaseHealth;
    }
}