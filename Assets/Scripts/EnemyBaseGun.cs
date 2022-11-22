using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseGun : MonoBehaviour
{
    public GameObject projectileType;
    public float shootingRate = 0.2f;
    public float projSpawnOffset = 0.5f;
    public float projSpeed = 5.0f;
    protected float _nextShootTime = 0.0f;
    
    protected virtual void Fire(bool leftShoot)
    {
        Vector3 offset = new Vector3(leftShoot ? -projSpawnOffset : projSpawnOffset, 0.0f, 0.0f);
        Vector3 projSpawnPos = transform.position + offset;

        GameObject projectile = Instantiate(projectileType, projSpawnPos, Quaternion.identity);
        Projectile projComponent = projectile.GetComponent<Projectile>();
        projComponent.HittingPlayer(true);
        projComponent.direction = leftShoot ? Vector3.left : Vector3.right;
        projComponent.speed = projSpeed;
    }

    public void Attack(bool leftShoot)
    {
        if (Time.time > _nextShootTime)
        {
            Fire(leftShoot);
            _nextShootTime = Time.time + shootingRate;
        }
    }
}
