using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap_DamageBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object entered damageBox");
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapVictim"))
        {
            other.GetComponent<AI_TowerDefenseEnemy>().TakeDamage(100);
        }
    }
}
