using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;

public class TranslateBuy : MonoBehaviour
{
    public TMP_Text descriptionBuy;

    public string descriptionBuyKey;
    public string descriptionBuyTable;

    public void GetStringForUIBuy()
    {
        descriptionBuy.text = LocalizationSettings.StringDatabase.GetLocalizedString(descriptionBuyTable, descriptionBuyKey);
    }
}
