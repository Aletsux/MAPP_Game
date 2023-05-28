using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class PlanetScript : ItemScript
{
    private string buyButtonTable = "ButtonBuy";
    private string buyButtonKey;

    public override void Start()
    {
        table = "ButtonsPlanet";
        descriptionPrice += "Planet " + (index + 1);
        base.Start();
    }

    protected override void OnPanelClick()
    {
        SetDescriptionTranslations();
        desc.GetAllInformation(this, false);
    }

    public override void OnBuyClick()
    {
        SetDescriptionTranslations();
        desc.GetAllInformation(this, true);
        if (GameController.GetStardust() >= store.GetPrice(title) || PlayerPrefs.GetInt("PlanetPurchased_" + index) == 1)
        {
            store.EquipPlanet(index);
            SetBuyButtonText();
        }
    }

    protected override void SetBuyButtonText()
    {
        if (PlayerPrefs.GetInt("PlanetPurchased_" + index) == 0)
        {
            buyButtonText.text = ReturnPrice();
        }
        else if (PlayerPrefs.GetInt("ActivePlanetIndex") == index)
        {
            buyButtonKey = "Current";
            buyButtonText.text = LocalizationSettings.StringDatabase.GetLocalizedString(buyButtonTable, buyButtonKey);
        }
        else
        {
            buyButtonKey = "Completed";
            buyButtonText.text = LocalizationSettings.StringDatabase.GetLocalizedString(buyButtonTable, buyButtonKey);
        }
    }

    public void SetCompleted()
    {
        SetBuyButtonText();
    }

    public override bool ActiveCondition()
    {
        if ((PlayerPrefs.GetInt("PlanetPurchased_" + index) == 1) || base.ActiveCondition())
            return true;
        return false;
    }
}