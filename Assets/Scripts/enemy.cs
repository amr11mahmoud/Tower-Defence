using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// sudo code this script 
// 1. we create an array of waypoints in other script with the name of wayPoints that contain all points we want to reach
// 2. we create speed , and target is the next point we want the enemy to reach
// 3. we use the variable wavePointIndex to increase the index in array to store the next point in the target
// 4. 


public class enemy : MonoBehaviour
{
    // enemy speed
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    
    // enemy health
    public float startHealth = 100f;
    [HideInInspector]
    public float health;
    // money you will get after killing an enemy
    public int moneyGain = 50;
    
    // the way point that the enemy will move to [ enemy movement ]
    private Transform target;
    private int wavePointIndex = 0;
    
    
    // Die effect
    public GameObject dieEffect;
    
    // Image that will adjust the health bar according to the damage that enemy take
    public Image healthBarImage;
    private float startFillAmount = 1;
    private float fillAmount;

    private bool isDead = false;
    public GameObject fuckSoundEffect;

    void Start()
     {
         // related to enemy movement
         target = wayPoints.points[0];
         speed = startSpeed;
         fillAmount = startFillAmount;
         health = startHealth;
     }

     public void TakeDamage(float amount)
     {
         health -= amount;
         adjustHealthBar();
         if (health <= 0 && !isDead)
         {
             
                 Die();
         }
     }

     private void adjustHealthBar ()
     {
         fillAmount = health / startHealth;
         if (fillAmount < .7 && fillAmount > .4)
         {
             healthBarImage.color = Color.yellow;
         }
         else if (fillAmount <= .4)
         {
             healthBarImage.color = Color.red;
         }
         else
         {
             healthBarImage.color = Color.green;
         }

         healthBarImage.fillAmount = fillAmount;
     }

     public void Slow(float slowPct)
     {
         speed = startSpeed * (1f - slowPct);
     }

     private void Die()
     {
         isDead = true;
         // Die Effect
         GameObject dieEffectgfx = (GameObject)Instantiate(dieEffect, transform.position, transform.rotation);
         GameObject fuckSoundfx = (GameObject)Instantiate(fuckSoundEffect, transform.position, transform.rotation);
         Destroy(dieEffectgfx, 2f);
         StartCoroutine(destroyFuckSound());
         PlayerStats.Money += moneyGain;
         
         // Subtract one enemy from enemies alive
         waveSpawner.EnemiesAlive--;
         Destroy(gameObject);
     }

     IEnumerator destroyFuckSound()
     {
         yield return new WaitForSeconds(1f);
         DestroyImmediate(fuckSoundEffect);
     }
     

     void Update()
      {
          // to reach target to substract our current position from it [ that give us the distance ] 
          Vector3 dir = target.position - transform.position;
          // transform [ Translate ] is using to move the object from place to another 
          transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            
          // check if we reached our target to store next point in target variable and do things again
          if (Vector3.Distance(transform.position, target.position) <= 0.4f)
          {
              GetNextWayPoint();
          }
          // return speed to normal [ start speed ] after it scape from the laser range 
          speed = startSpeed;
      }

      private void GetNextWayPoint()
      {
          if (wavePointIndex >= wayPoints.points.Length -1)
          {
              EndPath();
              return;
          }
          wavePointIndex++;
          target = wayPoints.points[wavePointIndex];
      }

      void EndPath()
      {
          livesUI.decreaseHearts = true;
          DestroyImmediate(gameObject);
          PlayerStats.Lives--;
          waveSpawner.EnemiesAlive--;
      }
}
