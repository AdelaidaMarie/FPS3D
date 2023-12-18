using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public int Score = 0;
    public bool gamePaused;
    public static GameManager instance;
    public GameObject pauseText;
    public GameObject scene;
    private void Awake()
    {
        instance = this;

        Time.timeScale = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseText.SetActive(true);
            UpdateGamePause();
        }
        
    }
    //Pause system
    public void UpdateGamePause()
    {
        gamePaused = !gamePaused;
        if (!gamePaused)
        {
            scene.SetActive(true);
            pauseText.SetActive(false);
        }
        else
        {
            scene.SetActive(false);
            pauseText.SetActive(true);
        }

        Time.timeScale = (gamePaused) ? 0.0f : 1f;

        Cursor.lockState = (gamePaused) ? CursorLockMode.None : CursorLockMode.Locked;


    }
    //Update the system score
    public void UpdateScore(int points)
    {
       Score += points;
       HUDController.instance.scoreText.text = "Coins: " + Score;
    }
    
}
