using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // carry which turret we will build 
    private TurretBluePrint turretToBuild;

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

    public GameObject buildEffect;
    
    // property we get it's value only we can't set it
   public bool CanBuild
   {
       get { return turretToBuild != null; }
   }
   
   public bool HasMoney
   {
       get { return  PlayerStats.Money >= turretToBuild.cost; }
   }

    // method used to set which Turret to built [ it will be called in Shop Script]
    public void selectTurretToBuild(TurretBluePrint Turret)
    {
        turretToBuild = Turret;
    }
    
    // method to build the Turret on top of the Node
    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log(" Not Enough Money To build");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        // Build a Turret
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition() , Quaternion.identity);
        node.turret = turret;
        GameObject buildEfx = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(buildEfx, 5f);
        Debug.Log((" Turret build ! Money Left : ") + PlayerStats.Money);
    }
}
