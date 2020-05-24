using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class livesUI : MonoBehaviour
{
    public Text liveText;

    private void Update()
    {
        liveText.text = PlayerStats.Lives.ToString() + " Lives";
    }
}
