using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelSelector = "levelSelector";
    public SceneFader sceneFader;
    
    public void Play()
    {
        sceneFader.FadeTo(levelSelector);
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
