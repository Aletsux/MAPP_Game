using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class ShieldUpgrade : PowerupScript
{
    protected override void SetBuyButtonText()
    {
        if (GameController.GetPowerupAmount(title) == 1)
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
        if (GameController.GetStardust() >= store.GetPrice(title) && GameController.GetPowerupAmount(title) < 1)
            return true;
        return false;
    }

    public override string ReturnPrice()
    {
        if (GameController.GetPowerupAmount(title) == 1)
            return "MAX";
        return base.ReturnPrice();
    }

    public override void SetAmountText()
    {
        string amount = (GameController.GetPowerupAmount(title) == 1) ? "MAX" : "0";
        amountText.text = LocalizationSettings.StringDatabase.GetLocalizedString(table, amountKey) + amount;
    }
}