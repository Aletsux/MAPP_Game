using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherShipHit : PlanetHit
{
    protected override void Start()
    {
        blinkAmount = 4;
        image = GetComponent<Image>();
    }
}
