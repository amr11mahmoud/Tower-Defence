using System;
using UnityEngine;

public class Node : MonoBehaviour
{

    // Get hover color from the inspector
    [Header("Colors")]
    public Color hoverColor;
    public Color defualtColor;
    // the variable that will store the component to change it's material color 
    private Renderer rend;
    
    // info that will be stored in this Node
    private GameObject turret;
    
    // offset to raise the Turret above the ground 
    public Vector3 positionOffset;

    private void Start()
    {
        // Get the component if we hover on it 
        rend = GetComponent<Renderer>();
        defualtColor = rend.material.color;
    }

    // method that get called when we press on the Node [ will build the turret ]
    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log(" Can't Build There ! there is already a turret here "); //TODO display on Screen
            return;
        }
        
        // Build a Turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset , transform.rotation);
        
    }

    // built in function get calls on each hover
    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    
    // built in function get calls when hover ends
    private void OnMouseExit()
    {
        rend.material.color = defualtColor;
    }
}
