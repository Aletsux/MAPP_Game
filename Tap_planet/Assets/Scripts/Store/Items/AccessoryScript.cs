using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class AccessoryScript : ItemScript
{
    private string buyButtonTable = "ButtonBuy";
    private string buyButtonKey;

    public override void Start()
    {
        table = "ButtonsAccessories";
        base.Start();
        descriptionPrice += "Accessories " + (index + 1);
        SetBuyButtonText();
    }

    protected override void OnPanelClick()
    {
        SetDescriptionTranslations();
        desc.GetAllInformation(this, false);
    }

    protected override void OnBuyClick()
    {
        SetDescriptionTranslations();
        desc.GetAllInformation(this, true);
        if (GameController.GetStardust() >= store.GetPrice(title) || PlayerPrefs.GetInt("AccessoryPurchased_" + index) == 1)
        {
            store.EquipAccessory(index);
            SetBuyButtonText();
        }
    }

    protected override void SetBuyButtonText()
    {
        if (PlayerPrefs.GetInt("AccessoryPurchased_" + index) == 0)
        {
            buyButtonText.text = ReturnPrice();
        }
        if (PlayerPrefs.GetInt("AccessoryEquipped_" + index) == 1)
        {
            buyButtonKey = "Equipped";
            buyButtonText.text = LocalizationSettings.StringDatabase.GetLocalizedString(buyButtonTable, buyButtonKey);
        }
        else if (PlayerPrefs.GetInt("AccessoryEquipped_" + index) == 0 && PlayerPrefs.GetInt("AccessoryPurchased_" + index) == 1)
        {
            buyButtonKey = "Equip";
            buyButtonText.text = LocalizationSettings.StringDatabase.GetLocalizedString(buyButtonTable, buyButtonKey);
        }
    }
}