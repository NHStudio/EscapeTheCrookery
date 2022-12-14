using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.TerrainTools;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    public int ActiveSpawnPointCount = 1;
    void Start()
    {
        var spawners = transform
            .GetComponentsInChildren<Transform>()
            .Select(x => x.GetComponent<EnemySpawnPoint>())
            .Where(x => x != null)
            .OrderBy(x => Guid.NewGuid())
            .Take(ActiveSpawnPointCount);

        var rand = new System.Random();
        foreach (var spawner in spawners)
        {
            spawner.Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
