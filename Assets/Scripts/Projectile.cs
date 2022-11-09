using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 1.0f;

    // Update is called once per frame
    public void Update()
    {
        var transformRef = transform;
        transformRef.position = transformRef.position + Time.deltaTime * speed * direction;
    }
}
