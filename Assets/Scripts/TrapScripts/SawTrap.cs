using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SawTrap : MonoBehaviour
{
    [SerializeField] private GameObject damageVolume;

    [SerializeField] private float raiseSpeed;
    [SerializeField] private float lowerSpeed;
    [SerializeField] private float raiseHeight;
    [SerializeField] public float damagePerTick;

    [SerializeField] public float bleedDamage;
    [SerializeField] public float bleedProc;

    [SerializeField] public float slowDuration;

    [SerializeField] private float durability;

    [SerializeField] private bool raising;

    [SerializeField] private Slider healthBarSlider;

    Vector3 originalPosition;
    Vector3 raisePosition;
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Awake()
    {
        originalPosition = damageVolume.transform.position;

        raisePosition = new Vector3(originalPosition.x, originalPosition.y + raiseHeight, originalPosition.z);

        healthBarSlider.maxValue = durability;

        healthBarSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (raising)
        {
            targetPosition = raisePosition;
            healthBarSlider.gameObject.SetActive(true);
            durability -= 1;
            raising = enemiesInTrigger();
        }
        if (!raising)
        {
            targetPosition = originalPosition;
        }

        damageVolume.transform.position = Vector3.MoveTowards(damageVolume.transform.position, targetPosition, raiseSpeed * Time.deltaTime);
        healthBarSlider.value = durability;

        if (durability <= 0)
        {
            Die();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapVictim"))
        {
            FindObjectOfType<AudioManager>().PlaySound("BuzzsawTrapStart", transform.position, gameObject);
            raising = true;
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapVictim"))
        {
            raising = false;

            FindObjectOfType<AudioManager>().PlaySound("BuzzsawTrapEnd", transform.position, gameObject);

            if (other.gameObject.GetComponent<AI_TowerDefenseEnemy>() != null && (other.gameObject.GetComponent<AI_TowerDefenseEnemy>().slowed == false))
            {
                StartCoroutine(ApplySlow(other.gameObject, slowDuration));
            }

            if (other.gameObject.GetComponent<AI_FirstPersonEnemy>() != null && (other.gameObject.GetComponent<AI_FirstPersonEnemy>().slowed == false))
            {
                StartCoroutine(ApplySlow(other.gameObject, slowDuration));
            }
            if (other.gameObject.GetComponent<FPSController>() != null && (other.gameObject.GetComponent<FPSController>().slowed == false))
            {
                StartCoroutine(ApplySlow(other.gameObject, slowDuration));
            }

        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator ApplySlow(GameObject target, float slowDuration)
    {
        if (target.gameObject != null)
        {
            if (target.GetComponent<AI_TowerDefenseEnemy>() != null)
            {
                target.GetComponent<AI_TowerDefenseEnemy>().slowed = true;
                target.GetComponent<AI_TowerDefenseEnemy>().speed = target.GetComponent<AI_TowerDefenseEnemy>().speed / 2;

                yield return new WaitForSeconds(slowDuration);

                if (target.gameObject != null)
                {
                    target.GetComponent<AI_TowerDefenseEnemy>().speed = target.GetComponent<AI_TowerDefenseEnemy>().speed * 2;
                    target.GetComponent<AI_TowerDefenseEnemy>().slowed = false;
                }
            }
            if (target.GetComponent<AI_FirstPersonEnemy>() != null)
            {
                target.GetComponent<AI_FirstPersonEnemy>().slowed = true;
                target.GetComponent<AI_FirstPersonEnemy>().agent.speed = target.GetComponent<AI_TowerDefenseEnemy>().speed / 2;

                yield return new WaitForSeconds(slowDuration);

                if (target.gameObject != null)
                {
                    target.GetComponent<AI_FirstPersonEnemy>().agent.speed = target.GetComponent<AI_TowerDefenseEnemy>().speed * 2;
                    target.GetComponent<AI_FirstPersonEnemy>().slowed = false;
                }
            }
            if (target.GetComponent<FPSController>() != null)
            {
                target.GetComponent<FPSController>().slowed = true;
                target.GetComponent<FPSController>().runSpeed = target.GetComponent<FPSController>().runSpeed / 2;
                target.GetComponent<FPSController>().walkSpeed = target.GetComponent<FPSController>().walkSpeed / 2;

                yield return new WaitForSeconds(slowDuration);

                if (target.gameObject != null)
                {
                    target.GetComponent<FPSController>().walkSpeed = target.GetComponent<FPSController>().walkSpeed * 2;
                    target.GetComponent<FPSController>().runSpeed = target.GetComponent<FPSController>().runSpeed * 2;
                    target.GetComponent<FPSController>().slowed = false;
                }
            }
        }
        




    }
    private bool enemiesInTrigger()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(damageVolume.transform.localScale.x / 2, damageVolume.transform.localScale.y / 2, damageVolume.transform.localScale.z / 2), Quaternion.identity);
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
