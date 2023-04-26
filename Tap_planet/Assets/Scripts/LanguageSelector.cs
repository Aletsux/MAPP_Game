using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSelector : MonoBehaviour
{

    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LanguageKey", 0);
        ChangeLanguage(ID);
    }

    private bool active = false;

    public void ChangeLanguage(int languageID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLanguage(languageID));
    }
   IEnumerator SetLanguage(int languageID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageID];
        PlayerPrefs.SetInt("LanguageKey", languageID);
        active = false;
    }



}
