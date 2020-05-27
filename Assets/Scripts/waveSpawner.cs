using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// responsible to create enemy waves 
public class waveSpawner : MonoBehaviour
{
    // the enemy prefab that will be Instantiated
    public Transform enemyPrefab;
    
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

     void Update()
    {
        if (countdown <= 0f)
        {
            // call the coroutine 
            StartCoroutine(spawnWave());
            countdown = timeBetweenWaves;
        }
        
        // decrease the countdown by 1 each second
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        
        // Change the UI text 
        waveCountdownText.text = "Timer: "+string.Format("{0:00.00}", countdown);
    }

     // coroutine to create the wave
     IEnumerator spawnWave()
     {
         waveIndex++;
         PlayerStats.roundsSurvived++;
         for (int i = 0; i < waveIndex; i++)
         {
             spawnEnemy();
             // wait .5 sec between each enemy we create
             yield return new WaitForSeconds(0.5f);
         }
     }

     // method to spawn the enemy
     void spawnEnemy()
     {
         Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
     }
}
