using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleExtenderUpgrade : UpgradeScript
{
    protected override void Update()
    {
        if (galaxyLvl != PlayerPrefs.GetInt("ActivePlanetIndex"))
        {
            ToggleItemActive();
            galaxyLvl = PlayerPrefs.GetInt("ActivePlanetIndex");
        }
        if (GameController.IsIdleTrue() && GameController.GetCrystals() >= store.GetPrice(title))
        {
            buyButton.image.color = activeColor;
        }
        else
        {
            buyButton.image.color = inactiveColor;
        }
    }
}
