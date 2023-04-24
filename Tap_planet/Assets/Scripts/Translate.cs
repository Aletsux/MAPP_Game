using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;

public class Translate : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    public string titleKey;
    public string titleTable;
    public string descriptionKey;
    public string descriptionTable;

    public void GetStringForUI() {
        title.text = LocalizationSettings.StringDatabase.GetLocalizedString(titleTable, titleKey);
        description.text = LocalizationSettings.StringDatabase.GetLocalizedString(descriptionTable, descriptionKey);
    }
}
