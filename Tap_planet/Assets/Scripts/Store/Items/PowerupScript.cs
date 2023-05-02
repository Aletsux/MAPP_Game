using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : ItemScript
{
    public override void Start()
    {
        table = "ButtonsPowerup";
        base.Start();
        priceKey += "Powerup " + (index + 1);
    }

    protected override void OnPanelClick()
    {
        GetStringForUI();
        desc.GetAllInformation(this, false);
    }
}