using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : MonoBehaviour
{
    [SerializeField] private float damagePerTick;
    [SerializeField] private float durability;
    [SerializeField] private GameObject damageVolume;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesInTrigger())
        {
            durability -= 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger Entered!");
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapVictim"))
        {
            other.GetComponent<AI_TowerDefenseEnemy>().TakeDamage(damagePerTick);
        }
    }


    private bool enemiesInTrigger()
    {
        Collider[] colliders = Physics.OverlapBox(damageVolume.transform.position, damageVolume.transform.localScale, Quaternion.identity);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("TrapVictim"))
            {
                
                return true;
            }
        }
        return false;
    }
}
