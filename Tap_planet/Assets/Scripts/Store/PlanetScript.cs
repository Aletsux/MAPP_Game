using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : ItemScript
{
    public override void OnBuyClick()
    {
        desc.GetAllInformation(this, true);
        store.EquipPlanet(index);
    }
}