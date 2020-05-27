using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // carry which turret we will build 
    private TurretBluePrint turretToBuild;
    // carry which node we select to sell or upgrade the turret on it
    private Node selectedNode;
    public NodeUI nodeUI;
    
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
   

    public void  SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        // so we only select one at time, Node or Turret to build 
        turretToBuild = null;
        // Disable the Node UI before enable it again to display the animation
        nodeUI.hideNodeUI();
        nodeUI.setTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.hideNodeUI();
    }

    
    // method used to set which Turret to built [ it will be called in Shop Script]
    public void selectTurretToBuild(TurretBluePrint Turret)
    {
        turretToBuild = Turret;

        // so we only select one at time, Node or Turret to build 
        DeselectNode();
    }


    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
