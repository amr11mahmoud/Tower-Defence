using System.Collections;
using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 1000;
    public static int Lives;
    public int startLives = 1;

    public static int roundsSurvived;
    
    private void Awake()
    {
        Money = startMoney;
        Lives = startLives;
        roundsSurvived = 0;
    }
}
