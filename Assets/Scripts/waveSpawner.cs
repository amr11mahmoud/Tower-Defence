using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// responsible to create enemy waves 
public class waveSpawner : MonoBehaviour
{
    // you can use static in variable to access and change it from other scripts without a reference 
    public static int EnemiesAlive = 0;
    
    // the enemy prefab that will be Instantiated
    public Wave[] waves;
    
    // where to spawn the enemy
    public Transform spawnPoint;
    
    // time between each wave
    public float timeBetweenWaves = 5f;
    
    // time before first wave
    private float countdown = 2f;
    
    // number of enemies per wave [ will increase by one in each wave ]
    private int waveIndex = 0;
    
    // Countdown Timer Text
    public Text waveCountdownText;

    public GameManager gameManager;

     void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }
        
        // load next level after killing all enemies
        if (LoadNextLevel()) return;
        
        if (countdown <= 0f)
        {
            // call the coroutine 
            StartCoroutine(spawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        
        // decrease the countdown by 1 each second
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        
        // Change the UI text 
        waveCountdownText.text = "Timer: "+string.Format("{0:00.00}", countdown);
    }

     private bool LoadNextLevel()
     {
         if (waveIndex == waves.Length)
         {
             gameManager.winLevel();
             this.enabled = false;
             return true;
         }
         
         return false;
     }

     // coroutine to create the wave
     IEnumerator spawnWave()
     {
         PlayerStats.roundsSurvived++;
         // Get the wave to spawn
         Wave wave = waves[waveIndex];
         
         EnemiesAlive = wave.count;
         for (int i = 0; i < wave.count; i++)
         {
             spawnEnemy(wave.enemy);
             // wait Wave rate sec between each enemy we create
             yield return new WaitForSeconds(1f / wave.rate);
         }
         waveIndex++;
     }

     // method to spawn the enemy
     void spawnEnemy( GameObject enemy)
     { 
         Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
     }
}
