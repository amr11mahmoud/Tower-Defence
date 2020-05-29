using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameManager gameManager;
    public SceneFader sceneFader;
    public string mainMenu = "mainMenu";

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Debug.Log(" Exiting ...");
        Application.Quit();
    }
    
    public void Options()
    {
        Debug.Log("Options");
    }

    public void Continue()
    { 
        gameManager.Resume();
    }

    public void Menu()
    {
        gameManager.Resume();
        sceneFader.FadeTo(mainMenu);
    }

}
