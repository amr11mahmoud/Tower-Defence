using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// use To end the game if the player died, load the game ... etc
public class GameManager : MonoBehaviour
{
    private bool gameOver = false;

    private void Update()
    {
        // check if game ended 
        if (!gameOver && PlayerStats.Lives <= 0)
        {
            EndGame(); 
        }
        
    }

    // method to run when the game end
    private void EndGame()
    {
        gameOver = true;
        Debug.Log(" Game Over ! ");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
