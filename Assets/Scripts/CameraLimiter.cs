using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimiter : MonoBehaviour
{
    // Static array of all the camera limiters in the scene
    private static List<CameraLimiter> allLimiters = new();

    private void Awake()
    {
        // Add this limiter to the static array
        allLimiters.Add(this);
    }
    
    private void OnDestroy()
    {
        // Remove this limiter from the static array
        allLimiters.Remove(this);
    }

    public Vector2 Clamp(Vector2 vector, Vector2 space)
    {
        var tr = transform;
        var pos = tr.position;

        // Get the world-space size of the limiter
        var size = tr.lossyScale;

        // Clamp the vector to the bounds of this limiter
        vector.x = Mathf.Clamp(vector.x, pos.x - size.x / 2 + space.x / 2, pos.x + size.x / 2 - space.x / 2);
        vector.y = Mathf.Clamp(vector.y, pos.y - size.y / 2 + space.y / 2, pos.y + size.y / 2 - space.y / 2);

        return vector;
    }
    
    public static List<CameraLimiter> GetAllLimiters()
    {
        return allLimiters;
    }
}
