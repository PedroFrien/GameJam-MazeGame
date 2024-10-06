using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[System.Serializable]
public struct TrapFigurines
{

    public string name;
    public GameObject figurinePrefab;

    [Range(0f, 20f)]
    public float spawnWeight;
}
public class TrapSpawner : MonoBehaviour
{
    public TrapFigurines[] trapFigurines;
    [SerializeField] private float startCoolDownRange;
    [SerializeField] private float endCoolDownRange;
    [SerializeField] private float dispenseDelay;

    // Start is called before the first frame update
    void OnEnable()
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
            FindObjectOfType<AudioManager>().PlaySound("TrapDrop", transform.position, gameObject);
            yield return new WaitForSeconds(dispenseDelay);
            FindObjectOfType<AudioManager>().PlaySound("TrapDispensed", transform.position, gameObject);


            GameObject selectedTrap = GetRandomTrap();
            Instantiate(selectedTrap, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(startCoolDownRange, endCoolDownRange));
        }
    }

    private GameObject GetRandomTrap()
    {
        float totalWeight = 0f; 

        foreach (TrapFigurines figurine in trapFigurines)
        {
            totalWeight += figurine.spawnWeight;
        }

        float randomValue = Random.Range(0f, totalWeight);

        float cumulativeWeight = 0f;

        foreach (TrapFigurines figure in trapFigurines)
        {
            cumulativeWeight += figure.spawnWeight;
            if (randomValue < cumulativeWeight)
            {
                return figure.figurinePrefab;
            }
        }

        return trapFigurines[0].figurinePrefab;
    }


}
