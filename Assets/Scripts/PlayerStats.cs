using System;
using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int startMoney = 400;
    public static int Lives;
    public static int startLives = 1;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }
}
