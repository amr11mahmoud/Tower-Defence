using System;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // the target that the bullet will chase 
    private Transform target;

    public float speed = 70f;
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
    }

    void hitTarget()
    {
        // display the effect of bullet get destroyed after hitting the target 
        GameObject effectIns = (GameObject)Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        Destroy(target.gameObject);
        
    }
}
