using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();

        int numOfMusicPlayers = FindObjectsOfType<BackgroundMusic>().Length;
        if (numOfMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
       
       
    }
    
    public void setLevel(float sliderValue)
    {
        source.volume = sliderValue;
    }

    private void Update()
    {
        if (GameManager.gameIsPaused)
        {
            source.Stop();
        }else if (source.isPlaying == false)
        {
            source.Play();
        }
    }
}
