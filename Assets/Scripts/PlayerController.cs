using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : GroundedActorController
{
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
        
        if (_currWeapon is not null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _currWeapon.MainAttackStart();
            }
            
            if (Input.GetButtonUp("Fire1"))
            {
                _currWeapon.MainAttackEnd();
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
}