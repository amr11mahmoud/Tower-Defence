using System;
using UnityEngine;
using UnityEngine.UI;

public class levelSelector : MonoBehaviour
{
    public SceneFader fader;
    public Button[] levelButtons;


    private void Start()
    {
        // Load which levels are Unlocked for the player
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        if (levelReached == 10)
        {
            PlayerPrefs.SetString("gameUnlocked", "true");
        }

        string unLocked = PlayerPrefs.GetString("gameUnlocked");
        // Lock all levels greater than what a player has reached
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i +1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }

            if (unLocked == "true")
            {
                levelButtons[i].interactable = true; 
            }
        }
    }

    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
