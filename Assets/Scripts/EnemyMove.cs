using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : BaseEnemyClass
{
    
    //end
    private Transform _playerTransform;
    private PlayerParameters _playerParameters;
    private EnemyBaseGun _enemyGun;
    private SpriteRenderer _enemySR;
    private GameObject _player;

    private void Move(bool toRight)
    {
        int sign = toRight ? -1 : 1;
        transform.position += new Vector3(sign * moveSpeed * Time.deltaTime, 0f, 0f);
        // make walking animation
        _enemySR.flipX = toRight;

        }

    private void Attack(bool toRight)
    {
        // Shoot
        _enemyGun.Attack(toRight);
        _enemySR.flipX = toRight;
        // make animation for shooting
    }
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerTransform = _player.GetComponent<Transform>();
        _playerParameters = _player.GetComponent<PlayerParameters>();
        _enemySR = GetComponent<SpriteRenderer>();
        _enemyGun = GetComponent<EnemyBaseGun>();
    }
    
    void Update()
    {
        if (_isDead || _playerParameters.IsDead())
        {
            return;
        }
        if (CheckFollowRadius(_playerTransform.position.x,transform.position.x))
        {
            bool isRight = _playerTransform.position.x < transform.position.x;
            Move(isRight);
            
        }
        else
        {
            //idle animation
        }

        if (CheckAttackRadius(_playerTransform.position.x, transform.position.x))
        {
            bool isRight = _playerTransform.position.x < transform.position.x;
            Attack(isRight);
        }
    }
}