using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class AccessoryScript : ItemScript
{
    public override void Start()
    {
        table = "ButtonsAccessories";
        base.Start();
        priceKey += "Accessories " + (index + 1);
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
        store.EquipAccessory(index);
    }

    public override string ReturnPrice()
    {
        double price = store.GetPrice(title);
        if (price < 1000)
        {
            return price.ToString();
        }
        else if (price < 1000000)
        {
            return (price / 1000).ToString() + "k";
        }
        else if (price < 1000000000)
        {
            return (price / 1000000).ToString() + "M";
        }
        else
        {
            return (price / 1000000000).ToString() + "B";
        }
    }
}