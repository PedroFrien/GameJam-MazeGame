using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_FirstPersonEnemy : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;

    [SerializeField] private float health = 200;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
