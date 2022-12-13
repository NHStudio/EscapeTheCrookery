using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector2 cameraVelocity;

    private Camera camera;
    
    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (!target)
            return;
        
        Vector2 desiredPosition = target.position;
        var smoothedPosition = Vector2.SmoothDamp(transform.position, desiredPosition, ref cameraVelocity, smoothSpeed);
        
        // Get the width and height of the camera viewport in the world space
        var cameraWidth = camera.orthographicSize * camera.aspect;
        var cameraHeight = camera.orthographicSize;

        var cameraSize = new Vector2(cameraWidth, cameraHeight) * 2;

        // Iterate through active camera limiters
        foreach (var limiter in CameraLimiter.GetAllLimiters())
        {
            smoothedPosition = limiter.Clamp(smoothedPosition, cameraSize);
        }
        
        var newPosition = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);
        
        transform.position = newPosition;
    }
    
}
