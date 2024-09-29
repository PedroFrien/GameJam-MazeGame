using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Obstacle : MonoBehaviour
{
    private NavMeshObstacle obstacle;
    // Start is called before the first frame update
    void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();

        obstacle.enabled = false;
        obstacle.carving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
            obstacle.enabled = true;
            obstacle.carving = true;

        }
    }

}
