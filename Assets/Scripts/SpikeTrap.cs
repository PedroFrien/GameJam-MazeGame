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
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapVictim"))
        {
            //StartCoroutine(RaiseDamageVolume(raiseSpeed, lowerSpeed, raiseHeight, lowerDelay));
        }
    }

    //private IEnumerator RaiseDamageVolume(float raiseSpeed, float lowerSpeed, float raiseHeight, float lowerDelay)
    //{
    //    while (damageVolume.transform.position.y < raiseHeight)
    //    {
    //        Vector3 newPosition = new Vector3(damageVolume.transform.position.x, damageVolume.transform.position.y, damageVolume.transform.position.z);
    //        newPosition.y += raiseSpeed;
    //    }
    //    yield return new WaitForSeconds(lowerDelay);
    //    while (damageVolume.transform.position.y > 0)
    //    {
    //        Vector3 newPosition = new Vector3(damageVolume.transform.position.x, damageVolume.transform.position.y, damageVolume.transform.position.z);
    //        newPosition.y -= lowerSpeed;
    //    }
    //}
}
