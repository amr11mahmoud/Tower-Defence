using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public Node target;
    public GameObject ui;

    public Text UpgradeCost;
    public Text SellCost;
    
    // Reference to disable it in case if it's already upgraded
    public Button UpgradeButton;
    public void setTarget(Node _node)
    {
        this.target = _node;
        transform.position = target.GetBuildPosition();

        SellCost.text = "$" + target.turretBluePrint.GetSellAmount().ToString();
        if (!target.isUpgraded)
        {
            UpgradeCost.text = "$" + target.turretBluePrint.upgradecost.ToString();
            UpgradeButton.interactable = true;

        }
        else
        {
            UpgradeCost.text = "DONE";
            UpgradeButton.interactable = false;
        }
        ui.SetActive(true);
    }

    public void hideNodeUI()
    {
        ui.SetActive(false);
    }

    public void upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
    
    public void sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
