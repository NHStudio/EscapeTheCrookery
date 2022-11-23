using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseShootingWeapon
{
    public float sideProjOffset = 0.5f;
    public float sideProjAngle = 30.0f;
    
    protected override void Fire()
    {
        bool leftShoot = _actorController.Facing == BaseActorController.ActorFacing.Left;

        Vector3 offset = new Vector3(leftShoot ? -projSpawnOffset : projSpawnOffset, 0.0f, 0.0f);
        Vector3 projSpawnPos = transform.position + offset;

        Vector3[] projSpawnPoses =
        {
            projSpawnPos,
            projSpawnPos + Vector3.up * sideProjOffset,
            projSpawnPos + Vector3.down * sideProjOffset,
        };
        
        Vector3[] angles =
        {
            Vector3.right,
            Quaternion.Euler(0, 0, sideProjAngle) * Vector3.right,
            Quaternion.Euler(0, 0, -sideProjAngle) * Vector3.right
        };

        Projectile[] projComponents = new Projectile[3];
        for (int i = 0; i < 3; i++)
        {
            GameObject projectile = Instantiate(projectileType, projSpawnPoses[i], Quaternion.identity);
            projComponents[i] = projectile.GetComponent<Projectile>();
            
            projComponents[i].speed = projSpeed;
            projComponents[i].HittingPlayer(false);

            Vector3 direction = angles[i];
            if (leftShoot)
            {
                direction.x = -direction.x;
            }

            projComponents[i].direction = direction;
        }
    }
}
