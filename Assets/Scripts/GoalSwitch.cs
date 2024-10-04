using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSwitch : MonoBehaviour
{
    public GameObject characaterSpawn;
    public Camera camera1;
    public Camera camera2;

    private GameObject[] enemiesOnField;
    private Camera activeCamera;

    [SerializeField] private GameObject towerDefenseCameraRig;
    [SerializeField] private GameObject firstPersonPlayerPrefab;

    private GameObject[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        activeCamera = camera1;

        

    }

    // Update is called once per frame
    void Update()
    {
        enemiesOnField = GameObject.FindGameObjectsWithTag("Agent");

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Agent"))
        {

            ClearEnemies(enemiesOnField);
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(characaterSpawn, spawnPosition, spawnRotation);

            if (activeCamera == camera1)
            {
                SwitchCamera(camera2);
            }

            else
            {
                SwitchCamera(camera1);
            }


        }

        {

        }
    }

    void SwitchCamera(Camera newCamera)
    {
        if (activeCamera != newCamera)
        {

            DeactivateCamera(activeCamera);

            ActivateCamera(newCamera);

            activeCamera = newCamera;
        }
        if (activeCamera == camera1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void ActivateCamera(Camera cam)
    {
        cam.enabled = true;
    }

    void DeactivateCamera(Camera cam)
    {
        cam.enabled = false;
    }

    private void ClearEnemies(GameObject[] enemiesOnField)
    {
        foreach (GameObject obj in enemiesOnField)
        {
            Destroy(obj);
        }
    }

    
}
