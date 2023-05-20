using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : ItemScript
{
    public override void Start()
    {
        base.Start();
        table = "ButtonsUpgrade";
        descriptionPrice += "Upgrade " + (index + 1);
    }

    protected override void OnPanelClick()
    {
        SetDescriptionTranslations();
        desc.GetAllInformation(this, false);
    }
}
