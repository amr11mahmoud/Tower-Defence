using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private GameObject turretToBuild;
    
    // get this from the inspector
    public GameObject standardTurretPrefab;

    // variable to carry the Build Manager
    public static BuildManager instance;

    private void Awake()
    {
        // create the build manager and store it at instance variable 
        if (instance != null)
        {
            Debug.LogError(" More than One Build Manager on scene ");
        }
        instance = this;
    }


    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    // will call at Node to pass the turret we will build [ the player chosen one ]
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
