using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heater : MonoBehaviour
{
    [SerializeField] private float damagePerTick;
    [SerializeField] private float durability;

    [SerializeField] private GameObject damageVolume;
    [SerializeField] private Slider healthBarSlider;


    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider.maxValue = durability;
        healthBarSlider.value = durability;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesInTrigger())
        {
            durability -= 1;
            healthBarSlider.value = durability;
        }
        
        if (durability <= 0)
        {
            Die();
        }
    }

   
    private void OnTriggerStay(Collider other)
    {
        healthBarSlider.gameObject.SetActive(true);
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapVictim"))
        {
            other.gameObject.GetComponent<AI_TowerDefenseEnemy>().TakeDamage(damagePerTick);
        }
    }

    


    private bool enemiesInTrigger()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3 (transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2), Quaternion.identity);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("TrapVictim"))
            {
                return true;
            }
        }
        return false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
