using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : GroundedActorController
{
    private float _turnSmoothVelocity;
    private Vector3 _velocity;

    private PlayerParameters _playerParameters;

    protected new void Start()
    {
        base.Start();
        _playerParameters = GetComponent<PlayerParameters>();
    }

    protected new void Update()
    {
        if (_playerParameters.IsDead())
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
        base.FixedUpdate();
        
        var x = Input.GetAxisRaw("Horizontal");
        Move((int)x);
        
        if (Input.GetButton("Jump"))
        {
            Jump();
        }
    }
}