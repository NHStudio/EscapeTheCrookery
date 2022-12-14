using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemyReference;
    private GameObject _spawnedEnemy;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Spawn()
    {
        _spawnedEnemy = Instantiate(enemyReference);
        _spawnedEnemy.transform.position = gameObject.transform.position;
    }
}
