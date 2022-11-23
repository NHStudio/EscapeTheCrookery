using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 1.0f;
    public int damage = 1;
    public GameObject owner;
    public float lifeSpan = 3.0f;

    private Collider2D _collider2D;

    public void Start()
    {
        Destroy(gameObject, lifeSpan);
        _collider2D = gameObject.GetComponent<Collider2D>();
    }
    
    public void Update()
    {
        var transformRef = transform;
        transformRef.position += Time.deltaTime * speed * direction;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        bool isPlayer = col.gameObject.CompareTag("Player");
        bool isEnemy = col.gameObject.CompareTag("Enemy");

        if (!(isPlayer || isEnemy))
        {
            Destroy(gameObject);
            return;
        }

        if (col.gameObject != owner)
        {
            BaseActorParameters actorParameters = col.gameObject.GetComponent(typeof(BaseActorParameters)) as BaseActorParameters;
            actorParameters.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Physics2D.IgnoreCollision(_collider2D, col.collider);
        }
    }
}

