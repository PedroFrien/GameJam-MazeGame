using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private GameObject damageVolume;

    [SerializeField] private float raiseSpeed;
    [SerializeField] private float lowerSpeed;
    [SerializeField] private float raiseHeight;
    [SerializeField] private float lowerDelay;
    [SerializeField] private float raiseDelay;
    [SerializeField] public float trapDamage;
    // Start is called before the first frame update
    void Start()
    {
        damageVolume = transform.Find("DamageBox").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapVictim"))
        {
            StartCoroutine(RaiseDamageVolume(raiseSpeed, lowerSpeed, raiseHeight, lowerDelay, raiseDelay));
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
        yield return new WaitForSeconds(lowerDelay);

        while (Vector3.Distance(damageVolume.transform.position, originalPosition) > 0.01f)
        {
            damageVolume.transform.position = Vector3.MoveTowards(damageVolume.transform.position, originalPosition, lowerSpeed * Time.deltaTime);
            yield return null;
        }


    }
}
