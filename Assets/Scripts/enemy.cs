using System;
using UnityEngine;

// sudo code this script 
// 1. we create an array of waypoints in other script with the name of wayPoints that contain all points we want to reach
// 2. we create speed , and target is the next point we want the enemy to reach
// 3. we use the variable wavePointIndex to increase the index in array to store the next point in the target
// 4. 


public class enemy : MonoBehaviour
{
    // enemy speed
    public float speed = 10f;
    // the way point that the enemy will move to
    private Transform target;
    private int wavePointIndex = 0;
    // enemy health
    public int health = 100;
    // money you will get after killing an enemy
    public int moneyGain = 50;

     void Start()
     {
         target = wayPoints.points[0];
     }

     public void TakeDamage(int amount)
     {
         health -= amount;
         if (health <= 0)
         {
             Die();
         }
     }

     private void Die()
     {
         PlayerStats.Money += moneyGain;
         Destroy(gameObject);
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
          DestroyImmediate(gameObject);
          PlayerStats.Lives--;
      }
}
