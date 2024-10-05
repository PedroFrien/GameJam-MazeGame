using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrapDamageVolume : MonoBehaviour
{
    private float damagePerTick;
    private float bleedDamage;
    private float bleedProc;
    private float slowDuration;
    
    // Start is called before the first frame update
    void Awake()
    {
        damagePerTick = transform.GetComponentInParent<SawTrap>().damagePerTick;
        bleedDamage = transform.GetComponentInParent<SawTrap>().bleedDamage;
        bleedProc = transform.GetComponentInParent<SawTrap>().bleedProc;
        slowDuration = transform.GetComponentInParent<SawTrap>().slowDuration;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(damagePerTick);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger Staying");
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapVictim"))
        {
            FindObjectOfType<AudioManager>().PlaySound("BuzzsawTrapRepeat", transform.position, gameObject);

            if (other.gameObject.GetComponent<AI_TowerDefenseEnemy>() != null)
            {
                Debug.Log("Dealing Damage");
                other.gameObject.GetComponent<AI_TowerDefenseEnemy>().TakeDamage(damagePerTick);
                StartCoroutine(ApplyBleed(other.gameObject, bleedProc, bleedDamage));
                
            }

            if (other.gameObject.GetComponent<FPSController>() != null)
            {
                other.gameObject.GetComponent<FPSController>().TakeDamage(damagePerTick);
                StartCoroutine(ApplyBleed(other.gameObject, bleedProc, bleedDamage));
            }

            if (other.gameObject.GetComponent<AI_FirstPersonEnemy>() != null)
            {
                other.gameObject.GetComponent<AI_FirstPersonEnemy>().TakeDamage(damagePerTick);
                StartCoroutine(ApplyBleed(other.gameObject, bleedProc, bleedDamage));
            }

        }
    }

    

    private IEnumerator ApplyBleed(GameObject target, float bleedProc, float bleedDamage)
    {
        if (target.GetComponent<AI_TowerDefenseEnemy>() != null || target.GetComponent<FPSController>() != null || target.GetComponent<AI_FirstPersonEnemy>() != null)
        {
            Debug.Log("Applying Bleed");
            yield return new WaitForSeconds(bleedProc);
            if (target != null )
            {
                if (target.GetComponent<AI_TowerDefenseEnemy>() != null)
                {
                    target.GetComponent<AI_TowerDefenseEnemy>().TakeDamage(bleedDamage);
                }

                if (target.GetComponent<FPSController>() != null)
                {
                    target.GetComponent<FPSController>().TakeDamage(bleedDamage);
                }

                if (target.GetComponent<AI_FirstPersonEnemy>() != null)
                {
                    target.GetComponent<AI_FirstPersonEnemy>().TakeDamage(bleedDamage);
                }
                
            }
            
        }
    }
}
