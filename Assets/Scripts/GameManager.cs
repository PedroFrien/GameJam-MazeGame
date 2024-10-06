using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameTimer;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject jumpscareScreen;
    [SerializeField] private GameObject jumpscarePlayer;
    [SerializeField] private GameObject playerHealthbar;

    [SerializeField] private TMP_Text gameOverTime;

    

    private float survivalTime;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die(bool enemyKill)
    {
        Debug.Log("Dead");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Agent");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        StartCoroutine(JumpScare(enemyKill));
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator JumpScare(bool enemyKill)
    {
        if (enemyKill)
        {
            jumpscarePlayer.SetActive(true);
            jumpscareScreen.SetActive(true);
            FindObjectOfType<AudioManager>().PlaySound("Jumpscare", player.transform.position, player);

            yield return new WaitForSeconds(4.5f);

            Destroy(jumpscarePlayer);
            Destroy(jumpscareScreen);
        }

        playerHealthbar = player.transform.Find("Healthbar").gameObject;

        playerHealthbar.SetActive(false);

        Time.timeScale = 0;

        gameOverScreen.SetActive(true);

        survivalTime = gameTimer.GetComponent<GameTimer>().time;

        gameOverTime.text = $"{survivalTime.ToString("F0")} seconds.";

        player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<FPSController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        FindObjectOfType<AudioManager>().PlaySound("DeathSting", player.transform.position, player);
    }
}
