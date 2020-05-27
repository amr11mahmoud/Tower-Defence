using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// use To end the game if the player died, load the game ... etc
public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;
    public GameObject gameOverUi;

    private void Start()
    {
        // we have to put reset the static variable value each time we start the scene 
        gameIsOver = false;
    }

    private void Update()
    {
        // check if game ended 
        if (!gameIsOver && PlayerStats.Lives <= 0)
        {
            EndGame(); 
        }
    }

    // method to run when the game end
    private void EndGame()
    {
        gameIsOver = true;
        // enable game over UI menu when the player died
        gameOverUi.SetActive(true);
    }
}
