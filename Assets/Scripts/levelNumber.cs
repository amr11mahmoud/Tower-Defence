using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelNumber : MonoBehaviour
{
    public Text level;

    private void Start()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        level.text = "Level: "+ currentLevel.ToString();
    }
}
