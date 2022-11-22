using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (!target)
            return;
        transform.position = new Vector3(target.position.x, target.position.y, -10);

    }
    
}
