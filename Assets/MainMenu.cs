using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "mainLevel";
    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
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
}
