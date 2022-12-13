using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerParameters : BaseActorParameters
{
    public override int AttackDamage => PlayerStatsManager.Instance.stats.baseDamage;
}