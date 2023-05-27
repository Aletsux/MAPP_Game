using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleExtenderUpgrade : UpgradeScript
{
    public override bool ActiveCondition()
    {
        if (GameController.IsIdleTrue() && GameController.GetCrystals() >= store.GetPrice(title))
            return true;
        return false;
    }
}
