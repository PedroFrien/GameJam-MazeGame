using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporaryWall : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    [SerializeField] private Slider healthBarSlider;

    [SerializeField] private Renderer objectRenderer;

    [SerializeField] private Material sprite1;
    [SerializeField] private Material sprite2;
    [SerializeField] private Material sprite3;


    private float currentLifeTime;
    
    // Start is called before the first frame update
    void Awake()


    {
        FindObjectOfType<AudioManager>().PlaySound("TempWallStart", transform.position, gameObject);

        objectRenderer.material = sprite1;
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
        
        yield return new WaitForSeconds(deathTime / 3);
        objectRenderer.material = sprite2;
        Debug.Log("Sprite 2");
        yield return new WaitForSeconds(deathTime / 3);
        objectRenderer.material = sprite3;
        Debug.Log("Sprite 3");
        yield return new WaitForSeconds(deathTime / 3);
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

        FindObjectOfType<AudioManager>().PlaySound("TempWallEnd", transform.position, gameObject);
        Destroy(gameObject);
    }
}
