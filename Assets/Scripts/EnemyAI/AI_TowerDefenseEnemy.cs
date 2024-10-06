using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI_TowerDefenseEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private GameObject towerDefenseEnemyManager;

    public float reverseTime = 2f;
    private bool isReversing = false;

    public bool slowed = false;

    public float health = 200;
    public float maxHealth;

    public float speed;

    private string[] deathSounds = new string[] { "FigureDeath1", "FigureDeath2", "FigureDeath3", "FigureDeath4", "FigureDeath5" };
    private void Awake()
    {
        towerDefenseEnemyManager = FindObjectOfType<TowerDefenseEnemyManager>().gameObject;

        speed = towerDefenseEnemyManager.GetComponent<TowerDefenseEnemyManager>().speed;
        
        maxHealth = health;
    }
    // Update is called once per frame
    void Update()
    {
        //Checks for goal cube by searching it's name. Once found it sets that
        //point as the destination
        agent.speed = speed;

        GameObject goal = GameObject.FindWithTag("Goal");

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
    }

    private void OnTriggerEnter(Collider other)
    {
        //On collision with another object this checks to see if it has the Obstacle tag,
        //if t does it executes the reverse direction routine which is what creates the bounce
        if (other.CompareTag("Obstacle") && !isReversing)
        {

            StartCoroutine(ReverseDirection());
        }

    }

    private IEnumerator ReverseDirection()
    {
        isReversing = true;

        //grabs AI's velocity 
        Vector3 currentVelocity = agent.velocity;

        if (currentVelocity.magnitude > 0.1f)
        {

            Vector3 reverseDirection = -currentVelocity;

            agent.isStopped = true;
            agent.velocity = reverseDirection;

        }

        agent.isStopped = false;

        yield return new WaitForSeconds(reverseTime);


        isReversing = false;


    }

    public void TakeDamage(float damage)
    {
        
        health -= damage;
        healthBarSlider.GetComponent<EnemyHealthbar>().UpdateHealth(health, maxHealth);
    }

    private void Die()
    {
        //int randomIndex = Random.Range(0, deathSounds.Length);

        //GetComponent<AudioManager>().PlaySound(deathSounds[randomIndex], transform.position, gameObject);

        Destroy(gameObject);
    }
}
