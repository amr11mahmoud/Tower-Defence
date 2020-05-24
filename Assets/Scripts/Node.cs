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
    [Header("Optional")]
    public GameObject turret;
    
    // offset to raise the Turret above the ground 
    public Vector3 positionOffset;

    private BuildManager _buildManager;

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
        
        // return nothing if no turret is selected
        if (!_buildManager.CanBuild )
        {
            return;
        }
        
        if (turret != null)
        {
            Debug.Log(" Can't Build There ! there is already a turret here "); //TODO display on Screen
            return;
        }
        
        // method that will build the turret
        _buildManager.BuildTurretOn(this);

    }

    // locate the position to build the turret [ will be called at Build Manager]
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
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
}
