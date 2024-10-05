using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap_DamageBox : MonoBehaviour
{
    [SerializeField] private GameObject SpikeTrapMain;
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = SpikeTrapMain.GetComponent<SpikeTrap>().trapDamage;
}

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapVictim") && SpikeTrapMain.GetComponent<SpikeTrap>().onCooldown == false)
        {
            StartCoroutine(DealDamage(damage, other));
        }
    }

    private IEnumerator DealDamage(float damage, Collider entity)
    {
        if (entity.GetComponent<AI_TowerDefenseEnemy>() != null)
        {
            entity.GetComponent<AI_TowerDefenseEnemy>().TakeDamage(damage);
        }
        if (entity.GetComponent<FPSController>() != null)
        {
            entity.GetComponent<FPSController>().TakeDamage(damage);
        }
        if (entity.GetComponent<AI_FirstPersonEnemy>() != null)
        {
            entity.GetComponent<AI_FirstPersonEnemy>().TakeDamage(damage);
        }
        yield return new WaitForSeconds((float)0.05);
        SpikeTrapMain.GetComponent<SpikeTrap>().StartCoolDown();
    }
}
