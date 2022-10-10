using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject spawnPoint;
    public GameObject enemyPrefab;
    public float secondsBetweenSpawns;
    float secondsSinceLastSpawn;

    public int enemiesToSpawn;

    private void OnEnable()
    {
        References.spawners.Add(this);
    }

    private void OnDisable()
    {
        References.spawners.Remove(this);
    }

  
    void Start()
    {
      secondsSinceLastSpawn = 0;
    }

  
    private void FixedUpdate()
    {
        
    }
}
