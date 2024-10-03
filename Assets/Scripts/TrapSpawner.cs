using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] trapFigurines;
    [SerializeField] private float startCoolDownRange;
    [SerializeField] private float endCoolDownRange;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnTraps());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnTraps()
    {
        while (true)
        {
            GameObject selectedTrap = trapFigurines[Random.Range(0, trapFigurines.Length)];
            Instantiate(selectedTrap, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(startCoolDownRange, endCoolDownRange));
        }
    }


}
