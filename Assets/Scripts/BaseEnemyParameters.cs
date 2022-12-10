using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BaseEnemyParameters : BaseActorParameters
{
    public int attackDamage;
    
    public override int AttackDamage => attackDamage;
}