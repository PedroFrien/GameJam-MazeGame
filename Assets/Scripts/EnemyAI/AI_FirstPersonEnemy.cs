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

    public bool slowed = false;
    private bool alertSoundAvailable;
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
        goal = GameObject.FindWithTag("Player");

        if (goal != null)
        {
            Transform goalTransform = goal.transform;
            Vector3 goalPosition = goalTransform.position;
            agent.SetDestination(goalPosition);

            
        }


        

        if (SeesPlayer())
        {
            agent.speed = speed + chaseSpeed;
            FindObjectOfType<AudioManager>().PlaySound("FigureSlideFar", transform.position, gameObject);
        }
        if (!SeesPlayer())
        {
            agent.speed = speed;
            FindObjectOfType<AudioManager>().PlaySound("FigureSlideDanger", transform.position, gameObject);
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
            gameManager.GetComponent<GameManager>().Die(true);
        }
    }

    private bool SeesPlayer()
    {
        Vector3 directionToTarget = goal.transform.position - transform.position;

        if (Physics.Raycast(transform.position, directionToTarget.normalized, out RaycastHit hit, 100))
        {
            return hit.transform == goal.transform;
        }

        return true;

        if (alertSoundAvailable)
        {

        }
    }

    public void TakeDamage(float damage)
    {


        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator PlayAlertSound()
    {
        alertSoundAvailable = false;
        FindObjectOfType<AudioManager>().PlaySound("EnemyAlert", transform.position, gameObject);
        yield return new WaitForSeconds(10);
        alertSoundAvailable = true;
    }
}
