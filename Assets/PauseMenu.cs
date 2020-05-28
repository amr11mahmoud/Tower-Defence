using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameManager gameManager;
    
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        Debug.Log("Open Menu");
    }

}
