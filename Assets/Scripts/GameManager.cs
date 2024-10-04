using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameTimer;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject player;

    [SerializeField] private TMP_Text gameOverTime;

    

    private float survivalTime;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Debug.Log("Dead");

        Time.timeScale = 0;

        gameOverScreen.SetActive(true);

        survivalTime = gameTimer.GetComponent<GameTimer>().time;

        gameOverTime.text = $"{ survivalTime.ToString("F0")} seconds.";

        player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<FPSController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
