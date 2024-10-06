using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class NewGoalSwitch : MonoBehaviour
{
    private GameObject[] spawnPoints;

    [SerializeField] private GameObject firstPersonPlayerPrefab;

    [SerializeField] private bool firstPerson;

    [SerializeField] private GameObject towerDefenseCameraRig;

    [SerializeField] private GameObject towerDefenseEnemyManager;

    [SerializeField] private GameObject firstPersonEnemyManager;

    [SerializeField] private GameObject gameTimer;

    [SerializeField] private GameObject newTrapManager;

    [SerializeField] private GameObject trapSpawner;

    [SerializeField] private GameObject grid;

    [SerializeField] private GameObject trapButtons;

    [SerializeField] private GameObject trapCollection;

    [SerializeField] private GameObject scaries;

    [SerializeField] private GameObject lighting;

    [SerializeField] private Camera crystalCam;
    private GameObject[] enemies;
    private GameObject[] healthBars;

    private GameObject player;

    [SerializeField] private float spawnOffset;

    [SerializeField] private GameObject towerDefenseTutorial;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawner");

        towerDefenseTutorial.gameObject.SetActive(true);

        scaries.gameObject.SetActive(false);
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
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            towerDefenseTutorial.gameObject.SetActive(false);
        }

        
    }

    public void StartGoalSwap()
    {
        StartCoroutine(SwitchGoals());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agent") && (!firstPerson))
        {
            StartCoroutine(SwitchGoals());
        }
        if (other.gameObject.CompareTag("Player") && (firstPerson))
        {
            StartCoroutine(SwitchGoals());
        }
    }
    private GameObject FindSpawnPoint()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        GameObject spawnPoint = spawnPoints[spawnPointIndex];

        return spawnPoint;

    }

    public IEnumerator SwitchGoals()
    {

        CleanEnemies();

        healthBars = GameObject.FindGameObjectsWithTag("HealthBar");

        FindObjectOfType<AudioManager>().PlaySound("GoalSwap", transform.position, gameObject);


        if (!firstPerson)
        {
            towerDefenseCameraRig.gameObject.SetActive(false);
            crystalCam.gameObject.SetActive(true);

            yield return new WaitForSeconds(5);
            //FindObjectOfType<AudioManager>().PlaySound("")

            crystalCam.gameObject.SetActive(false);
            towerDefenseEnemyManager.gameObject.SetActive(false);

            GameObject spawnPoint = FindSpawnPoint();

            Vector3 offset = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + spawnOffset, spawnPoint.transform.position.z);

            player = Instantiate(firstPersonPlayerPrefab, offset, Quaternion.identity);
            Camera playerCamera = player.transform.Find("FP Camera").GetComponent<Camera>();

            towerDefenseCameraRig.gameObject.SetActive(false);
            firstPersonEnemyManager.gameObject.SetActive(true);
            gameTimer.SetActive(false);

            playerCamera.enabled = true;

            firstPerson = true;

            newTrapManager.gameObject.SetActive(false);

            trapSpawner.gameObject.SetActive(false);

            grid.gameObject.SetActive(false);

            trapButtons.gameObject.SetActive(false);

            RemoveHealthBars();

            scaries.gameObject.SetActive(true);

            lighting.gameObject.SetActive(false);

            

        }

        else
        {
            towerDefenseEnemyManager.gameObject.SetActive(true);

            Destroy(player);

            towerDefenseCameraRig.gameObject.SetActive(true);
            firstPersonEnemyManager.gameObject.SetActive(false);
            gameTimer.SetActive(true);

            firstPerson = false;

            newTrapManager.gameObject.SetActive(true);

            trapSpawner.gameObject.SetActive(true);

            trapButtons.gameObject.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            EnableHealthBars();

            scaries.gameObject.SetActive(false);

            lighting.gameObject.SetActive(true);
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

    private void RemoveHealthBars()
    {
        foreach (var healthBar in healthBars)
        {
            healthBar.gameObject.SetActive(false);
        }
    }

    private void EnableHealthBars()
    {
        healthBars = GameObject.FindGameObjectsWithTag("HealthBar");

        foreach (Transform trap in trapCollection.transform)
        {
            foreach (Transform item in trap.gameObject.transform)
            {
                if (item.name == "HealthBarFunctionality")
                {
                    item.gameObject.SetActive(true);
                }
            }
        }
    }

}
