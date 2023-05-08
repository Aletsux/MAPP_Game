using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : ItemScript
{
    public override void Start()
    {
        base.Start();
        table = "ButtonsUpgrade";
        priceKey += "Upgrade " + (index + 1);
    }

    protected override void OnPanelClick()
    {
        GetStringForUI();
        desc.GetAllInformation(this, false);
    }
}
