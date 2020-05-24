using System;
using UnityEngine;


public class shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint missileLauncher;
    public TurretBluePrint leaserBeam;
    
    // Get instance from the Build Manager
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.instance;
    }

    // if this method get called, then standard turret will set to be built [ it called when you press on the turret icon on the game screen]
    public void SelectStandardTurret()
    {
        _buildManager.selectTurretToBuild(standardTurret);
    }
    
    // if this method get called, then missile launcher will set to be built [ it called when you press on the turret icon on the game screen]
    public void SelectMissileLauncher()
    {
        _buildManager.selectTurretToBuild(missileLauncher);

    }
    
    // if this method get called, then leaser beam will set to be built [ it called when you press on the turret icon on the game screen]
    public void SelectLeaserBeam()
    {
        _buildManager.selectTurretToBuild(leaserBeam);

    }
}
