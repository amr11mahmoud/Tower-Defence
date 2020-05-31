using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;
    public string mainMenu = "mainMenu";
    public GameManager gameManager;
    
    

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        gameManager.Resume();
        sceneFader.FadeTo(mainMenu);
        Time.timeScale = 1;
    }
}
