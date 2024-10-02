using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefenseEnemyManager : MonoBehaviour
{
    public float spawnRate;
    public float speed;
    [SerializeField] private float spawnRateIncrease;
    [SerializeField] private float speedIncrease;
    [SerializeField] private float spawnRateTickRate;
    [SerializeField] private float speedTickRate;
    [SerializeField] private float initialSpawnRateDelay;
    [SerializeField] private float initialSpeedDelay;

    [SerializeField] private float baseSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(increaseSpeed(initialSpeedDelay, speedTickRate, speedIncrease));
        StartCoroutine(increaseSpawnRate(initialSpawnRateDelay, spawnRateTickRate, spawnRateIncrease));
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator increaseSpeed(float initialDelay, float tickRate, float increase)
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            speed += increase;
            yield return new WaitForSeconds(tickRate);

        }
    }

    public IEnumerator increaseSpawnRate(float initialDelay, float tickRate, float increase)
    {
        yield return new WaitForSeconds(initialDelay);

        while(true)
        {
            spawnRate += increase;
            yield return new WaitForSeconds(tickRate);
        }

    }
}
