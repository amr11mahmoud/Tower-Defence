using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// use To end the game if the player died, load the game ... etc
public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;
    public static bool gameIsPaused;
    public GameObject gameOverUi;
    public GameObject pauseMenu;

    public GameObject winMenu;
    

    public SceneFader fader;
    

    private void Start()
    {
        // we have to put reset the static variable value each time we start the scene 
        gameIsOver = false;
        gameIsPaused = false;
        waveSpawner.EnemiesAlive = 0;
    }

    private void Update()
    {
        // check if game ended 
        if (!gameIsOver && PlayerStats.Lives <= 0)
        {
            EndGame(); 
        }
        
        // show pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsOver)
            {
                return;
            }
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        gameIsPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        gameIsPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    // method to run when the game end
    private void EndGame()
    {
        gameIsOver = true;
        // enable game over UI menu when the player died
        gameOverUi.SetActive(true);
    }
    
    public void winLevel()
    {
        gameIsOver = true;
        winMenu.SetActive(true);
    }
    

}
