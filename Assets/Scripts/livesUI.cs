using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class livesUI : MonoBehaviour
{
    public GameObject heartLife;
    private static Transform[] hearts;
    public static bool decreaseHearts;
    private static int heartsCount;

    private void Start()
    {
        decreaseHearts = false;
        for (int i = 0; i < PlayerStats.Lives ; i++)
        {
            GameObject child = Instantiate(heartLife) as GameObject;
            child.transform.parent = this.transform;
        }

        heartsCount = PlayerStats.Lives -1 ;
    }

    private void Update()
    {
        if (decreaseHearts && heartsCount >= 0)
        {
            DestroyHeart();
        }
      
    }

    public void DestroyHeart()
    {
        hearts = new Transform[transform.childCount];
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
        }
        Image img = hearts[heartsCount].GetComponent<Image>();
        img.enabled = false;
        heartsCount--;
        decreaseHearts = false;
    }
    
    
}
