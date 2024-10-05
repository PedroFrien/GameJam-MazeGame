using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private GameObject damageVolume;
    [SerializeField] private Slider healthBarSlider;

    [SerializeField] private float raiseSpeed;
    [SerializeField] private float lowerSpeed;
    [SerializeField] private float raiseHeight;
    [SerializeField] private float lowerDelay;
    [SerializeField] private float raiseDelay;
    [SerializeField] public float trapDamage;
    [SerializeField] private float trapCooldown;
    [SerializeField] private float trapDurability;

    private float currentCooldownTime;

    [SerializeField] public bool onCooldown = false;
    private bool durabilityCoolDown = false;
    // Start is called before the first frame update
    void Start()
    {
        damageVolume = transform.Find("DamageBox").gameObject;

        healthBarSlider.maxValue = trapCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (onCooldown)
        {
            HealthBarDecrease();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapVictim") && (!onCooldown))
        {
            Debug.Log("RaiseDamageVolume Called");
            if (!durabilityCoolDown)
            {
                trapDurability -= 1;
            }
            StartCoroutine(RaiseDamageVolume(raiseSpeed, lowerSpeed, raiseHeight, lowerDelay, raiseDelay));
            durabilityCoolDown = true;
            
        }
    }

    private IEnumerator RaiseDamageVolume(float raiseSpeed, float lowerSpeed, float raiseHeight, float lowerDelay, float raiseDelay)
    {
        
        Vector3 targetPosition = new Vector3(damageVolume.transform.position.x, damageVolume.transform.position.y + raiseHeight, damageVolume.transform.position.z);
        Vector3 originalPosition = damageVolume.transform.position;
        yield return new WaitForSeconds(raiseDelay);
        while (Vector3.Distance(damageVolume.transform.position, targetPosition) > 0.01f)
        {
            damageVolume.transform.position = Vector3.MoveTowards(damageVolume.transform.position, targetPosition, raiseSpeed * Time.deltaTime);
            yield return null;
        }
        onCooldown = true;
        yield return new WaitForSeconds(lowerDelay);

        while (Vector3.Distance(damageVolume.transform.position, originalPosition) > 0.01f)
        {
            damageVolume.transform.position = Vector3.MoveTowards(damageVolume.transform.position, originalPosition, lowerSpeed * Time.deltaTime);
            yield return null;
        }
        durabilityCoolDown = false;
        if (trapDurability == 0)
        {
            Die();
        }


    }
    public void StartCoolDown()
    {
        StartCoroutine(CoolDown(trapCooldown));
        
    }

    private IEnumerator CoolDown(float cooldown)
    {
        if (trapDurability != 0)
        {
            healthBarSlider.gameObject.SetActive(true);
        }
        onCooldown = true;

        currentCooldownTime = cooldown;

        yield return new WaitForSeconds(cooldown);
        
        onCooldown = false;
        healthBarSlider.gameObject.SetActive(false);
        healthBarSlider.value = cooldown;

        
    }

    private void HealthBarDecrease()
    {
        if (currentCooldownTime > 0)
        {
            currentCooldownTime -= Time.deltaTime;
            healthBarSlider.value = currentCooldownTime;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
