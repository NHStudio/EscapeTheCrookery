using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : GroundedActorController
{
    public GameObject droppedItemPrefab;
    public float dropDistance;
    
    private float _turnSmoothVelocity;
    private Vector3 _velocity;

    public PlayerParameters Parameters { get; private set; }

    protected new void Start()
    {
        base.Start();
        Parameters = GetComponent<PlayerParameters>();
    }

    protected new void Update()
    {
        if (Parameters.Dead)
        {
            return;
        }
        
        base.Update();
        
        if (MainWeapon is not null)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                MainWeapon.MainAttackStart();
            }
            
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                MainWeapon.MainAttackEnd();
            }
        }
        
        if (SecondaryWeapon is not null)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                SecondaryWeapon.MainAttackStart();
            }
            
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                SecondaryWeapon.MainAttackEnd();
            }
        }
    }

    protected new void FixedUpdate()
    {
        if (Parameters.Dead)
        {
            return;
        }
        
        base.FixedUpdate();
        
        var x = Input.GetAxisRaw("Horizontal");
        Move((int)x);
        
        if (Input.GetButton("Jump"))
        {
            Jump();
        }
    }

    public override bool TakeItem(ItemsMeta.Item item)
    {
        return InventoryManager.Instance.Store(item);
    }

    public void DropItem(ItemsMeta.Item item)
    {
        Vector3 dropOffset = new Vector3(Facing == ActorFacing.Right ? 1.0f : -1.0f, 0.0f, 0.0f) * dropDistance;
        GameObject droppedItem = Instantiate(droppedItemPrefab, transform.position + dropOffset, Quaternion.identity);
        DroppedItem itemComponent = droppedItem.GetComponent<DroppedItem>();
        itemComponent.StoredItem = item;
    }
}