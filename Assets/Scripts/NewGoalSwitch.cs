using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class NewGoalSwitch : MonoBehaviour
{
    private GameObject[] spawnPoints;

    [SerializeField] private GameObject firstPersonPlayerPrefab;

    [SerializeField] private bool firstPerson;

    [SerializeField] private GameObject towerDefenseCameraRig;

    [SerializeField] private GameObject TowerDefenseEnemyManager;

    private GameObject[] enemies;

    private GameObject player;

    [SerializeField] private float spawnOffset;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawner");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("GoalSwitchTest"))
        //    {
        //        Debug.Log("Switching Goals");
        //        SwitchGoals();

        //    }
        //}
        // FOR TESTING PURPOSES
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TrapVictim"))
        {
            SwitchGoals();
        }
    }
    private GameObject FindSpawnPoint()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        GameObject spawnPoint = spawnPoints[spawnPointIndex];

        return spawnPoint;

    }

    public void SwitchGoals()
    {
        if (!firstPerson)
        {
            CleanEnemies();

            TowerDefenseEnemyManager.gameObject.SetActive(false);

            GameObject spawnPoint = FindSpawnPoint();

            Vector3 offset = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + spawnOffset, spawnPoint.transform.position.z);

            player = Instantiate(firstPersonPlayerPrefab, offset, Quaternion.identity);
            Camera playerCamera = player.transform.Find("FP Camera").GetComponent<Camera>();

            towerDefenseCameraRig.gameObject.SetActive(false);
            playerCamera.enabled = true;

            firstPerson = true;
        }

        else
        {
            CleanEnemies();

            TowerDefenseEnemyManager.gameObject.SetActive(true);

            Destroy(player);

            towerDefenseCameraRig.gameObject.SetActive(true);

            firstPerson = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    private void CleanEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Agent");

        foreach (GameObject obj in enemies)
        {
            Destroy(obj);
        }
    }

}
