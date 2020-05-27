using System;
using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int startMoney = 1000;
    public static int Lives;
    public static int startLives = 1;

    public static int roundsSurvived;
    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
        roundsSurvived = 0;
    }
}
