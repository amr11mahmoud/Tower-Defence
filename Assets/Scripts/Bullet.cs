using System;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // the target that the bullet will chase 
    private Transform target;

    public float speed = 70f;

    public int damage = 50;
    // Radius for the explosion after hitting the target
    public float explosionRadius = 0f;
    
    public GameObject ImpactEffect;

    // this method will use to get the target from Turret Script [ the target that the bullet will chase ] and it will be called at the Turret Script
    public void Seek(Transform _target)
    {
        target = _target;
        
    }
    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // get the direction that bullet will look to
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // if this condition is true then we had hit the target 
        if (dir.magnitude <= distanceThisFrame)
        {
            hitTarget();
            return;
        }
        
        // if the bullet didn't hit the target yet it will move towards it [ Space.World is important]
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        // Rotate the Bullet or the Missile to look at the target
        transform.LookAt(target);
    }

    void hitTarget()
    {
        // display the effect of bullet get destroyed after hitting the target 
        GameObject effectIns = (GameObject)Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        // check if the bullet in radius of the target make an explosion 
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject);
        
    }
    
    // Damage the target
    void Damage(Transform enemy)
    {
        // get specific Component from the Transform enemy using GetComponent<>
        enemy e = enemy.GetComponent<enemy>();
        if (e != null)
        {
            // call get damage and pass the amount of damage
            e.TakeDamage(damage);
        }
    }

    // Explosion Effect when hitting the target
    void Explode()
    {
        Collider [] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }
}
