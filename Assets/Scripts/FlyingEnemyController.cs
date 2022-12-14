using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : BaseActorController
{
    private BaseEnemyParameters _parameters;

    // Player
    private Transform _playerTransform;
    private PlayerParameters _playerParameters;
    private GameObject _player;
    
    public float attackRadius;
    public float followRadius;
    
    public float speed = 5f;
    public float knockback = 25f;

    public BaseEnemyParameters Parameters { get; private set; }

    protected new void Start()
    {
        base.Start();
        
        _parameters = GetComponent(typeof(BaseEnemyParameters)) as BaseEnemyParameters;

        _player = GameObject.FindWithTag("Player");
        _playerTransform = _player.GetComponent<Transform>();
        _playerParameters = _player.GetComponent<PlayerParameters>();
        Debug.Assert(_playerParameters is not null);
        
        _rb = GetComponent<Rigidbody2D>();
        
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
            bool isPlayerAbove = _playerTransform.position.y > transform.position.y + 2/*(jump height)*/;
            bool isPlayerBelow = _playerTransform.position.y < transform.position.y -  2/*(jump height)*/;
            
            if (isPlayerAbove) GoUp();
            else if (isPlayerBelow) GoDown();
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
    
    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player Bullet") && 
            !col.gameObject.CompareTag(gameObject.tag))
        {
            BulletKnockback(col);
        }
    }

    protected void Move(int x)
    {
        Debug.Assert(x is -1 or 0 or 1);
        
        var tr = transform;
        Vector3 moveForce = Time.deltaTime * speed * tr.right;

        if (x != 0)
        {
            // As int: -1 or 1
            Facing = (ActorFacing)x;
            _rb.AddForce((int)Facing * moveForce);
        }
    }
    
    protected void GoUp()
    {
        _rb.AddForce(Time.deltaTime * speed * Vector2.up);
    }
    
    protected void GoDown()
    {
        _rb.AddForce(Time.deltaTime * speed * Vector2.down);
    }

    protected void BulletKnockback(Collision2D col)
    {
        var bulletDir = col.gameObject.GetComponent<Projectile>().direction;
        HitKnockback(bulletDir);
    }

    protected void HitKnockback(Vector3 dir)
    {
        // Keep only right or left
        dir = Vector3.Project(dir, Vector3.right).normalized;
        
        Vector2 knockbackDir =
            new Vector2(dir.x > 0 ? 1.0f : -1.0f, 1.0f).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDir * knockback);
    }
    
    public override void OnDeath()
    {
        DropItem((ItemsMeta.Item)new System.Random().Next(1, 5));
        Wallet.Instance.Add(PlayerStatsManager.Instance.stats.dropAmount);
    }
}