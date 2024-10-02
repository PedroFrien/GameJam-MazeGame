using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private TowerDefenseEnemyManager enemyManager;

    private float spawnSpeed;

    private Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnSpeed = enemyManager.spawnRate;


    }
}
