using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 1.0f;
    private bool _hitsPlayer;
    private int _damage;
    public float lifeSpan = 3.0f;

    public void HittingPlayer(bool hitsPlayer)
    {
        _hitsPlayer = hitsPlayer;
        if (_hitsPlayer)
        {
            _damage =
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemyClass>().attackDamage;
        }
        else
        {
            _damage = 
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParameters>().attackDamage;
        }
    }

    public void Start()
    {
        Destroy(gameObject, lifeSpan);
    }
    
    public void Update()
    {
        var transformRef = transform;
        transformRef.position += Time.deltaTime * speed * direction;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        bool isPlayer = col.gameObject.CompareTag("Player");
        if (_hitsPlayer == isPlayer)
        {
            if (isPlayer)
            {
                PlayerParameters playerParameters = col.gameObject.GetComponent<PlayerParameters>();
                playerParameters.TakeDamage(_damage);
                if (playerParameters.IsDead())
                {
                    Destroy(col.gameObject);
                }
            } else
            {
                BaseEnemyClass enemy = col.gameObject.GetComponent<BaseEnemyClass>();
                enemy.TakeDamage(_damage);
                if (enemy.IsDead())
                {
                    Destroy(col.gameObject);
                }
            }
            Destroy(gameObject);
        }
        else
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col.collider);
        }
    }
}
