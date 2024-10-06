using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Hourglass : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    [SerializeField] private Slider healthBarSlider;

    [SerializeField] private GameObject towerDefenseEnemyManager;

    private float currentLifeTime;

    private GameObject[] enemies;

    
    // Start is called before the first frame update
    void Awake()


    {
        enemies = GameObject.FindGameObjectsWithTag("Agent");
        towerDefenseEnemyManager = FindObjectOfType<TowerDefenseEnemyManager>().gameObject;


        currentLifeTime = lifeSpan;
        healthBarSlider.maxValue = lifeSpan;
        healthBarSlider.value = lifeSpan;

        StartCoroutine(StartDying(lifeSpan));


    }

    // Update is called once per frame
    void Update()
    {
        HealthBarDecrease();
    }

    private IEnumerator StartDying(float deathTime)
    {
        FindObjectOfType<AudioManager>().PlaySound("PrestonHourglass", transform.position, gameObject);

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                Debug.Log("Tried to lobotomize");
                enemy.gameObject.GetComponent<AI_TowerDefenseEnemy>().enabled = false;
                enemy.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                towerDefenseEnemyManager.SetActive(false);
            }
            
        }
        yield return new WaitForSeconds(deathTime);
        foreach (var enemy in enemies)
        {
            if (enemy != null && enemy.gameObject.GetComponent<AI_TowerDefenseEnemy>() != null)
            {
                enemy.gameObject.GetComponent<AI_TowerDefenseEnemy>().enabled = true;
                enemy.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                towerDefenseEnemyManager.SetActive(true);
            }       
        }
        Die();
    }

    private void HealthBarDecrease()
    {
        if (currentLifeTime > 0)
        {
            currentLifeTime -= Time.deltaTime;
            healthBarSlider.value = currentLifeTime;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
