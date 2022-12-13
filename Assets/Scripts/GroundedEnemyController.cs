using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnemyController : GroundedActorController
{
    private BaseEnemyParameters _parameters;

    public enum AIState
    {
        Idle,
        Chase,
        Attack,
        Runaway
    }

    // Player
    private Transform _playerTransform;
    private PlayerParameters _playerParameters;
    private GameObject _player;
    
    public float attackRadius;
    public float followRadius;

    public BaseEnemyParameters Parameters { get; private set; }

    protected new void Start()
    {
        base.Start();
        
        _parameters = GetComponent(typeof(BaseEnemyParameters)) as BaseEnemyParameters;

        _player = GameObject.FindWithTag("Player");
        _playerTransform = _player.GetComponent<Transform>();
        _playerParameters = _player.GetComponent<PlayerParameters>();
        Debug.Assert(_playerParameters is not null);
        
        MainWeapon = GetComponent(typeof(IWeapon)) as IWeapon;
    }
    
    protected new void Update()
    {
        if (_parameters.Dead || _playerParameters.Dead)
        {
            return;
        }
        
        base.Update();
        
        if (CheckFollowRadius(_playerTransform.position.x,transform.position.x))
        {
            bool isPlayerOnRight = _playerTransform.position.x < transform.position.x;
            Move(isPlayerOnRight ? -1 : 1);
        }
        else
        {
            Move(0);
        }

        if (MainWeapon is not null)
        {
            if (CheckAttackRadius(_playerTransform.position.x, transform.position.x))
            {
                MainWeapon.MainAttackStart();
            }
            else
            {
                MainWeapon.MainAttackEnd();
            }   
        }
    }
    
    protected bool CheckFollowRadius(float playerPosition, float enemyPosition)
    {
        return Mathf.Abs(playerPosition - enemyPosition) < followRadius;
    }
    
    protected bool CheckAttackRadius(float playerPosition, float enemyPosition)
    {
        return Mathf.Abs(playerPosition - enemyPosition) < attackRadius;
    }
}