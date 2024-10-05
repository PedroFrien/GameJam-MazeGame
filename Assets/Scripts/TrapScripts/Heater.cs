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

    [SerializeField] private bool isFiring;

    [SerializeField] private Light areaLight;

    private float areaLightIntensity;
    private float areaLightStartingIntensity;

    [SerializeField] private float areaLight_IntensityIncrease;

    // Start is called before the first frame update
    void Awake()
    {
        healthBarSlider.maxValue = durability;
        healthBarSlider.value = durability;

        areaLightStartingIntensity = areaLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            durability -= 1;
            healthBarSlider.value = durability;
            isFiring = enemiesInTrigger();

            areaLight.intensity += areaLight_IntensityIncrease;

            FindObjectOfType<AudioManager>().PlaySound("HeaterRepeat", transform.position, gameObject);
        }

        if (!isFiring)
        {
            areaLight.intensity = areaLightStartingIntensity;
        }
        
        if (durability <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().PlaySound("HeaterStart", transform.position, gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        FindObjectOfType<AudioManager>().PlaySound("HeaterEnd", transform.position, gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        healthBarSlider.gameObject.SetActive(true);
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapVictim"))
        {
            if (other.gameObject.GetComponent<AI_TowerDefenseEnemy>() != null)
            {
                other.gameObject.GetComponent<AI_TowerDefenseEnemy>().TakeDamage(damagePerTick);
            }
            if (other.gameObject.GetComponent<FPSController>() != null)
            {
                other.gameObject.GetComponent<FPSController>().TakeDamage(damagePerTick);
            }
            if (other.gameObject.GetComponent<AI_FirstPersonEnemy>() != null)
            {
                other.gameObject.GetComponent<AI_FirstPersonEnemy>().TakeDamage(damagePerTick);
            }


        }
        isFiring = true;
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
