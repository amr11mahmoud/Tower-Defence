﻿using System;
using UnityEngine;

// sudo code this script 
// 1. we create an array of waypoints in other script with the name of wayPoints that contain all points we want to reach
// 2. we create speed , and target is the next point we want the enemy to reach
// 3. we use the variable wavePointIndex to increase the index in array to store the next point in the target
// 4. 


public class enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wavePointIndex = 0;

     void Start()
     {
         target = wayPoints.points[0];
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
             DestroyImmediate(gameObject);
             return;
          }
          wavePointIndex++;
          target = wayPoints.points[wavePointIndex];
      }
}