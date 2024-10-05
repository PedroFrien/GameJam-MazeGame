using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_FirstPersonEnemy : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;

    [SerializeField] private float health = 200;
    [SerializeField] private float chaseSpeed;

    private float speed;

    private GameObject firstPersonEnemyManager;

    private GameObject gameManager;
    private GameObject goal;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    void Awake()
    {
        firstPersonEnemyManager = FindObjectOfType<TowerDefenseEnemyManager>().gameObject;

        speed = firstPersonEnemyManager.GetComponent<TowerDefenseEnemyManager>().speed;

        agent.speed = speed;
    }
    // Update is called once per frame
    void Update()
    {
        //Checks for goal cube by searching it's name. Once found it sets that
        //point as the destination
        GameObject goal = GameObject.FindWithTag("Player");

        if (goal != null)
        {
            Transform goalTransform = goal.transform;
            Vector3 goalPosition = goalTransform.position;
            agent.SetDestination(goalPosition);

        }


        if (health <= 0)
        {
            Die();
        }

        if (SeesPlayer())
        {
            agent.speed = speed + chaseSpeed;
        }
        if (!SeesPlayer())
        {
            agent.speed = speed;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().Die();
        }
    }

    private bool SeesPlayer()
    {
        Debug.Log("You are being watched...");

        Vector3 directionToTarget = (goal.transform.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit))
        {
            return hit.transform == goal;
        }

        return true;

        
    }
}
