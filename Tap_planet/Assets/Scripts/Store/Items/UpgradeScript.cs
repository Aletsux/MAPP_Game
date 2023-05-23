using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class UpgradeScript : ItemScript
{
    private Text levelText;
    private string levelKey = "upgradeLevel";
    public bool usingChangeText;

    public override void Start()
    {
        levelText = transform.GetChild(3).GetComponent<Text>();
        table = "ButtonsUpgrade";
        if (!usingChangeText)
        {
            descriptionPrice += "Upgrade " + (index + 1);
        }
        SetBuyButtonText();
        SetLevelText();
        base.Start();
    }

    protected override void OnPanelClick()
    {
        if (!usingChangeText)
            SetDescriptionTranslations();
        desc.GetAllInformation(this, false);
    }

    protected override void OnBuyClick()
    {
        if (!usingChangeText)
            SetDescriptionTranslations();
        store.BuyPowerUp(title);
        SetBuyButtonText();
        SetLevelText();
        desc.GetAllInformation(this, true);
    }

    private void SetLevelText()
    {
        string level = (GameController.GetLevel(title) == 0) ? "" : LocalizationSettings.StringDatabase.GetLocalizedString(table, levelKey) + GameController.GetLevel(title);
        levelText.text = level;
        print(title + " "  + GameController.GetLevel(title));
    }
}