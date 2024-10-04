using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerDefenseEnemyManager : MonoBehaviour
{
    [Header("Spawn Rate")]
    public float spawnCooldown;
    [SerializeField] private float spawnCooldownDecrease;
    [SerializeField] private float spawnCooldownTickRate;
    [SerializeField] private float initialSpawnDelay;
    [SerializeField] private float minSpawnCooldown;

    [Header("Speed")]
    public float speed;
    [SerializeField] private float speedIncrease;
    [SerializeField] private float speedTickRate;    
    [SerializeField] private float initialSpeedDelay; 
    [SerializeField] private float maxSpeed;


    [SerializeField] private GameObject[] enemySpawners;
    [SerializeField] private GameObject towerDefenseEnemy;
    

    // Start is called before the first frame update
    void Start()

    {
        enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
        StartCoroutine(increaseSpeed(initialSpeedDelay, speedTickRate, speedIncrease));
        StartCoroutine(increaseSpawnRate(initialSpawnDelay, spawnCooldownTickRate, spawnCooldownDecrease));
        StartCoroutine(ChooseRandomSpawner());


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator increaseSpeed(float initialDelay, float tickRate, float increase)
    {
        yield return new WaitForSeconds(initialDelay);

        while (speed + increase <= maxSpeed)
        {
            speed += increase;
            yield return new WaitForSeconds(tickRate);

        }
        speed = maxSpeed;
    }

    public IEnumerator increaseSpawnRate(float initialDelay, float tickRate, float decrease)
    {
        yield return new WaitForSeconds(initialDelay);

        while(spawnCooldown - decrease >= minSpawnCooldown)
        {
            spawnCooldown -= decrease;
            yield return new WaitForSeconds(tickRate);
        }

        spawnCooldown = minSpawnCooldown;

    }

    
    private IEnumerator ChooseRandomSpawner()
    {
        if (enemySpawners.Length != 0)
        {
            while(true)
            {
                int randomIndex = Random.Range(0, enemySpawners.Length);
                GameObject selectedSpawner = enemySpawners[randomIndex];
                SpawnEnemy(towerDefenseEnemy, selectedSpawner);
                yield return new WaitForSeconds(spawnCooldown);
            }
            
        }
    }

    private void SpawnEnemy(GameObject enemy, GameObject spawner)
    {
        Transform spawnPoint = spawner.transform.GetChild(0);
        Instantiate(enemy, spawnPoint.position, Quaternion.identity);
    }
}
