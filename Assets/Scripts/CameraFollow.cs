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

        smoothedPosition = clampPosition(smoothedPosition);

        var newPosition = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);

        transform.position = newPosition;
    }

    private Vector2 clampPosition(Vector2 position)
    {
        // Get the width and height of the camera viewport in the world space
        var cameraWidth = camera.orthographicSize * camera.aspect;
        var cameraHeight = camera.orthographicSize;

        var cameraSize = new Vector2(cameraWidth, cameraHeight) * 2;

        // Iterate through active camera limiters
        foreach (var limiter in CameraLimiter.GetAllLimiters())
        {
            position = limiter.Clamp(position, cameraSize);
        }

        return position;
    }

    public void teleportInstantly()
    {
        cameraVelocity = Vector2.down;
        var position = target.position;
        position = clampPosition(position);
        transform.position = new Vector3(position.x, position.y, -10);
    }
}
