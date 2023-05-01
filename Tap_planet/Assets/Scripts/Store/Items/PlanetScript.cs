using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class PlanetScript : ItemScript
{
    public override void Start()
    {
        table = "ButtonsPlanet";
        base.Start();
        priceKey += "Planet " + (index + 1);
    }

    protected override void OnPanelClick()
    {
        GetStringForUI();
        desc.GetAllInformation(this, false);
    }

    protected override void OnBuyClick()
    {
        GetStringForUI();
        desc.GetAllInformation(this, true);
        store.EquipPlanet(index);
    }
}