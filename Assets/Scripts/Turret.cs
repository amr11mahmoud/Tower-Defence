using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // the target we will aim at
    private Transform target;
    
    // the range of Turret [ if enemy gets in Turret will start shoot at the enemy]
    [Header("Attributes")]
    public float range = 15f;
    public float fireRate =1f;
    private float fireCountDown = 0f;
    
    [Header("Unity SetUp Fields")]
    public string enemyTag = "Enemy";
    // Reference to the part we want to Rotate in the Turret [ Head ]
    public Transform partToRotate;
    // speed of Turret Rotation 
    public float turnSpeed = 10f;
    
    [Header("Bullet Attributes")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    
  
    
    
    
    // we use this method to visualise the range of the Turret 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Start()
    {
        // we call the method [UpdateTarget] twice per second to save computing power 
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // method used to determine which enemy is the nearest one
    void UpdateTarget()
    {
        // find all game objects using it's tag, then store them into array
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        
        // variable to carry the distance between the Turret and the nearest enemy
        float shortestDistance = Mathf.Infinity;
        
        // variable to store the nearest enemy [ the one that we will shot at ]
        GameObject nearestEnemy = null;
        
        // we loop at the array of all enemies and calculate the distance between each one of them and the Turret
        // to decide which one we will shot at
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                // if we found the nearest enemy we will store it's distance in [ shortestDistance ] 
                // and the enemy itself in [nearestEnemy] 
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        
        // check if it founds enemy in it's range it will mark it as a target
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        // if there is no target do noting 
        if (target == null)
            return;

        // Rotate the Turret to face the enemy
        
        // 1. find the direction 
        Vector3 dir = target.position - transform.position;
        // 2. calculate the angel 
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        // .3 convert to Euler angels [ Quaternion.Lerp is using to smooth the movement from (current rotation) to (next rotation)]
        Vector3 rotation = (Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed)).eulerAngles;
        // 4. rotate the head
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        
        // check it countdown is zero it will shoot a bullet and wait till next countdown reach zero [ fireRate will control how long to wait ]
        if (fireCountDown <= 0f)
        {
            shoot();
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    void shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // reference to Bullet 
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
}
