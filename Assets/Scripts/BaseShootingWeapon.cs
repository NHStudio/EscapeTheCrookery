using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShootingWeapon : MonoBehaviour, IWeapon
{
    public GameObject projectileType;
    public float shootingRate = 0.2f;
    public float projSpawnOffset = 0.5f;
    public float projSpeed = 5.0f;
    public bool IsFiring { get; set; } = false;

    protected float _nextShootTime = 0.0f;

    protected PlayerController _playerController;

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFiring && Time.time > _nextShootTime)
        {
            Fire();
            _nextShootTime = Time.time + shootingRate;
        }
    }

    protected virtual void Fire()
    {
        bool leftShoot = _playerController.Facing == PlayerController.PlayerFacing.Left;
        
        Vector3 offset = new Vector3(leftShoot ? -projSpawnOffset : projSpawnOffset, 0.0f, 0.0f);
        Vector3 projSpawnPos = transform.position + offset;

        GameObject projectile = Instantiate(projectileType, projSpawnPos, Quaternion.identity);
        Projectile projComponent = projectile.GetComponent<Projectile>();
        
        projComponent.direction = leftShoot ? Vector3.left : Vector3.right;
        projComponent.speed = projSpeed;
    }

    public virtual void MainAttackStart()
    {
        IsFiring = true;
    }

    public virtual void MainAttackEnd()
    {
        IsFiring = false;
    }
}
