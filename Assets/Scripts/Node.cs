using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{

    // Get hover color from the inspector
    [Header("Colors")]
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Color defualtColor;
    // the variable that will store the component to change it's material color 
    private Renderer rend;
    
    // turret that will be build on top of the node
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;

    [HideInInspector] public bool isUpgraded = false;
    
    // offset to raise the Turret above the ground 
    public Vector3 positionOffset;

    private BuildManager _buildManager;

    private int tapCount;
    private float doubleTapTimer;

    private void Start()
    {
        // Get the component on start
        rend = GetComponent<Renderer>();
        defualtColor = rend.material.color;
        // instantiate build manger on every node
        _buildManager = BuildManager.instance;
    }

    // method that get called when we press on the Node [ will build the turret ]
    private void OnMouseDown()
    {
        
        // if there is any object above the node then we will not be able to build there
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        
        if (turret != null)
        {
            _buildManager.SelectNode(this);
            return;
        }
        
        
        // return nothing if no turret is selected
        if (!_buildManager.CanBuild )
        {
            return;
        }
        
        // BuildTurret(_buildManager.GetTurretToBuild());
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            tapCount++;
        }
        if (tapCount > 0)
        {
            doubleTapTimer += Time.deltaTime;
        }
        if (tapCount >= 2)
        {
            //What you want to do
            // method that will build the turret
            BuildTurret(_buildManager.GetTurretToBuild());
            doubleTapTimer = 0.0f;
            tapCount = 0;
        }
        
        
    }

    // locate the position to build the turret [ will be called at Build Manager]
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void BuildTurret(TurretBluePrint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log(" Not Enough Money To build");
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        // Build a Turret
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition() , Quaternion.identity);
        turret = _turret;

        turretBluePrint = blueprint;
        
        GameObject buildEfx = (GameObject)Instantiate(_buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEfx, 5f);
        Debug.Log((" Turret build ! ") + PlayerStats.Money);
    }
    
    // built in function get calls on each hover
    private void OnMouseEnter()
    {
        // if there is any object above the node then we will not be able to build there
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (!_buildManager.CanBuild)
        {
            return;
        }

        if (_buildManager.HasMoney)
        { 
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;

        }
    }
    
    // built in function get calls when hover ends
    private void OnMouseExit()
    {
        rend.material.color = defualtColor;
    }

    // method to upgrade the turret
    public void UpgradeTurret( )
    {
        if (PlayerStats.Money < turretBluePrint.upgradecost)
        {
            Debug.Log(" Not Enough Money To Upgrade");
            return;
        }

        PlayerStats.Money -= turretBluePrint.upgradecost;
        
        // Get rid of old Turret
        Destroy(turret);
        
        // Build the Upgraded Turret
        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, GetBuildPosition() , Quaternion.identity);
        turret = _turret;
        
        GameObject buildEfx = (GameObject)Instantiate(_buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEfx, 5f);
        
        isUpgraded = true;
        Debug.Log((" Turret upgraded ! ") + PlayerStats.Money);
    }
    
    // method to sell the turret
    public void SellTurret()
    {
        PlayerStats.Money += turretBluePrint.GetSellAmount();
        Destroy(turret);
        turretBluePrint = null;
        // Spawn a cool Effect
        GameObject buildEfx = (GameObject)Instantiate(_buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEfx, 5f);
        
    }
}
