using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyManager;
    [SerializeField] private GameObject enemyPrefab;

    private float spawnSpeed;

    private Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Trying to spawn enemies");
        StartCoroutine("SpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private IEnumerator SpawnEnemies()
    {
        Debug.Log("Couroutine Started");
        while (true)
        {
            Debug.Log("atempted to Spawn Enemy");
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(enemyManager.GetComponent<TowerDefenseEnemyManager>().spawnCooldown);
        }

        
    }
}
