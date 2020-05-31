using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // the target we will aim at
    private Transform target;
    // will use this variable to cache enemy, to operate on it if selected turret is laser 
    private enemy targetEnemy;

    // the range of Turret [ if enemy gets in Turret will start shoot at the enemy]
    [Header("General")]
    public float range = 15f;
    
    [Header("Use Bullets (defualt)")]
    public GameObject bulletPrefab;
    public float fireRate =1f;
    private float fireCountDown = 0f;

    [Header("Use leaser")] 
    // check if Turret will use leaser 
    public bool useLeaser = false;
    public int damageOverTime = 30;
    
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light leaserLight;
    public AudioSource laserSound;

    public float slowPct =.5f;
    
    [Header("Unity SetUp Fields")]
    public string enemyTag = "Enemy";
    // Reference to the part we want to Rotate in the Turret [ Head ]
    public Transform partToRotate;
    // speed of Turret Rotation 
    public float turnSpeed = 10f;
    
    [Header("Bullet Attributes")]
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
                targetEnemy = nearestEnemy.GetComponent<enemy>();
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
        if (GameManager.gameIsOver)
        {
            Destroy(gameObject);
        }
        
        // if there is no target do noting, only disable the laser [ if it's laser beam turret ]
        if (target == null)
        {
            if (useLeaser && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                leaserLight.enabled = false;
                laserSound.enabled = false;
            }
            return;
        }

        // Rotate the Turret to face the enemy
        lockOnTarget();

        if (useLeaser) // code for laser
        {
            Laser();
        }
        else // Code for bullet 
        {
            if (fireCountDown <= 0f)
            {
                shoot();
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }

        
    }
    
    // set the direction of the turret head to face the target
    private void lockOnTarget()
    {
        // 1. find the direction 
        Vector3 dir = target.position - transform.position;
        // 2. calculate the angel 
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        // .3 convert to Euler angels [ Quaternion.Lerp is using to smooth the movement from (current rotation) to (next rotation)]
        Vector3 rotation = (Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed)).eulerAngles;
        // 4. rotate the head
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    
    // set laser position, and enable it
    private void Laser( )
    {
      if (targetEnemy != null) 
      { 
          // call get damage and pass the amount of damage
          targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
          targetEnemy.Slow(slowPct);
          laserSound.enabled = true;
      }
      
      // set the position of the laser beam from the Leaser beam to the target
      lineRenderer.SetPosition(0, firePoint.position );
      lineRenderer.SetPosition(1, target.position);

      // get the position of enemy looking back to the turret
      Vector3 dir = firePoint.position - target.position;
      // set the position of the Particle system [ we need to offset it by by half from the center of the enemy ]
      impactEffect.transform.position = target.position + dir.normalized ;
      // set the rotation of the Particle system
      impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        
      // enable the object line renderer
      if (!lineRenderer.enabled)
      {
          lineRenderer.enabled = true;
          impactEffect.Play();
          leaserLight.enabled = true;
      }
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
