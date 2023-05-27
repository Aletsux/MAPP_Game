using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StardustMinerUpgrade : UpgradeScript
{
    protected override void SetBuyButtonText()
    {
        if (GameController.GetStardustMinerLevel() == 20)
        {
            buyButton.image.color = inactiveColor;
            buyButtonText.text = "MAX";
            buyButton.enabled = false;
        }
        else
            buyButtonText.text = ReturnPrice();
    }

    public override bool ActiveCondition()
    {
        long inBank = (costsStardust) ? GameController.GetStardust() : GameController.GetCrystals();
        if (inBank >= store.GetPrice(title) && GameController.GetStardustMinerLevel() < 20)
            return true;
        return false;
    }

    public override string ReturnPrice()
    {
        if (GameController.GetStardustMinerLevel() == 20)
            return "MAX";
        return base.ReturnPrice();
    }
}
