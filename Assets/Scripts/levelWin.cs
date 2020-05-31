using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class levelWin : MonoBehaviour
{
    public  SceneFader sceneFader;
    public string mainMenu = "mainMenu";
    public GameManager gameManager;
    
    // Unlock next level if win
    public  string nextLevel = "Level02";
    public  int levelUnlock = 2;
    
    public void Continue()
    {        
        PlayerPrefs.SetInt("levelReached", levelUnlock);
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        gameManager.Resume();
        sceneFader.FadeTo(mainMenu);
    }
    
}
