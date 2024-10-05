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

    public void Die()
    {
        Debug.Log("Dead");

        

        StartCoroutine(JumpScare());
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator JumpScare()
    {
        jumpscarePlayer.SetActive(true);
        jumpscareScreen.SetActive(true);

        yield return new WaitForSeconds(3);

        Destroy(jumpscarePlayer);
        Destroy(jumpscareScreen);

        Time.timeScale = 0;

        gameOverScreen.SetActive(true);

        survivalTime = gameTimer.GetComponent<GameTimer>().time;

        gameOverTime.text = $"{survivalTime.ToString("F0")} seconds.";

        player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<FPSController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        FindObjectOfType<AudioManager>().PlaySound("PrestonHourglass");
    }
}
